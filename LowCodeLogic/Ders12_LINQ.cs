// =============================================================
// DERS 12 - LINQ TEMELLERI (Language Integrated Query)
// =============================================================
// Bu derste:
// - LINQ nedir ve neden kullanilir
// - Where, Select, OrderBy, GroupBy
// - First, Last, Single, Any, All
// - Aggregate, Sum, Count, Average, Min, Max
// - Method syntax vs Query syntax
// - SelectMany, Distinct, Take, Skip
// =============================================================

using System;
using System.Collections.Generic;
using System.Linq;

namespace LowCodeLogic
{
    // Ornek veri sinifi
    class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Department { get; set; }
        public double GPA { get; set; }
        public List<int> Grades { get; set; }

        public override string ToString() =>
            $"{Name} (Age:{Age}, Dept:{Department}, GPA:{GPA:F1})";
    }

    class Lesson12_LINQ
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== LESSON 12: LINQ ===\n");

            // Ornek veri
            List<Student> students = new List<Student>
            {
                new Student { Id=1, Name="Alice",   Age=22, Department="CS",    GPA=3.8, Grades=new List<int>{90,85,92}},
                new Student { Id=2, Name="Bob",     Age=20, Department="EE",    GPA=3.2, Grades=new List<int>{78,82,75}},
                new Student { Id=3, Name="Charlie", Age=23, Department="CS",    GPA=3.5, Grades=new List<int>{88,90,85}},
                new Student { Id=4, Name="Diana",   Age=21, Department="Math",  GPA=3.9, Grades=new List<int>{95,92,98}},
                new Student { Id=5, Name="Eve",     Age=22, Department="CS",    GPA=2.8, Grades=new List<int>{70,65,72}},
                new Student { Id=6, Name="Frank",   Age=24, Department="EE",    GPA=3.1, Grades=new List<int>{80,75,78}},
                new Student { Id=7, Name="Grace",   Age=20, Department="Math",  GPA=3.7, Grades=new List<int>{88,92,90}},
            };

            int[] numbers = { 5, 3, 8, 1, 9, 2, 7, 4, 6, 10, 3, 5 };

            // -----------------------------------------------
            // 1) WHERE - Filtreleme
            // -----------------------------------------------
            Console.WriteLine("--- Where (Filter) ---");

            // Method syntax
            var csStudents = students.Where(s => s.Department == "CS");
            Console.WriteLine("CS Students:");
            foreach (var s in csStudents) Console.WriteLine($"  {s}");

            // Query syntax (SQL benzeri)
            var highGPA = from s in students
                          where s.GPA >= 3.5
                          select s;
            Console.WriteLine("\nHigh GPA (>=3.5):");
            foreach (var s in highGPA) Console.WriteLine($"  {s}");

            // Birden fazla kosul
            var filtered = students.Where(s => s.Age >= 21 && s.GPA >= 3.5);
            Console.WriteLine("\nAge>=21 AND GPA>=3.5:");
            foreach (var s in filtered) Console.WriteLine($"  {s}");

            // -----------------------------------------------
            // 2) SELECT - Donusum (Projection)
            // -----------------------------------------------
            Console.WriteLine("\n--- Select (Projection) ---");

            // Sadece isimleri sec
            var names = students.Select(s => s.Name);
            Console.WriteLine($"Names: {string.Join(", ", names)}");

            // Anonim tip olusturma
            var summaries = students.Select(s => new { s.Name, s.GPA, Status = s.GPA >= 3.5 ? "Honor" : "Regular" });
            foreach (var s in summaries) Console.WriteLine($"  {s.Name}: {s.GPA:F1} ({s.Status})");

            // -----------------------------------------------
            // 3) ORDERBY - Siralama
            // -----------------------------------------------
            Console.WriteLine("\n--- OrderBy (Sorting) ---");

            var byGPA = students.OrderByDescending(s => s.GPA);
            Console.WriteLine("By GPA (desc):");
            foreach (var s in byGPA) Console.WriteLine($"  {s}");

            // Coklu siralama
            var multiSort = students.OrderBy(s => s.Department).ThenByDescending(s => s.GPA);
            Console.WriteLine("\nBy Dept then GPA:");
            foreach (var s in multiSort) Console.WriteLine($"  {s}");

            // -----------------------------------------------
            // 4) GROUPBY - Gruplama
            // -----------------------------------------------
            Console.WriteLine("\n--- GroupBy ---");

            var byDepartment = students.GroupBy(s => s.Department);
            foreach (var group in byDepartment)
            {
                Console.WriteLine($"\n  {group.Key} ({group.Count()} students):");
                foreach (var s in group)
                    Console.WriteLine($"    {s.Name} - GPA: {s.GPA:F1}");
                Console.WriteLine($"    Avg GPA: {group.Average(s => s.GPA):F2}");
            }

            // -----------------------------------------------
            // 5) AGGREGATE FONKSIYONLARI
            // -----------------------------------------------
            Console.WriteLine("\n--- Aggregates ---");

            Console.WriteLine($"Count:   {students.Count}");
            Console.WriteLine($"Avg GPA: {students.Average(s => s.GPA):F2}");
            Console.WriteLine($"Max GPA: {students.Max(s => s.GPA):F1}");
            Console.WriteLine($"Min GPA: {students.Min(s => s.GPA):F1}");
            Console.WriteLine($"Sum Ages:{students.Sum(s => s.Age)}");

            // Sayilar uzerinde
            Console.WriteLine($"\nNumbers Sum: {numbers.Sum()}");
            Console.WriteLine($"Numbers Avg: {numbers.Average():F1}");

            // -----------------------------------------------
            // 6) FIRST, LAST, SINGLE, ANY, ALL
            // -----------------------------------------------
            Console.WriteLine("\n--- Element Operations ---");

            var first = students.First();
            var firstCS = students.First(s => s.Department == "CS");
            var lastStudent = students.Last();
            Console.WriteLine($"First:    {first.Name}");
            Console.WriteLine($"First CS: {firstCS.Name}");
            Console.WriteLine($"Last:     {lastStudent.Name}");

            // FirstOrDefault: bulamazsa null doner (exception firlatmaz)
            var notFound = students.FirstOrDefault(s => s.Name == "Zara");
            Console.WriteLine($"NotFound: {notFound?.Name ?? "null"}");

            // Any ve All
            bool anyHighGPA = students.Any(s => s.GPA >= 3.9);
            bool allAdults = students.All(s => s.Age >= 18);
            Console.WriteLine($"Any GPA>=3.9: {anyHighGPA}");
            Console.WriteLine($"All adults:   {allAdults}");

            // -----------------------------------------------
            // 7) TAKE, SKIP, DISTINCT
            // -----------------------------------------------
            Console.WriteLine("\n--- Take, Skip, Distinct ---");

            var top3 = students.OrderByDescending(s => s.GPA).Take(3);
            Console.WriteLine("Top 3 GPA:");
            foreach (var s in top3) Console.WriteLine($"  {s}");

            var skip2 = students.Skip(2).Take(3);
            Console.WriteLine("\nSkip 2, Take 3:");
            foreach (var s in skip2) Console.WriteLine($"  {s.Name}");

            var uniqueNumbers = numbers.Distinct().OrderBy(n => n);
            Console.WriteLine($"\nDistinct: {string.Join(", ", uniqueNumbers)}");

            // -----------------------------------------------
            // 8) SELECTMANY - Ic ice koleksiyonlari duzlestir
            // -----------------------------------------------
            Console.WriteLine("\n--- SelectMany ---");

            var allGrades = students.SelectMany(s => s.Grades);
            Console.WriteLine($"All grades: {string.Join(", ", allGrades)}");
            Console.WriteLine($"Overall avg: {allGrades.Average():F1}");

            // -----------------------------------------------
            // 9) METHOD vs QUERY SYNTAX KARSILASTIRMA
            // -----------------------------------------------
            Console.WriteLine("\n--- Method vs Query Syntax ---");

            // Method syntax
            var resultMethod = students
                .Where(s => s.GPA >= 3.5)
                .OrderBy(s => s.Name)
                .Select(s => $"{s.Name}: {s.GPA:F1}");

            // Query syntax (ayni sonuc)
            var resultQuery = from s in students
                              where s.GPA >= 3.5
                              orderby s.Name
                              select $"{s.Name}: {s.GPA:F1}";

            Console.WriteLine("Method syntax:");
            foreach (var r in resultMethod) Console.WriteLine($"  {r}");

            // -----------------------------------------------
            // PRATIK
            // -----------------------------------------------
            // 1. En yuksek GPA'ya sahip ogrenciyi bulun
            // 2. Her departmandaki ortalama yasi hesaplayin
            // 3. GPA'ya gore ogrencileri harf notuna cevirin (A/B/C/D/F)
            // 4. Tum notlarin medyanini bulun

            Console.WriteLine("\nLesson 12 completed! Press any key to exit...");
            Console.ReadKey();
        }
    }
}
