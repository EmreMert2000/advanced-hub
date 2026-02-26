// =============================================================
// DERS 06 - DIZILER VE KOLEKSIYONLAR (Arrays & Collections)
// =============================================================
// Bu derste:
// - Tek boyutlu diziler (Array)
// - Cok boyutlu diziler
// - Jagged diziler
// - List<T>
// - Dictionary<TKey, TValue>
// - Queue, Stack
// - Temel CRUD islemleri
// =============================================================

using System;
using System.Collections.Generic;
using System.Linq;

namespace LowCodeLogic
{
    class Lesson06_ArraysAndCollections
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== LESSON 06: Arrays & Collections ===\n");

            // -----------------------------------------------
            // 1) TEK BOYUTLU DIZILER (Single-Dimensional Arrays)
            // -----------------------------------------------
            // Dizi: Ayni tipte, sabit boyutlu eleman koleksiyonu
            // Index 0'dan baslar!

            Console.WriteLine("--- Arrays ---");

            // Tanimlama yontemleri
            int[] numbers = new int[5];                           // 5 elemanlik bos dizi (hepsi 0)
            int[] scores = new int[] { 90, 85, 78, 92, 88 };     // Degerlerle tanimlama
            string[] names = { "Alice", "Bob", "Charlie" };      // Kisa tanimlama

            // Elemana erisim ve degistirme
            numbers[0] = 10;
            numbers[1] = 20;
            numbers[2] = 30;
            numbers[3] = 40;
            numbers[4] = 50;

            Console.WriteLine($"First element:  {numbers[0]}");     // 10
            Console.WriteLine($"Last element:   {numbers[4]}");     // 50
            Console.WriteLine($"Array length:   {numbers.Length}");  // 5

            // Dizi uzerinde dongu
            Console.Write("All scores: ");
            foreach (int score in scores)
            {
                Console.Write($"{score} ");
            }
            Console.WriteLine();

            // Dizi metotlari
            Array.Sort(scores);                       // Sirala
            Console.Write("Sorted:     ");
            foreach (int s in scores) Console.Write($"{s} ");
            Console.WriteLine();

            Array.Reverse(scores);                     // Ters cevir
            int index = Array.IndexOf(scores, 85);     // Eleman bul
            Console.WriteLine($"Index of 85: {index}");

            // -----------------------------------------------
            // 2) COK BOYUTLU DIZILER (Multi-Dimensional Arrays)
            // -----------------------------------------------
            Console.WriteLine("\n--- Multi-Dimensional Arrays ---");

            // 2D dizi (matris)
            int[,] matrix = new int[3, 3]
            {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 9 }
            };

            Console.WriteLine("3x3 Matrix:");
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    Console.Write($"{matrix[row, col],4}");
                }
                Console.WriteLine();
            }
            Console.WriteLine($"Matrix[1,2] = {matrix[1, 2]}");   // 6

            // -----------------------------------------------
            // 3) JAGGED DIZILER (Dizi Icinde Dizi)
            // -----------------------------------------------
            // Her satirin farkli uzunlukta olabilecegi diziler
            Console.WriteLine("\n--- Jagged Arrays ---");

            int[][] jagged = new int[3][];
            jagged[0] = new int[] { 1, 2 };
            jagged[1] = new int[] { 3, 4, 5 };
            jagged[2] = new int[] { 6 };

            for (int i = 0; i < jagged.Length; i++)
            {
                Console.Write($"Row {i}: ");
                foreach (int val in jagged[i])
                {
                    Console.Write($"{val} ");
                }
                Console.WriteLine();
            }

            // -----------------------------------------------
            // 4) LIST<T> - Dinamik Boyutlu Liste
            // -----------------------------------------------
            // Array'den farki: boyutu otomatik buyur/kuculur
            Console.WriteLine("\n--- List<T> ---");

            List<string> cities = new List<string>();

            // Ekleme (Add)
            cities.Add("Istanbul");
            cities.Add("Ankara");
            cities.Add("Izmir");
            cities.Add("Antalya");

            Console.WriteLine($"Count: {cities.Count}");

            // Index ile erisim
            Console.WriteLine($"First city: {cities[0]}");

            // Araya ekleme (Insert)
            cities.Insert(1, "Bursa");        // Index 1'e ekle

            // Silme (Remove)
            cities.Remove("Antalya");         // Degere gore sil
            // cities.RemoveAt(0);            // Index'e gore sil

            // Kontrol (Contains)
            bool hasIstanbul = cities.Contains("Istanbul");
            Console.WriteLine($"Contains Istanbul: {hasIstanbul}");

            // Listeleme
            Console.WriteLine("All cities:");
            foreach (string city in cities)
            {
                Console.WriteLine($"  - {city}");
            }

            // Siralama
            cities.Sort();
            Console.Write("Sorted: ");
            cities.ForEach(c => Console.Write($"{c}, "));
            Console.WriteLine();

            // Listeyi temizleme
            // cities.Clear();

            // -----------------------------------------------
            // 5) DICTIONARY<TKey, TValue> - Anahtar-Deger Cifti
            // -----------------------------------------------
            // Her anahtarin benzersiz olmasi gerekir
            Console.WriteLine("\n--- Dictionary<TKey, TValue> ---");

            Dictionary<string, int> studentScores = new Dictionary<string, int>();

            // Ekleme
            studentScores.Add("Alice", 95);
            studentScores.Add("Bob", 82);
            studentScores["Charlie"] = 78;     // Bu sekilde de eklenebilir
            studentScores["Diana"] = 91;

            // Erisim
            Console.WriteLine($"Alice's score: {studentScores["Alice"]}");

            // Guvenli erisim (TryGetValue)
            if (studentScores.TryGetValue("Eve", out int eveScore))
            {
                Console.WriteLine($"Eve's score: {eveScore}");
            }
            else
            {
                Console.WriteLine("Eve not found in dictionary.");
            }

            // Guncelleme
            studentScores["Bob"] = 88;         // Degeri guncelle

            // Silme
            studentScores.Remove("Diana");

            // Kontrol
            bool hasAlice = studentScores.ContainsKey("Alice");
            bool has100 = studentScores.ContainsValue(100);
            Console.WriteLine($"Has Alice: {hasAlice}, Has 100: {has100}");

            // Dongu
            Console.WriteLine("All students:");
            foreach (KeyValuePair<string, int> kvp in studentScores)
            {
                Console.WriteLine($"  {kvp.Key}: {kvp.Value}");
            }

            // Dictionary ozellikleri
            Console.WriteLine($"Total students: {studentScores.Count}");
            Console.WriteLine($"Keys:   {string.Join(", ", studentScores.Keys)}");
            Console.WriteLine($"Values: {string.Join(", ", studentScores.Values)}");

            // -----------------------------------------------
            // 6) QUEUE<T> - Kuyruk (FIFO: First In, First Out)
            // -----------------------------------------------
            Console.WriteLine("\n--- Queue<T> (FIFO) ---");

            Queue<string> printQueue = new Queue<string>();

            // Kuyruğa ekleme
            printQueue.Enqueue("Document1.pdf");
            printQueue.Enqueue("Photo.jpg");
            printQueue.Enqueue("Report.docx");

            Console.WriteLine($"Queue count: {printQueue.Count}");
            Console.WriteLine($"Next in line: {printQueue.Peek()}");   // Cikmadan bakar

            // Kuyruktan cikartma (ilk giren ilk cikar)
            while (printQueue.Count > 0)
            {
                string item = printQueue.Dequeue();
                Console.WriteLine($"  Printing: {item}");
            }

            // -----------------------------------------------
            // 7) STACK<T> - Yigin (LIFO: Last In, First Out)
            // -----------------------------------------------
            Console.WriteLine("\n--- Stack<T> (LIFO) ---");

            Stack<string> undoHistory = new Stack<string>();

            // Yigina ekleme
            undoHistory.Push("Type 'Hello'");
            undoHistory.Push("Delete word");
            undoHistory.Push("Type 'World'");

            Console.WriteLine($"Stack count: {undoHistory.Count}");
            Console.WriteLine($"Last action: {undoHistory.Peek()}");

            // Yigindan cikartma (son giren ilk cikar)
            Console.WriteLine("Undo operations:");
            while (undoHistory.Count > 0)
            {
                string action = undoHistory.Pop();
                Console.WriteLine($"  Undoing: {action}");
            }

            // -----------------------------------------------
            // 8) HASHSET<T> - Benzersiz Eleman Koleksiyonu
            // -----------------------------------------------
            Console.WriteLine("\n--- HashSet<T> ---");

            HashSet<string> uniqueTags = new HashSet<string>();

            uniqueTags.Add("csharp");
            uniqueTags.Add("dotnet");
            uniqueTags.Add("programming");
            uniqueTags.Add("csharp");          // Tekrar eklenemez, yok sayilir

            Console.WriteLine($"Unique tags: {uniqueTags.Count}");   // 3

            foreach (string tag in uniqueTags)
            {
                Console.Write($"#{tag} ");
            }
            Console.WriteLine();

            // Kume islemleri
            HashSet<string> otherTags = new HashSet<string> { "dotnet", "web", "api" };

            HashSet<string> union = new HashSet<string>(uniqueTags);
            union.UnionWith(otherTags);
            Console.Write("Union: ");
            foreach (var t in union) Console.Write($"{t} ");
            Console.WriteLine();

            // -----------------------------------------------
            // PRATIK SORULARI
            // -----------------------------------------------
            // 1. Bir int dizisi olusturun, en buyuk ve en kucuk elemani bulun
            // 2. List<string> ile bir alisveris listesi yapin (ekle, sil, guncelle)
            // 3. Dictionary ile bir telefon rehberi yapin (isim -> numara)
            // 4. Stack kullanarak bir string'i ters cevirin

            Console.WriteLine("\nLesson 06 completed! Press any key to exit...");
            Console.ReadKey();
        }
    }
}
