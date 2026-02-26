// =============================================================
// DERS 01 - C# GIRIS (Introduction to C#)
// =============================================================
// Bu derste C# dilinin temel yapisini ogreneceksiniz.
// - Program yapisi (namespace, class, Main metodu)
// - Console'a yazdir ve oku
// - Yorum satirlari
// - Temel string islemleri
// =============================================================

using System;

namespace LowCodeLogic
{
    class Lesson01_Introduction
    {
        static void Main(string[] args)
        {
            // -----------------------------------------------
            // 1) HELLO WORLD - Ilk Programiniz
            // -----------------------------------------------
            // Console.WriteLine() ekrana yazi yazdirmak icin kullanilir.
            // Her C# programi bir Main() metodu ile baslar.

            Console.WriteLine("Hello World!");
            Console.WriteLine("Welcome to C# Programming!");

            // -----------------------------------------------
            // 2) CONSOLE.WRITE vs CONSOLE.WRITELINE
            // -----------------------------------------------
            // WriteLine -> yazdirir ve alt satira gecer
            // Write     -> yazdirir ama ayni satirda kalir

            Console.Write("Name: ");
            Console.WriteLine("Emre");
            // Cikti: "Name: Emre" (ayni satirda)

            Console.WriteLine("Line 1");
            Console.WriteLine("Line 2");
            // Cikti: 
            // Line 1
            // Line 2

            // -----------------------------------------------
            // 3) YORUM SATIRLARI (Comments)
            // -----------------------------------------------
            // Tek satirlik yorum: // ile baslar
            
            /* 
               Cok satirlik yorum:
               Slash-yildiz ile baslar,
               yildiz-slash ile biter.
            */

            /// <summary>
            /// XML dokumantasyon yorumu - genelde metot/sinif aciklamalarinda kullanilir
            /// </summary>

            // -----------------------------------------------
            // 4) STRING BIRLESTIRME (String Concatenation)
            // -----------------------------------------------
            string firstName = "John";
            string lastName = "Doe";

            // Yontem 1: + operatoru ile birlestirme
            Console.WriteLine("Full Name: " + firstName + " " + lastName);

            // Yontem 2: String Interpolation ($ isareti ile)
            // En modern ve okunak yontem
            Console.WriteLine($"Full Name: {firstName} {lastName}");

            // Yontem 3: String.Format
            Console.WriteLine(string.Format("Full Name: {0} {1}", firstName, lastName));

            // -----------------------------------------------
            // 5) KULLANICIDAN GIRDI ALMA (Reading Input)
            // -----------------------------------------------
            // Console.ReadLine() kullanicidan metin okur (string doner)

            Console.Write("Enter your name: ");
            string userName = Console.ReadLine();
            Console.WriteLine($"Hello, {userName}! Welcome to C#.");

            // -----------------------------------------------
            // 6) ESCAPE KARAKTERLERI (Escape Characters)
            // -----------------------------------------------
            // \n -> Yeni satir
            // \t -> Tab boslugu
            // \\ -> Ters slash
            // \" -> Ciift tirnak

            Console.WriteLine("First Line\nSecond Line");
            Console.WriteLine("Column1\tColumn2\tColumn3");
            Console.WriteLine("File path: C:\\Users\\Documents");
            Console.WriteLine("He said: \"Hello!\"");

            // Verbatim string: @ isareti ile escape'lere gerek kalmaz
            Console.WriteLine(@"File path: C:\Users\Documents");

            // -----------------------------------------------
            // 7) PROGRAM YAPISI OZETI
            // -----------------------------------------------
            /*
                using System;                    // Kutuphane ekleme (import)
                namespace LowCodeLogic            // Ad alani (proje organizasyonu)
                {
                    class Lesson01_Introduction   // Sinif tanimlamasi
                    {
                        static void Main(string[] args)  // Giris noktasi
                        {
                            // Kodlar buraya yazilir
                        }
                    }
                }
            */

            // -----------------------------------------------
            // PRATIK: Asagidaki bilgileri ekrana yazdirin
            // -----------------------------------------------
            // 1. Kendi adinizi ve soyadinizi string interpolation ile yazdirin
            // 2. Kullanicidan yasini isteyin ve "You are X years old" yazdirin
            // 3. Bir dosya yolu yazdirin (escape veya verbatim string ile)

            Console.WriteLine("\n--- PRACTICE ---");
            Console.Write("Enter your age: ");
            string ageInput = Console.ReadLine();
            Console.WriteLine($"You are {ageInput} years old.");

            Console.WriteLine("\nLesson 01 completed! Press any key to exit...");
            Console.ReadKey();
        }
    }
}
