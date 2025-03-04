// Dato che in C# la memoria non viene rilasciata esplicitamente tramite "delete",
// � prevista un'interfaccia speciale "IDisposable" per indicare che un oggetto
// deve fare delle operazioni quando non � pi� utilizzato.

public static class Program
{
    // Se � presente un metodo di nome "Main" viene usato come punto di ingresso.
    public static void Main()
    {
        Example1();
        Example2();
        Example3();
    }

    public static void Example1()
    {
        MyDisposableClass test = new();

        // La classe � disposable, quindi deve essere fatta la dispose quando non si utilizza pi� la sua istanza.
        test.Dispose();
    }

    public static void Example2()
    {
        // � possibile automatizzare la dispose al termine di un nuovo blocco con la seguente sintassi:
        using (MyDisposableClass test = new())
        {
            // Al termine di questo blocco di codice, viene automaticamente fatta la dispose.
        }
    }

    public static void Example3()
    {
        // � possibile automatizzare la dispose al termine del blocco corrente con la seguente sintassi:
        using MyDisposableClass test = new();

        // Al termine di questo blocco di codice, viene automaticamente fatta la dispose.
    }
}

// Questa � un'implementazione minimale di IDisposable solo a scopo dimostrativo:
class MyDisposableClass : IDisposable
{
    public void Dispose()
    {
        Console.WriteLine("Disposing");
    }
}

// Questa � un'implementazione corretta di IDisposable:
class BetterDisposable : IDisposable
{
    private bool _disposed;

    protected virtual void Dispose(bool disposing) //protected chiamato solo dall classi che estendono o se stessa
    {
        if (!_disposed)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            _disposed = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~BetterDisposable()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        global::System.Object value = GC.SuppressFinalize(this); //segnala al garbage collection (sportello di emergenza)
    }
}