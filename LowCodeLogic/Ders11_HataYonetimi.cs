// =============================================================
// DERS 11 - HATA YONETIMI (Exception Handling)
// =============================================================
// Bu derste:
// - try-catch-finally bloklari
// - Exception tipleri
// - throw ile hata firlatma
// - Custom exception sinifi
// - Exception filtreleri (when)
// - using statement (IDisposable)
// =============================================================

using System;
using System.IO;
using System.Collections.Generic;

namespace LowCodeLogic
{
    // -----------------------------------------------
    // CUSTOM EXCEPTION (Ozel Hata Sinifi)
    // -----------------------------------------------
    class InsufficientFundsException : Exception
    {
        public decimal Balance { get; }
        public decimal RequestedAmount { get; }

        public InsufficientFundsException(decimal balance, decimal amount)
            : base($"Insufficient funds. Balance: {balance:C}, Requested: {amount:C}")
        {
            Balance = balance;
            RequestedAmount = amount;
        }
    }

    class InvalidAgeException : Exception
    {
        public int Age { get; }
        public InvalidAgeException(int age)
            : base($"Invalid age: {age}. Age must be between 0 and 150.") { Age = age; }
    }

    // -----------------------------------------------
    // Ornek sinif
    // -----------------------------------------------
    class SafeAccount
    {
        public string Owner { get; }
        public decimal Balance { get; private set; }

        public SafeAccount(string owner, decimal initial)
        {
            Owner = owner;
            Balance = initial;
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be positive.", nameof(amount));
            if (amount > Balance)
                throw new InsufficientFundsException(Balance, amount);

            Balance -= amount;
        }
    }

    class Lesson11_ExceptionHandling
    {
        // Hata firlatabilecek metot
        static int SafeDivide(int a, int b)
        {
            if (b == 0)
                throw new DivideByZeroException("Cannot divide by zero!");
            return a / b;
        }

        static void ValidateAge(int age)
        {
            if (age < 0 || age > 150)
                throw new InvalidAgeException(age);
            Console.WriteLine($"  Valid age: {age}");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("=== LESSON 11: Exception Handling ===\n");

            // -----------------------------------------------
            // 1) TEMEL TRY-CATCH
            // -----------------------------------------------
            Console.WriteLine("--- Basic try-catch ---");

            try
            {
                int result = 10 / 0;    // DivideByZeroException firlatir
                Console.WriteLine(result);
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine($"Error caught: {ex.Message}");
            }

            // -----------------------------------------------
            // 2) BIRDEN FAZLA CATCH BLOGU
            // -----------------------------------------------
            Console.WriteLine("\n--- Multiple catch blocks ---");

            try
            {
                string[] names = { "Alice", "Bob" };
                // names[5] = "Eve";     // IndexOutOfRangeException
                int number = int.Parse("abc");    // FormatException
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine($"Index error: {ex.Message}");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Format error: {ex.Message}");
            }
            catch (Exception ex)   // Genel catch - en sona yazilmali
            {
                Console.WriteLine($"General error: {ex.Message}");
            }

            // -----------------------------------------------
            // 3) FINALLY BLOGU
            // -----------------------------------------------
            // finally: Hata olsa da olmasa da MUTLAKA calisir
            // Kaynak temizligi (dosya kapatma, baglanti kesme) icin idealdir
            Console.WriteLine("\n--- finally block ---");

            try
            {
                Console.WriteLine("  Opening resource...");
                int result = SafeDivide(100, 5);
                Console.WriteLine($"  Result: {result}");
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine($"  Error: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("  Closing resource... (always runs)");
            }

            // -----------------------------------------------
            // 4) THROW - Hata Firlatma
            // -----------------------------------------------
            Console.WriteLine("\n--- throw ---");

            try
            {
                SafeDivide(10, 0);
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine($"  Caught: {ex.Message}");
            }

            // -----------------------------------------------
            // 5) CUSTOM EXCEPTION
            // -----------------------------------------------
            Console.WriteLine("\n--- Custom Exception ---");

            SafeAccount account = new SafeAccount("Alice", 500);

            try
            {
                account.Withdraw(200);
                Console.WriteLine($"  Balance: {account.Balance:C}");
                account.Withdraw(1000);   // InsufficientFundsException
            }
            catch (InsufficientFundsException ex)
            {
                Console.WriteLine($"  {ex.Message}");
                Console.WriteLine($"  Shortage: {ex.RequestedAmount - ex.Balance:C}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"  Invalid argument: {ex.Message}");
            }

            try
            {
                ValidateAge(200);
            }
            catch (InvalidAgeException ex)
            {
                Console.WriteLine($"  {ex.Message}");
            }

            // -----------------------------------------------
            // 6) EXCEPTION FILTRELERI (when)
            // -----------------------------------------------
            Console.WriteLine("\n--- Exception Filters (when) ---");

            int[] testValues = { 0, -1, 5 };
            foreach (int val in testValues)
            {
                try
                {
                    if (val == 0) throw new ArgumentException("Zero value");
                    if (val < 0) throw new ArgumentException("Negative value");
                    Console.WriteLine($"  Value {val} is OK");
                }
                catch (ArgumentException ex) when (ex.Message.Contains("Zero"))
                {
                    Console.WriteLine($"  Filtered catch (Zero): {ex.Message}");
                }
                catch (ArgumentException ex) when (ex.Message.Contains("Negative"))
                {
                    Console.WriteLine($"  Filtered catch (Negative): {ex.Message}");
                }
            }

            // -----------------------------------------------
            // 7) YAYGIN EXCEPTION TIPLERI
            // -----------------------------------------------
            /*
                NullReferenceException       -> null nesneye erisim
                IndexOutOfRangeException     -> gecersiz dizi indeksi
                ArgumentException            -> gecersiz parametre
                ArgumentNullException        -> null parametre
                InvalidOperationException    -> gecersiz islem durumu
                FormatException              -> gecersiz format (Parse hatasi)
                DivideByZeroException        -> sifira bolme
                StackOverflowException       -> sonsuz recursion
                OutOfMemoryException         -> bellek yetersiz
                FileNotFoundException        -> dosya bulunamadi
                IOException                  -> genel I/O hatasi
            */

            // -----------------------------------------------
            // 8) USING STATEMENT (IDisposable)
            // -----------------------------------------------
            Console.WriteLine("\n--- using statement ---");

            // using: IDisposable nesnelerini otomatik temizler
            // try-finally'nin kisa hali

            // using declaration (C# 8+)
            try
            {
                using var reader = new StringReader("Hello\nWorld\nFrom\nC#");
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Console.WriteLine($"  Read: {line}");
                }
                // reader otomatik dispose edilir
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  Error: {ex.Message}");
            }

            // -----------------------------------------------
            // PRATIK SORULARI
            // -----------------------------------------------
            // 1. Kullanicidan iki sayi isteyin, bolme islemi yapin (sifira bolme kontrolu)
            // 2. PasswordTooWeakException ozel exception sinifi olusturun
            // 3. Bir dizi erisimini try-catch ile guvenli hale getirin
            // 4. finally blogunda kaynak temizligi ornegi yapin

            Console.WriteLine("\nLesson 11 completed! Press any key to exit...");
            Console.ReadKey();
        }
    }
}
