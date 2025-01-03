using System.Diagnostics.CodeAnalysis;

// C# � un linguaggio strongly-typed, quindi non � consentito assegnare ad una
// variabile valori di un tipo diverso rispetto al tipo dichiarato.

// Il controllo che il tipo assegnato coincida con il tipo dichiarato viene
// eseguito in fase di compilazione ed � garantito che se un programma compila,
// il valore in una variabile � del tipo previsto dalla variabile.

string str0 = "hello"; // OK
string str1 = 123; // Error: cannot implicitly convert 'int' to 'string'.

// Anche i controlli di nullabilit� sui "value type" vengono eseguiti in fase
// di compilazione, garantendo che se un programma compila, il valore in una
// variabile non nullabile non pu� essere null.

// C# distingue anche una variabile nullabile da una variabile non nullabile.
int num0 = null; // Error: cannot convert null to 'int' because it's a non-nullable value type.
int? num1 = null; // OK

// I controlli di nullabilit� sui "reference type" invece sono stati aggiunti
// pi� recentemente, e sono limitati ad un warning per non rompere la
// compatibilit� con le versioni precedenti del linguaggio e del runtime.

string str3 = null; // Warning: converting null literal or possible null value to non-nullable type.
string? str4 = null; // OK



// Il compilatore fa il possibile per segnalare inconsistenze sulla
// nullabilit� dei tipi...

// Questo metodo accetta un parametro di tipo string non null:
string Duplicate(string str)
{
    return str + str;
}

string? nullStr = null;
string example1 = Duplicate(nullStr); // Warning: possible null reference argument for parameter 'str' in `string Duplicate(string str)`.

string example2 = "";
if (nullStr != null)
{
    example2 = Duplicate(nullStr); // OK perch� questo codice non viene mai eseguito se str4 � null.
}

// ...ma non sempre con i "reference type" � in grado di farlo correttamente.

string? test = null;
bool isNull = test == null;
if (isNull)
{
    test = "not null";
}

string example3 = Duplicate(test); // Warning: possible null reference argument for parameter 'str' in `string Duplicate(string str)`.

// In questo caso � possibile zittire il warning:
string example4 = Duplicate(test!); // OK

// Usare il ! (chiamato operatore di "null-forgiving") disabilita i controlli
// sui null da parte del compilatore, causando potenziali problemi a runtime:
string? test2 = null;
string bad = test2!.Substring(1); // Nessun warning, eccezione a runtime.

// Nota dell'autore: personalmente consiglio di non utilizzare mai l'operatore
// di "null-forgiving" a meno che non si stia scrivendo del codice estremamente
// minimale e si abbia assoluta certezza non dovr� mai essere modificato.
// Una soluzione pi� affidabile per gestire questa casistica � per esempio:
string good = (test2 ?? throw new Exception("test2 is null")).Substring(1);



// In alcune situazioni pi� complesse il compilatore potrebbe non essere in
// grado di rilevare in autonomia quando un valore � null o non null:
bool TryFindBad(IEnumerable<string> items, string startsWith, out string? found)
{
    foreach (string item in items)
    {
        if (item.StartsWith(startsWith))
        {
            // Se questo metodo restituisce true,
            // found � sicuramente diverso da null.
            found = item;
            return true;
        }
    }

    found = null;
    return false;
}

string[] items = new[] { "banana", "tomato", "apple" };
if (TryFindBad(items, "toma", out string? toma))
{
    // Dato che TryFind1(�) ha restituito true,
    // toma � sicuramente diverso da null,
    // ma il compilatore non lo sa...

    string example = Duplicate(toma); // Warning: possible null reference argument for parameter 'str' in `string Duplicate(string str)`.
}

// In tal caso � possibile aiutare il compilatore tramite attributi:
bool TryFindGood(IEnumerable<string> items, string startsWith, [NotNullWhen(true)] out string? found)
{
    foreach (string item in items)
    {
        if (item.StartsWith(startsWith))
        {
            // Se questo metodo restituisce true,
            // found � sicuramente diverso da null.
            found = item;
            return true;
        }
    }

    found = null;
    return false;
}

if (TryFindGood(items, "toma", out string? toma2))
{
    string example = Duplicate(toma2); // OK
}



// Purtroppo esistono anche situazioni in cui una variabile pu� assumere
// valore null senza che il compilatore non se ne renda conto.

MyStruct hello = new();
string oops = hello.ThisIsNull.Substring(1); // Nessun warning, eccezione a runtime.

// � necessario fare attenzione, in particolare quando nel codice sono presenti
// operatori di "null-forgiving" e quando sono presenti "value type" (struct)
// che contengono "reference type" all'interno.

struct MyStruct
{
    public string ThisIsNull { get; set; }
}



// Per ulteriori informazioni:
// https://learn.microsoft