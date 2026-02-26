// =============================================================
// DERS 08 - SINIFLAR VE NESNELER (Classes & Objects - OOP Basics)
// =============================================================
// Bu derste:
// - Class (sinif) tanimlama
// - Object (nesne) olusturma
// - Property (ozellik) ve Field (alan)
// - Constructor (yapici metot)
// - Encapsulation (kapsulleme)
// - Access modifiers (erisim belirleyiciler)
// - static vs instance uyeleri
// - this anahtar kelimesi
// =============================================================

using System;
using System.Collections.Generic;

namespace LowCodeLogic
{
    // -----------------------------------------------
    // 1) BASIT SINIF TANIMLAMA
    // -----------------------------------------------
    // Sinif bir sablondur, nesne ise o sablondan turksen bir ornektir

    class Car
    {
        // ALANLAR (Fields) - Sinifin verileri
        // private: disaridan erisilmez (varsayilan)
        private string _brand;
        private string _model;
        private int _year;
        private double _speed;

        // OZELLIKLER (Properties) - Alanlara kontrol erisim saglar
        // get: okuma, set: yazma
        public string Brand
        {
            get { return _brand; }
            set { _brand = value; }     // value = atanan deger
        }

        public string Model
        {
            get { return _model; }
            set { _model = value; }
        }

        public int Year
        {
            get { return _year; }
            set
            {
                // Dogrulama (validation) ekleyebiliriz
                if (value >= 1886 && value <= DateTime.Now.Year + 1)
                    _year = value;
                else
                    throw new ArgumentException("Invalid year.");
            }
        }

        // Read-only property (sadece okunabilir)
        public double Speed => _speed;

        // Auto-implemented property (otomatik ozellik)
        // field'i compiler otomatik olusturur
        public string Color { get; set; }

        // -----------------------------------------------
        // CONSTRUCTOR (Yapici Metot)
        // -----------------------------------------------
        // Nesne olusturulurken cagrilir
        // Sinif adiyla ayni isimde, donus tipi yok

        // Parametresiz constructor
        public Car()
        {
            _brand = "Unknown";
            _model = "Unknown";
            _year = 2024;
            _speed = 0;
            Color = "White";
        }

        // Parametreli constructor
        public Car(string brand, string model, int year, string color)
        {
            _brand = brand;
            _model = model;
            _year = year;
            _speed = 0;
            Color = color;
        }

        // -----------------------------------------------
        // METOTLAR (Methods)
        // -----------------------------------------------
        public void Accelerate(double amount)
        {
            _speed += amount;
            if (_speed > 250) _speed = 250;    // Hiz limiti
            Console.WriteLine($"{_brand} {_model} accelerated to {_speed:F0} km/h");
        }

        public void Brake(double amount)
        {
            _speed -= amount;
            if (_speed < 0) _speed = 0;
            Console.WriteLine($"{_brand} {_model} slowed to {_speed:F0} km/h");
        }

        // Override: ToString metodu (her sinifta override edilebilir)
        public override string ToString()
        {
            return $"{_year} {_brand} {_model} ({Color}) - {_speed:F0} km/h";
        }
    }

    // -----------------------------------------------
    // 2) ENCAPSULATION ORNEGI - BankAccount
    // -----------------------------------------------
    // Kapsulleme: Ic detaylari gizleyip sadece gerekli erisimi saglamak

    class BankAccount
    {
        // Private alanlar - disaridan dogrudan erisilmez
        private string _accountNumber;
        private decimal _balance;
        private List<string> _transactionHistory;

        // Public read-only ozellikler
        public string AccountNumber => _accountNumber;
        public decimal Balance => _balance;
        public string Owner { get; private set; }  // Disaridan okuabilir, sadece sinif icinden yazilabilir

        // Constructor
        public BankAccount(string owner, string accountNumber, decimal initialBalance = 0)
        {
            Owner = owner;
            _accountNumber = accountNumber;
            _balance = initialBalance;
            _transactionHistory = new List<string>();
            _transactionHistory.Add($"Account opened with {initialBalance:C}");
        }

        // Para yatirma
        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Deposit amount must be positive.");
                return;
            }

            _balance += amount;
            _transactionHistory.Add($"Deposited: {amount:C} | Balance: {_balance:C}");
            Console.WriteLine($"Deposited {amount:C}. New balance: {_balance:C}");
        }

        // Para cekme
        public bool Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Withdrawal amount must be positive.");
                return false;
            }

            if (amount > _balance)
            {
                Console.WriteLine($"Insufficient funds. Balance: {_balance:C}");
                return false;
            }

            _balance -= amount;
            _transactionHistory.Add($"Withdrew: {amount:C} | Balance: {_balance:C}");
            Console.WriteLine($"Withdrew {amount:C}. New balance: {_balance:C}");
            return true;
        }

        // Islem gecmisi
        public void PrintHistory()
        {
            Console.WriteLine($"\n--- Transaction History ({Owner}) ---");
            foreach (string entry in _transactionHistory)
            {
                Console.WriteLine($"  {entry}");
            }
        }
    }

    // -----------------------------------------------
    // 3) STATIC UYELER
    // -----------------------------------------------
    // static: Nesne olusturmadan sinif uzerinden erisilebilir

    class MathHelper
    {
        // Static alan: Tum nesneler arasinda paylasilir
        public static int CalculationCount { get; private set; } = 0;

        // Static metot
        public static double CircleArea(double radius)
        {
            CalculationCount++;
            return Math.PI * radius * radius;
        }

        public static double RectangleArea(double width, double height)
        {
            CalculationCount++;
            return width * height;
        }

        // Static sabit
        public const double GoldenRatio = 1.6180339887;
    }

    // -----------------------------------------------
    // 4) THIS ANAHTAR KELIMESI
    // -----------------------------------------------

    class Player
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }

        // this: Mevcut nesneyi temsil eder
        // Parametre adi ile property adi ayni oldugunda ayirt etmek icin kullanilir
        public Player(string name, int level = 1)
        {
            this.Name = name;
            this.Level = level;
            this.MaxHealth = 100 + (level * 10);
            this.Health = this.MaxHealth;
        }

        // this ile method chaining (zincirleme metot cagirma)
        public Player TakeDamage(int damage)
        {
            Health -= damage;
            if (Health < 0) Health = 0;
            Console.WriteLine($"{Name} took {damage} damage. HP: {Health}/{MaxHealth}");
            return this;     // Kendini dondurerek zincirleme saglar
        }

        public Player Heal(int amount)
        {
            Health += amount;
            if (Health > MaxHealth) Health = MaxHealth;
            Console.WriteLine($"{Name} healed {amount}. HP: {Health}/{MaxHealth}");
            return this;
        }

        public override string ToString() => $"{Name} (Lv.{Level}) HP: {Health}/{MaxHealth}";
    }

    // -----------------------------------------------
    // 5) ERISIM BELIRLEYICILER OZETI
    // -----------------------------------------------
    /*
        public    : Her yerden erisilebilir
        private   : Sadece sinif icinden erisilebilir (varsayilan)
        protected : Sinif ve turetilmis siniflardan erisilebilir
        internal  : Ayni assembly (proje) icinden erisilebilir
        protected internal : protected VEYA internal
        private protected  : protected VE internal (ayni assembly'deki alt siniflar)
    */

    // -----------------------------------------------
    // MAIN
    // -----------------------------------------------
    class Lesson08_ClassesAndObjects
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== LESSON 08: Classes & Objects ===\n");

            // 1) Nesne olusturma
            Console.WriteLine("--- Creating Objects ---");

            Car car1 = new Car();                                        // Parametresiz constructor
            Car car2 = new Car("Tesla", "Model S", 2024, "Red");        // Parametreli constructor

            car1.Brand = "BMW";
            car1.Model = "M3";
            car1.Color = "Black";

            Console.WriteLine(car1);
            Console.WriteLine(car2);

            car2.Accelerate(80);
            car2.Accelerate(40);
            car2.Brake(30);

            // 2) Kapsulleme
            Console.WriteLine("\n--- Encapsulation (BankAccount) ---");

            BankAccount account = new BankAccount("Alice", "TR12345", 1000);
            account.Deposit(500);
            account.Withdraw(200);
            account.Withdraw(2000);     // Yetersiz bakiye
            account.PrintHistory();

            // account._balance = 1000000;  // HATA! private alana disaridan erisilmez
            Console.WriteLine($"Balance (read-only): {account.Balance}");

            // 3) Static uyeler
            Console.WriteLine("\n--- Static Members ---");

            double area1 = MathHelper.CircleArea(5);
            double area2 = MathHelper.RectangleArea(10, 20);

            Console.WriteLine($"Circle area: {area1:F2}");
            Console.WriteLine($"Rectangle area: {area2:F2}");
            Console.WriteLine($"Total calculations: {MathHelper.CalculationCount}");
            Console.WriteLine($"Golden Ratio: {MathHelper.GoldenRatio}");

            // 4) Method chaining
            Console.WriteLine("\n--- Method Chaining ---");

            Player hero = new Player("Knight", 5);
            Console.WriteLine(hero);

            // Zincirleme metot cagirma
            hero.TakeDamage(30)
                .TakeDamage(20)
                .Heal(25)
                .TakeDamage(50);

            Console.WriteLine(hero);

            // 5) Object initializer syntax
            Console.WriteLine("\n--- Object Initializer ---");

            Car car3 = new Car
            {
                Brand = "Audi",
                Model = "A4",
                Color = "Silver"
            };
            Console.WriteLine(car3);

            // -----------------------------------------------
            // PRATIK SORULARI
            // -----------------------------------------------
            // 1. Bir "Student" sinifi olusturun (Name, Age, Grades listesi, ortalama metodu)
            // 2. Bir "Rectangle" sinifi yapin (width, height, area, perimeter metotlari)
            // 3. Static bir "IdGenerator" sinifi yapin (her cagrildiginda artan ID uretsin)
            // 4. Method chaining ile bir "QueryBuilder" sinifi tasarlayin

            Console.WriteLine("\nLesson 08 completed! Press any key to exit...");
            Console.ReadKey();
        }
    }
}
