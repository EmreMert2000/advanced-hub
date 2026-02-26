// =============================================================
// DERS 07 - METOTLAR / FONKSIYONLAR (Methods / Functions)
// =============================================================
// Bu derste:
// - Metot tanimlama ve cagirma
// - Parametreler ve donus tipleri
// - ref, out, params anahtar kelimeleri
// - Method overloading (asiri yukleme)
// - Optional ve named parametreler
// - Recursive metotlar (ozzyineleme)
// - Local functions
// =============================================================

using System;

namespace LowCodeLogic
{
    class Lesson07_Methods
    {
        // -----------------------------------------------
        // METOT TANIMLAMA
        // -----------------------------------------------
        // erisim_belirleyici donus_tipi MetotAdi(parametreler)
        // {
        //     // kod blogu
        //     return deger;  // void ise return gerekmez
        // }

        // -----------------------------------------------
        // 1) BASIT METOTLAR
        // -----------------------------------------------

        // Parametresiz, donus degersiz (void) metot
        static void SayHello()
        {
            Console.WriteLine("Hello from a method!");
        }

        // Parametreli metot
        static void Greet(string name)
        {
            Console.WriteLine($"Hello, {name}!");
        }

        // Donus degerli metot
        static int Add(int a, int b)
        {
            return a + b;
        }

        // Expression-bodied metot (tek satirlik)
        static double Multiply(double a, double b) => a * b;

        static bool IsEven(int number) => number % 2 == 0;

        // -----------------------------------------------
        // 2) BIRDEN FAZLA PARAMETRE VE DONUS
        // -----------------------------------------------

        // Birden fazla donus degeri: Tuple kullanarak
        static (double min, double max, double avg) GetStatistics(int[] numbers)
        {
            double min = numbers[0];
            double max = numbers[0];
            double sum = 0;

            foreach (int n in numbers)
            {
                if (n < min) min = n;
                if (n > max) max = n;
                sum += n;
            }

            return (min, max, sum / numbers.Length);
        }

        // -----------------------------------------------
        // 3) REF VE OUT PARAMETRELERI
        // -----------------------------------------------

        // ref: Degiskeni referans olarak gonderir (disi degisir)
        static void DoubleValue(ref int value)
        {
            value *= 2;
        }

        // out: Metot icinde deger atanmak ZORUNDA, disari deger dondurur
        static void Divide(int a, int b, out int quotient, out int remainder)
        {
            quotient = a / b;
            remainder = a % b;
        }

        // -----------------------------------------------
        // 4) PARAMS - Degisken Sayida Parametre
        // -----------------------------------------------

        // params: Istenen sayida parametre alabilir
        static int Sum(params int[] numbers)
        {
            int total = 0;
            foreach (int n in numbers)
            {
                total += n;
            }
            return total;
        }

        // -----------------------------------------------
        // 5) METHOD OVERLOADING (Asiri Yukleme)
        // -----------------------------------------------
        // Ayni isimde ama farkli parametre listesiyle birden fazla metot

        static double CalculateArea(double radius)
        {
            // Daire alani
            return Math.PI * radius * radius;
        }

        static double CalculateArea(double width, double height)
        {
            // Dikdortgen alani
            return width * height;
        }

        static double CalculateArea(double a, double b, double c)
        {
            // Ucgen alani (Heron formulu)
            double s = (a + b + c) / 2;
            return Math.Sqrt(s * (s - a) * (s - b) * (s - c));
        }

        // -----------------------------------------------
        // 6) OPTIONAL VE NAMED PARAMETRELER
        // -----------------------------------------------

        // Optional: Varsayilan deger atanmis parametreler
        static void PrintMessage(string message, int repeatCount = 1, string prefix = ">>")
        {
            for (int i = 0; i < repeatCount; i++)
            {
                Console.WriteLine($"{prefix} {message}");
            }
        }

        // -----------------------------------------------
        // 7) RECURSIVE (Ozyinelemeli) METOTLAR
        // -----------------------------------------------

        // Faktoriyel hesaplama: n! = n * (n-1) * ... * 1
        static long Factorial(int n)
        {
            if (n <= 1) return 1;          // Base case (durma kosulu)
            return n * Factorial(n - 1);   // Recursive case
        }

        // Fibonacci sayisi
        static int Fibonacci(int n)
        {
            if (n <= 0) return 0;
            if (n == 1) return 1;
            return Fibonacci(n - 1) + Fibonacci(n - 2);
        }

        // -----------------------------------------------
        // MAIN METODU
        // -----------------------------------------------
        static void Main(string[] args)
        {
            Console.WriteLine("=== LESSON 07: Methods ===\n");

            // 1) Basit metot cagirilari
            Console.WriteLine("--- Basic Methods ---");
            SayHello();
            Greet("Alice");
            Greet("Bob");

            int sum = Add(15, 25);
            Console.WriteLine($"Add(15, 25) = {sum}");

            double product = Multiply(3.5, 4.0);
            Console.WriteLine($"Multiply(3.5, 4.0) = {product}");

            Console.WriteLine($"IsEven(7) = {IsEven(7)}");
            Console.WriteLine($"IsEven(8) = {IsEven(8)}");

            // 2) Tuple donus
            Console.WriteLine("\n--- Tuple Return ---");
            int[] data = { 45, 67, 23, 89, 12, 56 };
            var stats = GetStatistics(data);
            Console.WriteLine($"Min: {stats.min}, Max: {stats.max}, Avg: {stats.avg:F1}");

            // 3) ref ve out
            Console.WriteLine("\n--- ref & out ---");

            int myValue = 10;
            Console.WriteLine($"Before ref: {myValue}");
            DoubleValue(ref myValue);
            Console.WriteLine($"After ref:  {myValue}");    // 20

            Divide(17, 5, out int quot, out int rem);
            Console.WriteLine($"17 / 5 = {quot} remainder {rem}");

            // 4) params
            Console.WriteLine("\n--- params ---");
            Console.WriteLine($"Sum(1,2,3) = {Sum(1, 2, 3)}");
            Console.WriteLine($"Sum(10,20,30,40,50) = {Sum(10, 20, 30, 40, 50)}");

            // 5) Overloading
            Console.WriteLine("\n--- Method Overloading ---");
            Console.WriteLine($"Circle area (r=5):       {CalculateArea(5.0):F2}");
            Console.WriteLine($"Rectangle area (4x6):    {CalculateArea(4.0, 6.0):F2}");
            Console.WriteLine($"Triangle area (3,4,5):   {CalculateArea(3.0, 4.0, 5.0):F2}");

            // 6) Optional ve Named parametreler
            Console.WriteLine("\n--- Optional & Named Parameters ---");
            PrintMessage("Hello");                              // Varsayilan degerlerle
            PrintMessage("Warning", 2);                         // repeatCount = 2
            PrintMessage("Error", prefix: "!!", repeatCount: 3); // Named parametreler

            // 7) Recursive metotlar
            Console.WriteLine("\n--- Recursion ---");
            Console.WriteLine($"5! = {Factorial(5)}");          // 120
            Console.WriteLine($"10! = {Factorial(10)}");        // 3628800

            Console.Write("Fibonacci(0-10): ");
            for (int i = 0; i <= 10; i++)
            {
                Console.Write($"{Fibonacci(i)} ");
            }
            Console.WriteLine();

            // 8) Local Functions (C# 7+)
            Console.WriteLine("\n--- Local Functions ---");

            // Metot icinde metot tanimlanabilir
            int Power(int baseNum, int exponent)
            {
                int result = 1;
                for (int i = 0; i < exponent; i++)
                    result *= baseNum;
                return result;
            }

            Console.WriteLine($"2^10 = {Power(2, 10)}");
            Console.WriteLine($"3^5 = {Power(3, 5)}");

            // -----------------------------------------------
            // PRATIK SORULARI
            // -----------------------------------------------
            // 1. Bir string'in palindrome olup olmadigini kontrol eden metot yazin
            // 2. Verilen bir int dizisinin ortalamasini hesaplayan metot yazin
            // 3. BMI hesaplayan metot yazin (boy ve kilo parametreleri ile)
            // 4. Recursive olarak bir sayinin kuvvetini hesaplayan metot yazin

            Console.WriteLine("\nLesson 07 completed! Press any key to exit...");
            Console.ReadKey();
        }
    }
}
