#nullable disable

// In C# esistono vari tipi di "collection" pi� o meno adatte alle varie esigenze.

// Array:
// - accesso per indice O(1)
// - ricerca O(n)
// - non � possibile aggiungere elementi
// - non � possibile cancellare elementi
int[] arrayNumeri = new[] { 1, 2, 3 };

// legge il primo valore
int primoNumeroArray = arrayNumeri[0];

// rimpiazza il primo valore
arrayNumeri[0] = 10;



// List:
// - accesso per indice O(1)
// - ricerca O(n)
// - aggiunta O(n)
// - cancellazione O(n)
List<int> listNumeri = new() { 1, 2, 3 };

// legge il primo valore
int primoNumeroList = listNumeri[0];

// rimpiazza il primo valore
listNumeri[0] = 10;

// aggiunge un valore alla fine
listNumeri.Add(4);

// rimuove il secondo
listNumeri.RemoveAt(1);



// LinkedList:
// - accesso per indice non consentito
// - ricerca O(n)
// - aggiunta O(1)
// - cancellazione O(1)
LinkedList<int> linkedListNumeri = new();
linkedListNumeri.AddLast(1);
linkedListNumeri.AddLast(2);
linkedListNumeri.AddLast(3);

// aggiunge un valore all'inizio
linkedListNumeri.AddFirst(10);

// aggiunge un valore alla fine
linkedListNumeri.AddLast(4);

// rimuove il valore che precede l'ultimo
linkedListNumeri.Remove(linkedListNumeri.Last.Previous);



// HashSet:
// - accesso per indice non consentito
// - ricerca O(1)
// - aggiunta elemento O(1)
// - cancellazione elemento O(1)
HashSet<int> setNumeri = new() { 1, 2, 3 };

// aggiunge un valore
setNumeri.Add(4);

// rimuove un valore
setNumeri.Remove(2);

// ricerca per valore
bool setContiene3 = setNumeri.Contains(3);



// Dictionary
// - accesso per chiave O(1)
// - ricerca per chiave O(1), ricerca per valore O(n)
// - aggiunta elemento O(1)
// - cancellazione elemento O(1)
Dictionary<string, int> dicNumeri = new()
{
    ["Uno"] = 1,
    ["Due"] = 2,
    ["Tre"] = 3,
};

// legge il valore associato alla chiave "Tre"
int tre = dicNumeri["Tre"];

// legge il valore associato alla chiave "Dieci"
dicNumeri["Dieci"] = 10;

// rimuove la chiave "Due" ed il valore associato
dicNumeri.Remove("Due");



// Inoltre � prevista un'astrazione IEnumerable che rappresenta una serie di valori
// non modificabile. Tutte le "collection" sono anche IEnumerable, ma l'efficienza
// di ciascuna operazione su un IEnumerable dipende dalla "collection" sottostante.
IEnumerable<int> seqNumeri = new[] { 1, 2, 3 };

// legge il primo valore
int primoNumeroSeq = seqNumeri.ElementAt(0);

// ricerca per valore
bool seqContiene3 = seqNumeri.Contains(3);



// � possibile iterare tutti i valori IEnumerable, ovvero i valori delle "collection",
// utilizzando il ciclo foreach di C#.
foreach (int numero in listNumeri)
{
    Console.WriteLine(numero);
}