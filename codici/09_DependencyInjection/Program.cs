using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

// Si supponga di voler implementare una classe DataUploader che legge i dati
// da una sorgente generica IDataAccessor e li invia ad un server generico
// tramite IServerUploader.

IDataAccessor dataAccessor = new FileDataAccessor();
IServerUploader serverUploader = new HttpServerUploader();
IDataUploader dataUploader = new DataUploader(dataAccessor, serverUploader);

await dataUploader.UploadDataToServerAsync();

// Utilizzando questo modello � possibile separare le responsabilit� rendendo
// pi� facile ragionare sul codice.

// Inoltre, se in futuro dovesse sorgere la necessit� di modificare il
// comportamento dell'applicazione, potrebbe essere sufficiente sostituire
// il singolo componente responsabile per l'operazione da modificare.
//
// Per esempio, il FileDataAccessor potrebbe un domani essere rimpiazzato con
// un DatabaseDataAccessor, senza bisogno di modificare il codice delle classi
// che ne fanno uso.



// Microsoft mette a disposizione un set di funzionalit� per rendere il pi�
// trasparente possibile la gestione di queste dipendenze, tramite la pattern
// di design del software chiamata "dependency injection".

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

// I servizi dell'applicazione si configurano cos�;
builder.Services.TryAddSingleton<IDataAccessor, FileDataAccessor>(); // sola istanza
builder.Services.TryAddSingleton<IServerUploader, HttpServerUploader>();
builder.Services.TryAddSingleton<IDataUploader, DataUploader>(); 
builder.Services.TryAddTransient<IDataUploader, DataUploader>(); //per ogni richiesta una nuova istanza
builder.Services.TryAddScoped<IDataUploader, DataUploader>(); // una via di mezzo




// Il codice dell'applicazione risiede negli "hosted service",
// � anche consentito registrare pi� di un "hosted service":
builder.Services.AddHostedService<MainService>();
builder.Services.AddHostedService<SecondaryService>(); 

// Una volta configurato il tutto, l'applicazione pu� essere eseguita:
using IHost host = builder.Build();
await host.RunAsync();

class MainService : BackgroundService
{
    private readonly IDataUploader _dataUploader;

    public MainService(IDataUploader dataUploader)
    {
        // Nota: le dipendenze dei servizi vengono automaticamente risolte
        // ed iniettate dal "service provider" di Microsoft; � sufficiente
        // indicarle come parametri al costruttore.
        _dataUploader = dataUploader;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _dataUploader.UploadDataToServerAsync(stoppingToken);
    }
}



// I servizi dell'applicazione possono essere registrati con "lifetime":
//
// Singleton - servizi per i quali esiste una ed una sola istanza in tutta
// l'applicazione; questa istanza � condivisa tra tutti gli utilizzatori
// e deve generalmente essere thread-safe in quanto potrebbe essere utilizzata
// contemporaneamente da pi� punti nel codice.
//
// Transient - servizi con istanze effimere; per ogni utilizzatore che fa uso
// di questo servizio, viene automaticamente creata una nuova istanza, che
// verr� poi automaticamente "disposed" insieme all'utilizzatore.
//
// Scoped - servizi per i quali viene creata una singola istanza per "scope";
// in ASP.NET Core lo "scope" � una singola richiesta HTTP, ovvero se i
// controller che gestiscono una richiesta fanno uso di un servizio
// registrato con lifetime "scoped", verr� creata una nuova istanza per
// richiesta HTTP, e verr� poi "disposed" dopo aver inviato una risposta.

// Per ulteriori informazioni:
// https://learn.microso