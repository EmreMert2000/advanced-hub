// =============================================================
// DERS 05 - DONGULER (Loops)
// =============================================================
// Bu derste:
// - for dongusu
// - while dongusu
// - do-while dongusu
// - foreach dongusu
// - break, continue anahtar kelimeleri
// - Ic ice donguler (Nested loops)
// =============================================================

using System;

namespace LowCodeLogic
{
    class Lesson05_Loops
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== LESSON 05: Loops ===\n");

            // -----------------------------------------------
            // 1) FOR DONGUSU
            // -----------------------------------------------
            // for (baslangic; kosul; artis) { ... }
            // Kac kez donecegi belli oldugunda idealdir

            Console.WriteLine("--- For Loop ---");

            // 1'den 10'a kadar yazdirma
            for (int i = 1; i <= 10; i++)
            {
                Console.Write($"{i} ");
            }
            Console.WriteLine();   // Yeni satir

            // Geriye dogru sayma
            Console.Write("Countdown: ");
            for (int i = 5; i >= 1; i--)
            {
                Console.Write($"{i} ");
            }
            Console.WriteLine("GO!");

            // Ikisel artis
            Console.Write("Even numbers: ");
            for (int i = 0; i <= 20; i += 2)
            {
                Console.Write($"{i} ");
            }
            Console.WriteLine();

            // -----------------------------------------------
            // 2) WHILE DONGUSU
            // -----------------------------------------------
            // while (kosul) { ... }
            // Kosul true oldugu surece calisir
            // Kac kez donecegi onceden belli olmayabilir

            Console.WriteLine("\n--- While Loop ---");

            int count = 1;
            while (count <= 5)
            {
                Console.WriteLine($"Count: {count}");
                count++;
            }

            // Bir sayiyi 2'ye bolerek 1'e inme
            Console.Write("Halving: ");
            int number = 256;
            while (number >= 1)
            {
                Console.Write($"{number} ");
                number /= 2;
            }
            Console.WriteLine();

            // -----------------------------------------------
            // 3) DO-WHILE DONGUSU
            // -----------------------------------------------
            // do { ... } while (kosul);
            // En az bir kez calisir, sonra kosulu kontrol eder
            // Menu sistemlerinde sik kullanilir

            Console.WriteLine("\n--- Do-While Loop ---");

            int attempt = 0;
            do
            {
                attempt++;
                Console.WriteLine($"Attempt #{attempt}");
            } while (attempt < 3);

            // -----------------------------------------------
            // 4) FOREACH DONGUSU
            // -----------------------------------------------
            // foreach (tip eleman in koleksiyon) { ... }
            // Diziler ve koleksiyonlar uzerinde gezmek icin idealdir
            // Index gerekmiyorsa foreach tercih edin

            Console.WriteLine("\n--- Foreach Loop ---");

            string[] fruits = { "Apple", "Banana", "Cherry", "Date", "Elderberry" };

            foreach (string fruit in fruits)
            {
                Console.WriteLine($"  Fruit: {fruit}");
            }

            // String uzerinde foreach (her karakter icin)
            string word = "HELLO";
            Console.Write("Characters: ");
            foreach (char c in word)
            {
                Console.Write($"{c}-");
            }
            Console.WriteLine();

            // -----------------------------------------------
            // 5) BREAK VE CONTINUE
            // -----------------------------------------------
            Console.WriteLine("\n--- Break & Continue ---");

            // break: Donguyu tamamen sonlandirir
            Console.Write("Break at 5: ");
            for (int i = 1; i <= 10; i++)
            {
                if (i == 5) break;
                Console.Write($"{i} ");
            }
            Console.WriteLine();

            // continue: Mevcut turu atlar, sonraki tura gecer
            Console.Write("Skip odds: ");
            for (int i = 1; i <= 10; i++)
            {
                if (i % 2 != 0) continue;   // Tek sayilari atla
                Console.Write($"{i} ");
            }
            Console.WriteLine();

            // -----------------------------------------------
            // 6) IC ICE DONGULER (Nested Loops)
            // -----------------------------------------------
            Console.WriteLine("\n--- Nested Loops ---");

            // Carpim tablosu (1-5)
            Console.WriteLine("Multiplication Table (1-5):");
            for (int i = 1; i <= 5; i++)
            {
                for (int j = 1; j <= 5; j++)
                {
                    Console.Write($"{i * j,4}");     // ,4 = 4 karakter genislik
                }
                Console.WriteLine();
            }

            // Yildiz deseni (ucgen)
            Console.WriteLine("\nStar Pattern:");
            for (int row = 1; row <= 5; row++)
            {
                for (int col = 1; col <= row; col++)
                {
                    Console.Write("* ");
                }
                Console.WriteLine();
            }

            // -----------------------------------------------
            // 7) SONSUZ DONGU VE DIKKAT EDILECEKLER
            // -----------------------------------------------
            Console.WriteLine("\n--- Infinite Loop (controlled) ---");

            // Sonsuz dongu: while(true) veya for(;;)
            // Genellikle icerideki break ile kontrol edilir

            int maxIterations = 5;
            int iteration = 0;

            while (true)
            {
                iteration++;
                Console.WriteLine($"Iteration: {iteration}");

                if (iteration >= maxIterations)
                {
                    Console.WriteLine("Breaking out of infinite loop.");
                    break;
                }
            }

            // -----------------------------------------------
            // 8) GERCEK DUNYA ORNEGI: Basit Menu Sistemi
            // -----------------------------------------------
            Console.WriteLine("\n--- Menu System Example ---");

            bool running = true;
            int menuChoice = 0;

            // Gercek uygulamada Console.ReadLine() ile alinir
            // Burada simule ediyoruz
            int[] simulatedInputs = { 1, 2, 3, 4 };
            int inputIndex = 0;

            while (running && inputIndex < simulatedInputs.Length)
            {
                Console.WriteLine("\n[1] New Game");
                Console.WriteLine("[2] Load Game");
                Console.WriteLine("[3] Settings");
                Console.WriteLine("[4] Exit");

                menuChoice = simulatedInputs[inputIndex];
                inputIndex++;
                Console.WriteLine($"Selected: {menuChoice}");

                switch (menuChoice)
                {
                    case 1:
                        Console.WriteLine("Starting new game...");
                        break;
                    case 2:
                        Console.WriteLine("Loading saved game...");
                        break;
                    case 3:
                        Console.WriteLine("Opening settings...");
                        break;
                    case 4:
                        Console.WriteLine("Exiting...");
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }

            // -----------------------------------------------
            // 9) GERCEK DUNYA ORNEGI: FizzBuzz
            // -----------------------------------------------
            // Klasik programlama problemi
            Console.WriteLine("\n--- FizzBuzz (1-30) ---");

            for (int i = 1; i <= 30; i++)
            {
                if (i % 15 == 0)            // Hem 3'e hem 5'e bolunuyorsa
                    Console.Write("FizzBuzz ");
                else if (i % 3 == 0)        // Sadece 3'e bolunuyorsa
                    Console.Write("Fizz ");
                else if (i % 5 == 0)        // Sadece 5'e bolunuyorsa
                    Console.Write("Buzz ");
                else
                    Console.Write($"{i} ");
            }
            Console.WriteLine();

            // -----------------------------------------------
            // PRATIK SORULARI
            // -----------------------------------------------
            // 1. 1'den 100'e kadar sayilarin toplamini for dongusu ile bulun
            // 2. Bir sayinin faktoryelini while dongusu ile hesaplayin
            // 3. Kullanicidan surekli sayi isteyin, 0 girene kadar toplami hesaplayin (do-while)
            // 4. Bir string'deki sesli harf sayisini foreach ile bulun

            Console.WriteLine("\nLesson 05 completed! Press any key to exit...");
            Console.ReadKey();
        }
    }
}
