// =============================================================
// DERS 04 - KOSUL IFADELERI (Conditional Statements)
// =============================================================
// Bu derste:
// - if / else if / else
// - switch-case
// - switch expression (C# 8+)
// - Pattern matching
// - Nested (ic ice) kosullar
// =============================================================

using System;

namespace LowCodeLogic
{
    class Lesson04_Conditionals
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== LESSON 04: Conditional Statements ===\n");

            // -----------------------------------------------
            // 1) IF / ELSE YAPISI
            // -----------------------------------------------
            // En temel kosul yapisi. Kosulin true/false olmasina gore kod calistirir.

            int temperature = 35;

            if (temperature >= 40)
            {
                Console.WriteLine("Extreme heat warning!");
            }
            else if (temperature >= 30)
            {
                Console.WriteLine("It's hot outside.");
            }
            else if (temperature >= 20)
            {
                Console.WriteLine("Nice weather.");
            }
            else if (temperature >= 10)
            {
                Console.WriteLine("It's cool.");
            }
            else
            {
                Console.WriteLine("It's cold!");
            }

            // -----------------------------------------------
            // 2) TEK SATIRLIK IF (Braces Olmadan)
            // -----------------------------------------------
            // Tek satirlik kodlar icin suslu parantez opsiyonel
            // AMA okunurluk icin her zaman parantez kullanmak onerilir

            int age = 20;
            if (age >= 18) Console.WriteLine("You are an adult.");

            // -----------------------------------------------
            // 3) MANTIKSAL OPERATORLERLE KOSULLAR
            // -----------------------------------------------
            Console.WriteLine("\n--- Combined Conditions ---");

            string username = "admin";
            string password = "1234";

            if (username == "admin" && password == "1234")
            {
                Console.WriteLine("Login successful!");
            }
            else
            {
                Console.WriteLine("Invalid credentials.");
            }

            // OR (||) ornegi
            string day = "Saturday";
            if (day == "Saturday" || day == "Sunday")
            {
                Console.WriteLine($"{day} is a weekend day.");
            }
            else
            {
                Console.WriteLine($"{day} is a weekday.");
            }

            // -----------------------------------------------
            // 4) IC ICE KOSULLAR (Nested Conditions)
            // -----------------------------------------------
            Console.WriteLine("\n--- Nested Conditions ---");

            int studentAge = 22;
            double gpa = 3.5;
            bool hasScholarship = true;

            if (studentAge >= 18)
            {
                Console.WriteLine("Student is eligible for university.");

                if (gpa >= 3.0)
                {
                    Console.WriteLine("Good academic standing.");

                    if (hasScholarship)
                    {
                        Console.WriteLine("Has a scholarship - tuition covered!");
                    }
                }
                else
                {
                    Console.WriteLine("Needs to improve GPA.");
                }
            }

            // -----------------------------------------------
            // 5) SWITCH-CASE YAPISI
            // -----------------------------------------------
            // Bir degiskeni birden fazla degerle karsilastirmak icin idealdir
            Console.WriteLine("\n--- Switch-Case ---");

            int dayNumber = 3;

            switch (dayNumber)
            {
                case 1:
                    Console.WriteLine("Monday");
                    break;
                case 2:
                    Console.WriteLine("Tuesday");
                    break;
                case 3:
                    Console.WriteLine("Wednesday");
                    break;
                case 4:
                    Console.WriteLine("Thursday");
                    break;
                case 5:
                    Console.WriteLine("Friday");
                    break;
                case 6:
                case 7:                                 // Birden fazla case ayni kodu calistirabilir
                    Console.WriteLine("Weekend!");
                    break;
                default:                                // Hicbir case uymadiysa
                    Console.WriteLine("Invalid day number.");
                    break;
            }

            // String ile switch
            string command = "start";

            switch (command.ToLower())
            {
                case "start":
                    Console.WriteLine("System starting...");
                    break;
                case "stop":
                    Console.WriteLine("System stopping...");
                    break;
                case "restart":
                    Console.WriteLine("System restarting...");
                    break;
                default:
                    Console.WriteLine($"Unknown command: {command}");
                    break;
            }

            // -----------------------------------------------
            // 6) SWITCH EXPRESSION (C# 8.0+)
            // -----------------------------------------------
            // Daha kisa ve modern bir switch kullanimi
            Console.WriteLine("\n--- Switch Expression ---");

            int month = 7;
            string season = month switch
            {
                12 or 1 or 2 => "Winter",
                3 or 4 or 5 => "Spring",
                6 or 7 or 8 => "Summer",
                9 or 10 or 11 => "Autumn",
                _ => "Invalid month"                     // _ = default
            };
            Console.WriteLine($"Month {month}: {season}");

            // Grade hesaplama ornegi
            int examScore = 85;
            string grade = examScore switch
            {
                >= 90 => "A",
                >= 80 => "B",
                >= 70 => "C",
                >= 60 => "D",
                _ => "F"
            };
            Console.WriteLine($"Score {examScore}: Grade {grade}");

            // -----------------------------------------------
            // 7) PATTERN MATCHING (Desen Eslestirme)
            // -----------------------------------------------
            Console.WriteLine("\n--- Pattern Matching ---");

            // is anahtar kelimesi ile tip kontrolu
            object value = 42;

            if (value is int intValue)
            {
                Console.WriteLine($"It's an integer: {intValue}");
            }
            else if (value is string stringValue)
            {
                Console.WriteLine($"It's a string: {stringValue}");
            }

            // not, and, or pattern'leri (C# 9+)
            int level = 75;

            if (level is >= 1 and <= 100)
            {
                Console.WriteLine($"Level {level} is in valid range (1-100).");
            }

            if (level is not 0)
            {
                Console.WriteLine($"Level is not zero.");
            }

            // Null checking pattern
            string? input = null;

            if (input is null)
            {
                Console.WriteLine("Input is null.");
            }

            if (input is not null)
            {
                Console.WriteLine($"Input: {input}");
            }

            // -----------------------------------------------
            // 8) GERCEK DUNYA ORNEGI: Basit Hesap Makinesi
            // -----------------------------------------------
            Console.WriteLine("\n--- Mini Calculator ---");

            double num1 = 20;
            double num2 = 6;
            char operation = '/';

            double calcResult = operation switch
            {
                '+' => num1 + num2,
                '-' => num1 - num2,
                '*' => num1 * num2,
                '/' => num2 != 0 ? num1 / num2 : double.NaN,
                '%' => num1 % num2,
                _ => double.NaN
            };

            if (double.IsNaN(calcResult))
                Console.WriteLine("Invalid operation or division by zero.");
            else
                Console.WriteLine($"{num1} {operation} {num2} = {calcResult:F2}");

            // -----------------------------------------------
            // 9) GUARD CLAUSE PATTERN (Erken Cikis)
            // -----------------------------------------------
            // Ic ice kosullar yerine erken return/devam etme yontemi
            // Bu yontem kodun okunurlagunu arttirir
            Console.WriteLine("\n--- Guard Clause Pattern ---");

            string userInput = "hello";

            // KOTU YONTEM (Nested):
            // if (userInput != null) {
            //     if (userInput.Length > 0) {
            //         if (userInput != "exit") {
            //             // is yap
            //         }
            //     }
            // }

            // IYI YONTEM (Guard Clause):
            if (string.IsNullOrEmpty(userInput))
            {
                Console.WriteLine("Input is empty. Skipping...");
            }
            else if (userInput == "exit")
            {
                Console.WriteLine("Exit command received.");
            }
            else
            {
                Console.WriteLine($"Processing: {userInput}");
            }

            // -----------------------------------------------
            // PRATIK SORULARI
            // -----------------------------------------------
            // 1. Kullanicidan bir sayi isteyin: pozitif, negatif veya sifir yazdirin
            // 2. Kullanicidan not isteyin (0-100), harf notunu switch expression ile bulun
            // 3. Kullanicidan ay numarasi isteyin, kac gun oldugunu yazdirin
            // 4. BMI hesaplayici yapin: kilo ve boy isteyin, sonucu kategorize edin

            Console.WriteLine("\nLesson 04 completed! Press any key to exit...");
            Console.ReadKey();
        }
    }
}
