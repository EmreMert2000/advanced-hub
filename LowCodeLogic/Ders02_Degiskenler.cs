// =============================================================
// DERS 02 - DEGISKENLER VE VERI TIPLERI (Variables & Data Types)
// =============================================================
// Bu derste C# dilindeki veri tiplerini ve degisken tanimlamayi 
// ogreneceksiniz.
// - Temel veri tipleri (int, double, string, bool, char)
// - Degisken tanimlama ve atama
// - const ve readonly
// - var anahtar kelimesi
// - Nullable tipler
// - Default degerler
// =============================================================

using System;

namespace LowCodeLogic
{
    class Lesson02_Variables
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== LESSON 02: Variables & Data Types ===\n");

            // -----------------------------------------------
            // 1) TAM SAYI TIPLERI (Integer Types)
            // -----------------------------------------------
            // byte   : 0 to 255               (8-bit)
            // short  : -32,768 to 32,767       (16-bit)
            // int    : -2.1 milyar to 2.1 milyar (32-bit) -> En cok kullanilan
            // long   : cok buyuk sayilar        (64-bit)

            byte smallNumber = 255;
            short mediumNumber = 32000;
            int regularNumber = 1_000_000;      // Alt cizgi okunurluk icin kullanilabilir
            long bigNumber = 9_000_000_000L;    // L son eki long icin gerekli

            Console.WriteLine("--- Integer Types ---");
            Console.WriteLine($"byte:  {smallNumber}");
            Console.WriteLine($"short: {mediumNumber}");
            Console.WriteLine($"int:   {regularNumber}");
            Console.WriteLine($"long:  {bigNumber}");

            // -----------------------------------------------
            // 2) ONDALIKLI SAYI TIPLERI (Floating Point Types)
            // -----------------------------------------------
            // float  : 7 basamak hassasiyet   (32-bit) -> f son eki gerekli
            // double : 15 basamak hassasiyet   (64-bit) -> Varsayilan ondalik tip
            // decimal: 28 basamak hassasiyet  (128-bit) -> Para hesaplamalari icin ideal

            float temperature = 36.6f;
            double pi = 3.141592653589793;
            decimal price = 99.99m;             // m son eki decimal icin gerekli

            Console.WriteLine("\n--- Floating Point Types ---");
            Console.WriteLine($"float:   {temperature}");
            Console.WriteLine($"double:  {pi}");
            Console.WriteLine($"decimal: {price}");

            // -----------------------------------------------
            // 3) METIN TIPLERI (Text Types)
            // -----------------------------------------------
            // char   : Tek karakter (tek tirnak ile)
            // string : Metin dizisi (cift tirnak ile)

            char grade = 'A';
            char symbol = '#';
            string message = "Hello, C#!";
            string emptyText = "";                  // Bos string
            string nullText = null;                 // Null string (deger yok)
            string emptyString = string.Empty;      // Bos string (tercih edilen yontem)

            Console.WriteLine("\n--- Text Types ---");
            Console.WriteLine($"char:   {grade}");
            Console.WriteLine($"char:   {symbol}");
            Console.WriteLine($"string: {message}");
            Console.WriteLine($"Is empty: {string.IsNullOrEmpty(emptyText)}");
            Console.WriteLine($"Is null:  {string.IsNullOrEmpty(nullText)}");

            // -----------------------------------------------
            // 4) MANTIKSAL TIP (Boolean Type)
            // -----------------------------------------------
            // bool: true veya false (sadece iki deger alabilir)

            bool isActive = true;
            bool isCompleted = false;

            Console.WriteLine("\n--- Boolean Type ---");
            Console.WriteLine($"isActive:    {isActive}");
            Console.WriteLine($"isCompleted: {isCompleted}");

            // -----------------------------------------------
            // 5) VAR ANAHTAR KELIMESI (Type Inference)
            // -----------------------------------------------
            // var: Derleyici tipi otomatik belirler
            // Degiskenin tipi ilk atamada belirlenir ve degismez

            var autoInt = 42;               // int olarak belirlenir
            var autoDouble = 3.14;          // double olarak belirlenir
            var autoString = "Hello";       // string olarak belirlenir
            var autoBool = true;            // bool olarak belirlenir

            Console.WriteLine("\n--- var Keyword ---");
            Console.WriteLine($"autoInt type:    {autoInt.GetType().Name}");     // Int32
            Console.WriteLine($"autoDouble type: {autoDouble.GetType().Name}");  // Double
            Console.WriteLine($"autoString type: {autoString.GetType().Name}");  // String
            Console.WriteLine($"autoBool type:   {autoBool.GetType().Name}");    // Boolean

            // -----------------------------------------------
            // 6) SABITLER (Constants)
            // -----------------------------------------------
            // const: Derleme zamaninda belirlenir, ASLA degismez
            // readonly: Calisma zamaninda atanabilir (constructor'da), sonra degismez

            const double Gravity = 9.81;
            const string AppName = "LowCodeLogic";
            const int MaxRetryCount = 3;

            Console.WriteLine("\n--- Constants ---");
            Console.WriteLine($"Gravity:  {Gravity}");
            Console.WriteLine($"App Name: {AppName}");
            Console.WriteLine($"Max Retry: {MaxRetryCount}");

            // Gravity = 10.0; // HATA! const degistirilemez

            // -----------------------------------------------
            // 7) NULLABLE TIPLER (Nullable Types)
            // -----------------------------------------------
            // Deger tiplerinin (int, bool, double) null alabilmesi icin ? eklenir
            // Referans tipleri (string, object) zaten null alabilir

            int? nullableAge = null;        // Degeri yok
            double? nullableScore = 95.5;   // Degeri var
            bool? nullableFlag = null;

            Console.WriteLine("\n--- Nullable Types ---");
            Console.WriteLine($"Has value: {nullableAge.HasValue}");    // false
            Console.WriteLine($"Has value: {nullableScore.HasValue}");  // true

            // Null-coalescing operatoru: ?? -> null ise varsayilan deger kullan
            int age = nullableAge ?? 0;
            Console.WriteLine($"Age (with default): {age}");

            // -----------------------------------------------
            // 8) DEFAULT DEGERLER
            // -----------------------------------------------
            // Her tipin bir default degeri vardir:
            // int    -> 0
            // double -> 0.0
            // bool   -> false
            // string -> null
            // char   -> '\0'

            Console.WriteLine("\n--- Default Values ---");
            Console.WriteLine($"default(int):    {default(int)}");
            Console.WriteLine($"default(double): {default(double)}");
            Console.WriteLine($"default(bool):   {default(bool)}");
            Console.WriteLine($"default(char):   '{default(char)}'");
            Console.WriteLine($"default(string): {default(string) ?? "null"}");

            // -----------------------------------------------
            // 9) TIP BOYUTLARI (Type Sizes)
            // -----------------------------------------------
            Console.WriteLine("\n--- Type Sizes (bytes) ---");
            Console.WriteLine($"byte:    {sizeof(byte)}");
            Console.WriteLine($"short:   {sizeof(short)}");
            Console.WriteLine($"int:     {sizeof(int)}");
            Console.WriteLine($"long:    {sizeof(long)}");
            Console.WriteLine($"float:   {sizeof(float)}");
            Console.WriteLine($"double:  {sizeof(double)}");
            Console.WriteLine($"decimal: {sizeof(decimal)}");
            Console.WriteLine($"char:    {sizeof(char)}");
            Console.WriteLine($"bool:    {sizeof(bool)}");

            // -----------------------------------------------
            // PRATIK SORULARI
            // -----------------------------------------------
            // 1. Bir degisken tanimlayip icine dogum yilinizi yazin, tipini konsola yazdirin
            // 2. Bir decimal degisken tanimlayip icine maasinizi yazin
            // 3. Nullable bir int tanimlayip, null-coalescing ile varsayilan deger verin
            // 4. const ile PI sayisini tanimlayip ekrana yazdirin

            Console.WriteLine("\nLesson 02 completed! Press any key to exit...");
            Console.ReadKey();
        }
    }
}
