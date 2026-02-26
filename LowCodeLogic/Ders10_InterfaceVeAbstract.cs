// =============================================================
// DERS 10 - INTERFACE VE ABSTRACT SINIFLAR
// =============================================================
// Bu derste:
// - Interface tanimlama ve uygulama
// - Abstract class tanimlama
// - Interface vs Abstract class farklari
// - Coklu interface uygulama
// - Dependency Injection temeli
// =============================================================

using System;
using System.Collections.Generic;

namespace LowCodeLogic
{
    // -----------------------------------------------
    // 1) INTERFACE TANIMLAMA
    // -----------------------------------------------
    // Interface: "Ne yapilmali" yi tanimlar, "Nasil" i degil
    // I harfi ile baslar (IMovable, ISerializable vb.)

    interface IMovable
    {
        double Speed { get; set; }
        void Move(double x, double y);
        void Stop();
    }

    interface IDamageable
    {
        int Health { get; }
        int MaxHealth { get; }
        bool IsAlive { get; }
        void TakeDamage(int amount);
        void Heal(int amount);
    }

    interface IAttacker
    {
        int AttackPower { get; }
        void Attack(IDamageable target);
    }

    // -----------------------------------------------
    // 2) ABSTRACT CLASS
    // -----------------------------------------------
    // Hem gercek hem abstract metotlar icerebilir
    // Dogrudan nesne olusturulamaz

    abstract class GameEntity
    {
        public string Name { get; protected set; }
        public double X { get; protected set; }
        public double Y { get; protected set; }

        protected GameEntity(string name, double x = 0, double y = 0)
        {
            Name = name; X = x; Y = y;
        }

        // Abstract: Alt sinif yazmak ZORUNDA
        public abstract void Update();

        // Virtual: Override edilebilir ama zorunlu degil
        public virtual void Render()
        {
            Console.WriteLine($"  [{Name}] at ({X:F1}, {Y:F1})");
        }
    }

    // -----------------------------------------------
    // 3) COKLU INTERFACE UYGULAMA
    // -----------------------------------------------
    // C# coklu kalitim desteklemez AMA birden fazla interface uygulanir

    class Warrior : GameEntity, IMovable, IDamageable, IAttacker
    {
        public double Speed { get; set; }
        public int Health { get; private set; }
        public int MaxHealth { get; private set; }
        public bool IsAlive => Health > 0;
        public int AttackPower { get; private set; }

        public Warrior(string name, int hp, int atk, double spd) : base(name)
        {
            MaxHealth = hp; Health = hp; AttackPower = atk; Speed = spd;
        }

        public void Move(double x, double y)
        {
            X += x; Y += y;
            Console.WriteLine($"  {Name} moved to ({X:F1}, {Y:F1})");
        }
        public void Stop() => Console.WriteLine($"  {Name} stopped.");

        public void TakeDamage(int amount)
        {
            Health -= amount; if (Health < 0) Health = 0;
            Console.WriteLine($"  {Name} took {amount} dmg! HP: {Health}/{MaxHealth}");
        }
        public void Heal(int amount)
        {
            Health += amount; if (Health > MaxHealth) Health = MaxHealth;
            Console.WriteLine($"  {Name} healed {amount}. HP: {Health}/{MaxHealth}");
        }
        public void Attack(IDamageable target)
        {
            Console.WriteLine($"  {Name} attacks with {AttackPower} power!");
            target.TakeDamage(AttackPower);
        }

        public override void Update() { if (!IsAlive) Console.WriteLine($"  {Name} is dead."); }
        public override void Render()
        {
            Console.WriteLine($"  [{Name}] HP:{Health}/{MaxHealth} ATK:{AttackPower} SPD:{Speed}");
        }
    }

    // -----------------------------------------------
    // 4) DEPENDENCY INJECTION ORNEGI
    // -----------------------------------------------

    interface INotificationService
    {
        void Send(string to, string message);
    }

    class EmailNotification : INotificationService
    {
        public void Send(string to, string msg) => Console.WriteLine($"  EMAIL to {to}: {msg}");
    }

    class SmsNotification : INotificationService
    {
        public void Send(string to, string msg) => Console.WriteLine($"  SMS to {to}: {msg}");
    }

    // Bu sinif hangi notification kullanildigini bilmez
    class OrderService
    {
        private readonly INotificationService _notifier;

        public OrderService(INotificationService notifier) { _notifier = notifier; }

        public void PlaceOrder(string email, string product)
        {
            Console.WriteLine($"  Order placed: {product}");
            _notifier.Send(email, $"Order for '{product}' confirmed!");
        }
    }

    /*
        INTERFACE vs ABSTRACT CLASS:
        ┌─────────────────┬──────────────────────┬─────────────────────┐
        │                 │ Interface             │ Abstract Class      │
        ├─────────────────┼──────────────────────┼─────────────────────┤
        │ Coklu           │ EVET (birden fazla)   │ HAYIR (tek kalitim) │
        │ Constructor     │ HAYIR                 │ EVET                │
        │ Field           │ HAYIR                 │ EVET                │
        │ Gercek metot    │ EVET (C# 8+ default)  │ EVET                │
        │ Kullanim        │ "Can do" sozlesmesi   │ "Is a" iliskisi     │
        └─────────────────┴──────────────────────┴─────────────────────┘
    */

    class Lesson10_InterfaceAndAbstract
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== LESSON 10: Interface & Abstract ===\n");

            // Interface ve Abstract kullanimi
            Warrior knight = new Warrior("Knight", 150, 25, 3.0);
            Warrior archer = new Warrior("Archer", 100, 35, 5.0);

            knight.Render();
            archer.Render();

            Console.WriteLine("\n--- Battle ---");
            knight.Move(5, 3);
            knight.Attack(archer);
            archer.Attack(knight);
            knight.Heal(20);

            // Interface tipiyle polimorfizm
            Console.WriteLine("\n--- Interface as Type ---");
            List<IDamageable> targets = new List<IDamageable> { knight, archer };
            foreach (IDamageable t in targets)
                Console.WriteLine($"  HP: {t.Health}/{t.MaxHealth} Alive: {t.IsAlive}");

            // Dependency Injection
            Console.WriteLine("\n--- Dependency Injection ---");
            new OrderService(new EmailNotification()).PlaceOrder("alice@mail.com", "Laptop");
            new OrderService(new SmsNotification()).PlaceOrder("+905551234567", "Phone");

            Console.WriteLine("\nLesson 10 completed! Press any key to exit...");
            Console.ReadKey();
        }
    }
}
