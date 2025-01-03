// Supponendo di avere necessit� di implementare una classe che supporta pi�
// tipi di dati (in questo esempio int e string)...

TupleInt esempio1 = new(1, 2);
TupleString esempio2 = new("hello", "world");

// ...� possibile usare i generics per scrivere le classi una volta sola.

Tuple<int> esempio3 = new(1, 2);
Tuple<string> esempio4 = new("hello", "world");



public class TupleInt
{
    public int Item1 { get; set; }
    public int Item2 { get; set; }

    public TupleInt(int item1, int item2)
    {
        Item1 = item1;
        Item2 = item2;
    }
}

public class TupleString
{
    public string Item1 { get; set; }
    public string Item2 { get; set; }

    public TupleString(string item1, string item2)
    {
        Item1 = item1;
        Item2 = item2;
    }
}

public class Tuple<T>
{
    public T Item1 { get; set; }
    public T Item2 { get; set; }

    public Tuple(T item1, T item2)
    {
        Item1 = item1;
        Item2 = item2;
    }
}



// Per ulteriori informazioni:
// https://learn.microsoft.com/d