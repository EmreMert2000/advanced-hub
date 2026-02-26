// =============================================================
// DERS 03 - OPERATORLER VE TIP DONUSUMLERI (Operators & Type Conversions)
// =============================================================
// Bu derste:
// - Aritmetik operatorler (+, -, *, /, %)
// - Karsilastirma operatorleri (==, !=, <, >, <=, >=)
// - Mantiksal operatorler (&&, ||, !)
// - Atama operatorleri (=, +=, -=, *=, /=)
// - Artirma/Azaltma (++, --)
// - Tip donusumleri (casting, Parse, Convert)
// =============================================================

using System;

namespace LowCodeLogic
{
    class Lesson03_Operators
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== LESSON 03: Operators & Type Conversions ===\n");

            // -----------------------------------------------
            // 1) ARITMETIK OPERATORLER (Arithmetic Operators)
            // -----------------------------------------------
            int a = 17;
            int b = 5;

            Console.WriteLine("--- Arithmetic Operators ---");
            Console.WriteLine($"{a} + {b} = {a + b}");     // Toplama: 22
            Console.WriteLine($"{a} - {b} = {a - b}");     // Cikarma: 12
            Console.WriteLine($"{a} * {b} = {a * b}");     // Carpma: 85
            Console.WriteLine($"{a} / {b} = {a / b}");     // Bolme: 3 (tam sayi bolmesi!)
            Console.WriteLine($"{a} % {b} = {a % b}");     // Mod (kalan): 2

            // DIKKAT: int / int = int (ondalik kisim kaybolur!)
            // Ondalikli sonuc icin en az bir taraf double/float olmali
            double preciseResult = (double)a / b;
            Console.WriteLine($"{a} / {b} (precise) = {preciseResult}");  // 3.4

            // -----------------------------------------------
            // 2) ARTIRMA VE AZALTMA (Increment & Decrement)
            // -----------------------------------------------
            Console.WriteLine("\n--- Increment & Decrement ---");

            int counter = 10;
            Console.WriteLine($"Initial:  {counter}");      // 10

            counter++;                                        // Sonradan artir
            Console.WriteLine($"After ++: {counter}");       // 11

            counter--;                                        // Sonradan azalt
            Console.WriteLine($"After --: {counter}");       // 10

            // Prefix vs Postfix farki
            int x = 5;
            int resultA = ++x;   // Once artir, sonra ata -> x=6, resultA=6
            Console.WriteLine($"++x: x={x}, result={resultA}");

            int y = 5;
            int resultB = y++;   // Once ata, sonra artir -> resultB=5, y=6
            Console.WriteLine($"y++: y={y}, result={resultB}");

            // -----------------------------------------------
            // 3) ATAMA OPERATORLERI (Assignment Operators)
            // -----------------------------------------------
            Console.WriteLine("\n--- Assignment Operators ---");

            int score = 100;
            Console.WriteLine($"Initial:  {score}");

            score += 10;    // score = score + 10
            Console.WriteLine($"+= 10:    {score}");    // 110

            score -= 20;    // score = score - 20
            Console.WriteLine($"-= 20:    {score}");    // 90

            score *= 2;     // score = score * 2
            Console.WriteLine($"*= 2:     {score}");    // 180

            score /= 3;     // score = score / 3
            Console.WriteLine($"/= 3:     {score}");    // 60

            score %= 7;     // score = score % 7
            Console.WriteLine($"%= 7:     {score}");    // 4

            // -----------------------------------------------
            // 4) KARSILASTIRMA OPERATORLERI (Comparison Operators)
            // -----------------------------------------------
            // Sonuc her zaman bool (true/false) doner
            Console.WriteLine("\n--- Comparison Operators ---");

            int p = 10, q = 20;

            Console.WriteLine($"{p} == {q}: {p == q}");   // Esit mi? false
            Console.WriteLine($"{p} != {q}: {p != q}");   // Esit degil mi? true
            Console.WriteLine($"{p} <  {q}: {p < q}");    // Kucuk mu? true
            Console.WriteLine($"{p} >  {q}: {p > q}");    // Buyuk mu? false
            Console.WriteLine($"{p} <= {q}: {p <= q}");   // Kucuk esit mi? true
            Console.WriteLine($"{p} >= {q}: {p >= q}");   // Buyuk esit mi? false

            // -----------------------------------------------
            // 5) MANTIKSAL OPERATORLER (Logical Operators)
            // -----------------------------------------------
            Console.WriteLine("\n--- Logical Operators ---");

            bool isAdult = true;
            bool hasLicense = false;

            // && (AND): Her ikisi de true olmali
            Console.WriteLine($"isAdult && hasLicense: {isAdult && hasLicense}");   // false

            // || (OR): En az biri true olmali
            Console.WriteLine($"isAdult || hasLicense: {isAdult || hasLicense}");   // true

            // ! (NOT): Tersini alir
            Console.WriteLine($"!isAdult: {!isAdult}");                             // false
            Console.WriteLine($"!hasLicense: {!hasLicense}");                       // true

            // Birlesik ornek
            int userAge = 25;
            bool canDrive = userAge >= 18 && hasLicense;
            bool needsSupervision = userAge < 18 || !hasLicense;
            Console.WriteLine($"Can drive: {canDrive}");
            Console.WriteLine($"Needs supervision: {needsSupervision}");

            // -----------------------------------------------
            // 6) TERNARY OPERATOR (? :)
            // -----------------------------------------------
            // kosul ? deger_eger_true : deger_eger_false
            Console.WriteLine("\n--- Ternary Operator ---");

            int playerAge = 15;
            string status = playerAge >= 18 ? "Adult" : "Minor";
            Console.WriteLine($"Age {playerAge}: {status}");   // Minor

            int number = -5;
            string sign = number > 0 ? "Positive" : (number < 0 ? "Negative" : "Zero");
            Console.WriteLine($"Number {number}: {sign}");     // Negative

            // -----------------------------------------------
            // 7) TIP DONUSUMLERI (Type Conversions / Casting)
            // -----------------------------------------------
            Console.WriteLine("\n--- Type Conversions ---");

            // a) Otomatik (Implicit) Donusum: Kucuk tip -> Buyuk tip (guvenli)
            int smallValue = 42;
            long bigValue = smallValue;       // int -> long (otomatik)
            double doubleValue = smallValue;  // int -> double (otomatik)
            Console.WriteLine($"int -> long:   {bigValue}");
            Console.WriteLine($"int -> double: {doubleValue}");

            // b) Acik (Explicit) Donusum / Casting: Buyuk tip -> Kucuk tip (veri kaybi riski)
            double salary = 75_500.99;
            int roundedSalary = (int)salary;              // Ondalik kisim KAYBOLUR
            Console.WriteLine($"double -> int: {salary} -> {roundedSalary}");

            long hugeNumber = 300;
            byte tinyNumber = (byte)hugeNumber;           // Tasma riski!
            Console.WriteLine($"long -> byte: {hugeNumber} -> {tinyNumber}");

            // c) String -> Sayi Donusumu: Parse metodu
            string numberText = "123";
            int parsedNumber = int.Parse(numberText);
            double parsedDouble = double.Parse("45.67");
            Console.WriteLine($"Parse string -> int:    {parsedNumber}");
            Console.WriteLine($"Parse string -> double: {parsedDouble}");

            // d) Guvenli Donusum: TryParse (hata firlatmaz)
            string badInput = "abc";
            bool success = int.TryParse(badInput, out int result);
            Console.WriteLine($"TryParse '{badInput}': success={success}, value={result}");

            string goodInput = "999";
            success = int.TryParse(goodInput, out result);
            Console.WriteLine($"TryParse '{goodInput}': success={success}, value={result}");

            // e) Convert sinifi ile donusum
            string boolText = "true";
            bool convertedBool = Convert.ToBoolean(boolText);
            int convertedInt = Convert.ToInt32("456");
            double convertedDouble = Convert.ToDouble("78.9");

            Console.WriteLine($"\nConvert.ToBoolean: {convertedBool}");
            Console.WriteLine($"Convert.ToInt32:   {convertedInt}");
            Console.WriteLine($"Convert.ToDouble:  {convertedDouble}");

            // f) ToString: Her sey string'e donusebilir
            int myNumber = 42;
            string myString = myNumber.ToString();
            Console.WriteLine($"ToString: {myString} (type: {myString.GetType().Name})");

            // -----------------------------------------------
            // 8) MATH SINIFI (Math Class)
            // -----------------------------------------------
            Console.WriteLine("\n--- Math Class ---");

            Console.WriteLine($"Math.Abs(-15):        {Math.Abs(-15)}");         // 15
            Console.WriteLine($"Math.Max(10, 20):     {Math.Max(10, 20)}");      // 20
            Console.WriteLine($"Math.Min(10, 20):     {Math.Min(10, 20)}");      // 10
            Console.WriteLine($"Math.Pow(2, 8):       {Math.Pow(2, 8)}");        // 256
            Console.WriteLine($"Math.Sqrt(144):       {Math.Sqrt(144)}");        // 12
            Console.WriteLine($"Math.Round(3.7):      {Math.Round(3.7)}");       // 4
            Console.WriteLine($"Math.Floor(3.9):      {Math.Floor(3.9)}");       // 3
            Console.WriteLine($"Math.Ceiling(3.1):    {Math.Ceiling(3.1)}");     // 4
            Console.WriteLine($"Math.PI:              {Math.PI}");

            // -----------------------------------------------
            // PRATIK SORULARI
            // -----------------------------------------------
            // 1. Kullanicidan iki sayi isteyin, dort islem sonuclarini yazdirin
            // 2. Bir sayinin cift mi tek mi oldugunu % operatoru ile bulun
            // 3. Kullanicidan string olarak yas isteyin, TryParse ile donusturun
            // 4. Ternary operator ile "Pass" veya "Fail" yazdirin (50 ustunde ise Pass)

            Console.WriteLine("\nLesson 03 completed! Press any key to exit...");
            Console.ReadKey();
        }
    }
}
