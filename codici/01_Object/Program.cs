// In C# � possibile definire classi con questa sintassi:
class MyClass
{
    // Questo � un campo.
    private int _field;

    // I campi "readonly" possono essere assegnati solo dal costruttore.
    private readonly int _readonlyField;

    // Questa � una property con getter e setter generati automaticamente.
    public int AutoProperty { get; set; }

    // Questa � una property con getter e setter indicati manualmente.
    public int Property
    {
        get
        {
            return _field;
        }
        set
        {
            _field = value;
        }
    }

    // � supportata anche una sintassi pi� semplice.
    public int Property2
    {
        get => _field;
        set => _field = value;
    }

    // Le property possono anche avere solo un getter, o volendo solo un setter (raramente).
    public int GetterOnly
    {
        get => _readonlyField;
    }

    // Per le property con solo un getter � supportata anche una sintassi pi� semplice.
    public int GetterOnly2 => _readonlyField;

    // Questo � il costruttore della classe.
    public MyClass(int value)
    {
        _readonlyField = value;
    }

    // Questo � un metodo.
    public void PrintValue()
    {
        Console.WriteLine($"Il valore della property �: {this.Property}");
    }

    // � possibile definire un metodo "static" accessibile ovunque senza bisogno di creare istanze della classe.
    public static void Hello()
    {
        Console.WriteLine("Hello!");
    }
}



// Ereditariet� e polimorfismo sono supportati.

// Una classe "abstract" non permette la creazione di istanze.
public abstract class Animal
{
    // Un metodo o una property pu� essere "abstract" per indicare che non �
    // definito un effetto. Questo � possibile solo in classi astratte.
    public abstract string Family { get; }

    public abstract string Name { get; }

    public abstract void Talk();
}

public class Dog : Animal
{
    // "override" indica che si sta definendo l'effetto di un metodo o
    // di una property definita in una classe astratta.
    public override string Family => "Canidae";

    // Una auto-property con solo un getter si comporta come un campo readonly,
    // ovvero � assegnabile solamente dal costruttore della classe.
    public override string Name { get; }

    public Dog(string name)
    {
        Name = name;
    }

    public override void Talk()
    {
        Console.WriteLine("Bau bau!");
    }
}

public class Cat : Animal
{
    public override string Family => "Felidae";

    public override string Name { get; }

    public Cat(string name)
    {
        Name = name;
    }

    public override void Talk()
    {
        Console.WriteLine("Miao!");
    }
}



// Non � supportata l'ereditariet� multipla, ma per ovviare questo problema � possibile fare uso di interfacce.
// Una classe pu� ereditare da una sola classe padre, ma pu� implementare pi� di un'interfaccia.
public interface IAnimal
{
    string Family { get; }

    string Name { get; }

    void Talk();
}

public interface IJumping
{
    void Jump();
}

public class Human : Animal, IAnimal, IJumping
{
    public override string Family => "Hominidae";

    public override string Name { get; }

    public Human(string name)
    {
        Name = name;
    }

    public override void Talk()
    {
        Console.WriteLine("Ciao!");
    }

    public void Jump()
    {
        Console.WriteLine("Hop!");
    }
}




// Le "struct" sono equivalenti alle "class", ma non supportano ereditariet�; possono implementare interfacce.
// A differenza delle classi, le strutture risiedono sullo stack piuttosto che sulla heap.

public struct MyStruct : IJumping //estenendo un interfaccia la struct viene copiata nello heap
{
    public void Jump()
    {
        Console.WriteLine("Jump called.");
    }
}



// I "delegate" permettono di definire la firma di una funzione.
public delegate int MyDelegate(int param, string anotherParam);



// "enum" � equivalente ad una "struct" contenente solo costanti tutti di tipo intero.
public enum MyEnum
{
    Default, // implicitamente 0
    First = 10,
    Second = 20,
    Third, // implicitamente 21
    Fourth, // implicitamente 22
    Hundredth = 100,
}

// Equivalente approssimativamente a:
public struct MyEnum2
{
    public const int Default = 0;
    public const int First = 10;
    public const int Second = 20;
    public const int Third = 21;
    public const int Fourth = 22;
    public const int Hundredth = 100;
}

// "record" � equivalente ad una "class" di sole property assegnate al costruttore.
public record MyRecord(int Test, string Val);

// Equivalente approssimativamente a:
public class MyRecord2
{
    public int Test { get; }

    public string Val { get; }

    public MyRecord2(int test, string val)
    {
        Test = test;
        Val = val;
    }
}



// C# � un linguaggio ad alto livello che mette a disposizione "syntax sugar"
// per implementare funzionalit� complesse con poco codice.
//
// Per esempio una property viene convertita in un campo pi� due metodi.
//
// Per vedere nel dettaglio come uno snippet di codice viene "de-sugared"
// consiglio di utilizzare lo strumento open source SharpLab:
// https://sharplab