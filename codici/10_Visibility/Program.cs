// I modificatori di accessibilit� permettono di indicare dove ciascun elemento
// nel codice pu� essere utilizzato.

// C# supporta i seguenti modificatori di accessibilit�:
//
// public � visibile ovunque.
//
// protected � visibile all�interno della stessa classe e sottoclassi.
//
// private � visibile solo all�interno della stessa classe.
//
// internal � visibile ovunque nello stesso assembly.
//
// protected internal � visibile ovunque nello stesso assembly, e visibile
// nelle sottoclassi anche se sono definite in altri assembly.

// Classe "public" visibile ovunque nel codice.
public class MyClass
{
    // Campo "private" visibile solo all'interno della stessa classe.
    private string _field;

    // Campo "internal" visibile ovunque nello stesso assembly.
    internal string _internalField;

    // Property "public" visibile ovunque.
    public string Property
    {
        get => _field;

        // Setter "protected" visibile solo all'interno della stessa classe
        // e delle sottoclassi di questa classe.
        protected set => _field = value;
    }

    // Property "protected internal" visibile ovunque nello stesso assembly,
    // ed all'interno della stessa classe e sottoclassi di questa classe.
    protected internal string Property2 { get; set; }

    // Costruttore pubblico visibile ovunque.
    public MyClass(string val)
    {
        _field = val;
        _internalField = val;

        Property2 = "Initial Value";
    }

    // Metodo pubblico visibile ovunque.
    public void Print()
    {
        Console.WriteLine($"Field: {FormatField()}");
    }

    // Metodo protected visibile solo all'interno della stessa classe
    // e delle sottoclassi di questa classe.
    // Essendo virtual, pu� essere rimpiazzato dalle sottoclassi.
    protected virtual string FormatField()
    {
        return _field;
    }
}

// Se non indicato, il modificatore di accessibilit� di classi, struct,
// interfacce, delegate, enum e record, � implicitamente "internal".
class MyInternalClass : MyClass
{
    // Se non indicato, il modificatore dei membri di una classe �
    // implicitamente "private".
    // Ha lo stesso nome del campo nella classe base, ma non � lo stesso campo
    // dato che quello nella classe base � "private".
    string _field;

    // I campi readonly possono essere assegnati solo all'interno
    // del costruttore.
    readonly string _readonlyField;

    // Il costruttore � "public", ma dato che la classe � "internal" si
    // comporta come se fosse "internal".
    public MyInternalClass(string val) : base(val)
    {
        _field = val;
        _readonlyField = val;
    }

    // Questo metodo sovrascrive quello della classe base.
    protected override string FormatField()
    {
        return $">>> {base.FormatField()} <<<";
    }
}

public interface IMyInterface
{
    // Tutti i membri delle interfacce sono implicitamente "public".
    void Test();
}

public enum MyEnum
{
    // Tutti i membri delle enum sono implicitamente "public" e non possono
    // avere modificatori di accessibilit� indicati esplicitamente.
    Hello,
    World,
}

// I parametri indicati nei record sono property "public".
public record MyRecord(string Test);



// Per ulteriori informazioni:
// https://learn.micros