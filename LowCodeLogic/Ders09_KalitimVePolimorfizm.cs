// =============================================================
// DERS 09 - KALITIM VE POLIMORFIZM (Inheritance & Polymorphism)
// =============================================================
// Bu derste:
// - Kalitim (Inheritance) - bir sinifin baska bir siniftan turemesi
// - base anahtar kelimesi
// - virtual / override - metot gecersiz kilma
// - sealed - kalitimi engelleme
// - Polimorfizm - ayni arayuz, farkli davranis
// - Upcasting ve Downcasting
// - is ve as operatorleri
// =============================================================

using System;
using System.Collections.Generic;

namespace LowCodeLogic
{
    // -----------------------------------------------
    // 1) TEMEL SINIF (Base Class)
    // -----------------------------------------------

    class Animal
    {
        // Protected: Bu sinif ve turetilmis siniflardan erisilebilir
        protected string name;

        public string Name => name;
        public int Age { get; set; }

        // Virtual: Alt siniflar bu metodu override edebilir
        public virtual string Sound => "...";

        public Animal(string name, int age)
        {
            this.name = name;
            this.Age = age;
        }

        // Virtual metot: Alt siniflar degistirebilir
        public virtual void Speak()
        {
            Console.WriteLine($"{name} says: {Sound}");
        }

        public virtual void DisplayInfo()
        {
            Console.WriteLine($"Animal: {name}, Age: {Age}");
        }

        public override string ToString() => $"{name} (Age: {Age})";
    }

    // -----------------------------------------------
    // 2) TURETILMIS SINIFLAR (Derived Classes)
    // -----------------------------------------------
    // class Alt : Ust -> Alt sinif, Ust siniftan turetilmistir

    class Dog : Animal
    {
        public string Breed { get; set; }

        // override: Sound property'sini degistiriyoruz
        public override string Sound => "Woof!";

        // base(): Ust sinifin constructor'ini cagirir
        public Dog(string name, int age, string breed) : base(name, age)
        {
            Breed = breed;
        }

        // override: Ust sinifin metotunu degistiriyoruz
        public override void DisplayInfo()
        {
            base.DisplayInfo();     // Ust sinifin versiyonunu cagir
            Console.WriteLine($"  Breed: {Breed}");
        }

        // Alt sinifa ozel metot
        public void Fetch(string item)
        {
            Console.WriteLine($"{name} fetches the {item}!");
        }
    }

    class Cat : Animal
    {
        public bool IsIndoor { get; set; }

        public override string Sound => "Meow!";

        public Cat(string name, int age, bool isIndoor) : base(name, age)
        {
            IsIndoor = isIndoor;
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"  Indoor: {IsIndoor}");
        }

        public void Purr()
        {
            Console.WriteLine($"{name} is purring...");
        }
    }

    class Bird : Animal
    {
        public double Wingspan { get; set; }

        public override string Sound => "Tweet!";

        public Bird(string name, int age, double wingspan) : base(name, age)
        {
            Wingspan = wingspan;
        }

        public override void Speak()
        {
            // Tamamen kendi versiyonumuzu yazabiliriz
            Console.WriteLine($"{name} sings: {Sound} {Sound}");
        }

        public void Fly()
        {
            Console.WriteLine($"{name} is flying with {Wingspan}cm wingspan!");
        }
    }

    // -----------------------------------------------
    // 3) COKLU KATMAN KALITIM (Multi-Level Inheritance)
    // -----------------------------------------------

    class GuideDog : Dog
    {
        public string OwnerName { get; set; }

        public GuideDog(string name, int age, string breed, string ownerName)
            : base(name, age, breed)
        {
            OwnerName = ownerName;
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"  Guide for: {OwnerName}");
        }

        public void Guide()
        {
            Console.WriteLine($"{name} is guiding {OwnerName}.");
        }
    }

    // -----------------------------------------------
    // 4) SEALED SINIF - Kalitimi Engelleme
    // -----------------------------------------------

    sealed class GoldenRetriever : Dog
    {
        // Bu siniftan baska sinif turetilemez
        public GoldenRetriever(string name, int age) : base(name, age, "Golden Retriever")
        {
        }

        public void BeHappy()
        {
            Console.WriteLine($"{name} is wagging tail happily!");
        }
    }

    // class SuperGolden : GoldenRetriever {}  // HATA! sealed siniftan turetme yok

    // -----------------------------------------------
    // 5) POLIMORFIZM ORNEGI: Sekiller
    // -----------------------------------------------

    class Shape
    {
        public string Name { get; protected set; }
        public string Color { get; set; }

        public Shape(string name, string color = "Black")
        {
            Name = name;
            Color = color;
        }

        public virtual double CalculateArea() => 0;
        public virtual double CalculatePerimeter() => 0;

        public void PrintInfo()
        {
            Console.WriteLine($"  {Name} ({Color}): " +
                              $"Area = {CalculateArea():F2}, " +
                              $"Perimeter = {CalculatePerimeter():F2}");
        }
    }

    class Circle : Shape
    {
        public double Radius { get; set; }

        public Circle(double radius, string color = "Red") : base("Circle", color)
        {
            Radius = radius;
        }

        public override double CalculateArea() => Math.PI * Radius * Radius;
        public override double CalculatePerimeter() => 2 * Math.PI * Radius;
    }

    class Rectangle : Shape
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public Rectangle(double width, double height, string color = "Blue")
            : base("Rectangle", color)
        {
            Width = width;
            Height = height;
        }

        public override double CalculateArea() => Width * Height;
        public override double CalculatePerimeter() => 2 * (Width + Height);
    }

    class Triangle : Shape
    {
        public double SideA { get; set; }
        public double SideB { get; set; }
        public double SideC { get; set; }

        public Triangle(double a, double b, double c, string color = "Green")
            : base("Triangle", color)
        {
            SideA = a;
            SideB = b;
            SideC = c;
        }

        public override double CalculateArea()
        {
            double s = (SideA + SideB + SideC) / 2;
            return Math.Sqrt(s * (s - SideA) * (s - SideB) * (s - SideC));
        }

        public override double CalculatePerimeter() => SideA + SideB + SideC;
    }

    // -----------------------------------------------
    // MAIN
    // -----------------------------------------------
    class Lesson09_InheritancePolymorphism
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== LESSON 09: Inheritance & Polymorphism ===\n");

            // 1) Kalitim
            Console.WriteLine("--- Inheritance ---");

            Dog dog = new Dog("Rex", 5, "German Shepherd");
            Cat cat = new Cat("Whiskers", 3, true);
            Bird bird = new Bird("Tweety", 1, 15.5);

            dog.DisplayInfo();
            Console.WriteLine();
            cat.DisplayInfo();
            Console.WriteLine();
            bird.DisplayInfo();

            Console.WriteLine();
            dog.Speak();
            cat.Speak();
            bird.Speak();    // Kendi versiyonunu kullanir

            dog.Fetch("ball");
            cat.Purr();
            bird.Fly();

            // 2) Multi-level inheritance
            Console.WriteLine("\n--- Multi-Level Inheritance ---");

            GuideDog guideDog = new GuideDog("Buddy", 4, "Labrador", "John");
            guideDog.DisplayInfo();
            guideDog.Guide();
            guideDog.Fetch("stick");     // Dog'dan miras alinmis

            // 3) Polimorfizm
            Console.WriteLine("\n--- Polymorphism ---");

            // Farkli tip nesneler ust sinif referansiyla tutulabilir
            // Her biri kendi override edilmis metodunu calistirir

            List<Animal> animals = new List<Animal>
            {
                new Dog("Max", 3, "Husky"),
                new Cat("Luna", 2, false),
                new Bird("Eagle", 7, 200),
                new GuideDog("Coco", 5, "Poodle", "Sarah")
            };

            Console.WriteLine("All animals speak:");
            foreach (Animal animal in animals)
            {
                animal.Speak();     // Her hayvan kendi sesini cikarir -> POLIMORFIZM
            }

            // 4) Upcasting ve Downcasting
            Console.WriteLine("\n--- Upcasting & Downcasting ---");

            // Upcasting: Alt sinif -> Ust sinif (otomatik, guvenli)
            Animal myAnimal = new Dog("Pluto", 4, "Mixed");
            myAnimal.Speak();         // Dog'un Speak'i calisir (polimorfizm)
            // myAnimal.Fetch("bone");// HATA! Animal tipinde Fetch yok

            // Downcasting: Ust sinif -> Alt sinif (acik casting, riskli)
            if (myAnimal is Dog myDog)      // is ile guvenli kontrol
            {
                myDog.Fetch("bone");         // Artik Dog tipi olarak kullanilabilir
            }

            // as operatoru: Cast basarisiz olursa null doner (exception firlatmaz)
            Animal someAnimal = new Cat("Neko", 1, true);
            Dog? asDog = someAnimal as Dog;
            Cat? asCat = someAnimal as Cat;

            Console.WriteLine($"as Dog: {(asDog == null ? "null" : asDog.Name)}");
            Console.WriteLine($"as Cat: {(asCat == null ? "null" : asCat.Name)}");

            // 5) Polimorfizm ile sekiller
            Console.WriteLine("\n--- Shape Polymorphism ---");

            List<Shape> shapes = new List<Shape>
            {
                new Circle(5, "Red"),
                new Rectangle(10, 4, "Blue"),
                new Triangle(3, 4, 5, "Green"),
                new Circle(2.5, "Yellow")
            };

            double totalArea = 0;
            foreach (Shape shape in shapes)
            {
                shape.PrintInfo();
                totalArea += shape.CalculateArea();
            }
            Console.WriteLine($"\nTotal area of all shapes: {totalArea:F2}");

            // 6) Tip kontrolu
            Console.WriteLine("\n--- Type Checking ---");
            foreach (Shape shape in shapes)
            {
                string info = shape switch
                {
                    Circle c => $"Circle with radius {c.Radius}",
                    Rectangle r => $"Rectangle {r.Width}x{r.Height}",
                    Triangle t => $"Triangle ({t.SideA}, {t.SideB}, {t.SideC})",
                    _ => "Unknown shape"
                };
                Console.WriteLine($"  {info}");
            }

            // -----------------------------------------------
            // PRATIK SORULARI
            // -----------------------------------------------
            // 1. Vehicle -> Car, Truck, Motorcycle hiyerarsisi olusturun
            // 2. Her arac tipine ozel ozellik ve metot ekleyin
            // 3. Bir List<Vehicle> icinde polimorfik davranis sergileyin
            // 4. Employee -> Manager, Developer, Designer hiyerarsisi yapin

            Console.WriteLine("\nLesson 09 completed! Press any key to exit...");
            Console.ReadKey();
        }
    }
}
