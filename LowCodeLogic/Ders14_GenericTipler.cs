// =============================================================
// DERS 14 - GENERIC TIPLER (Generics)
// =============================================================
// Bu derste:
// - Generic nedir ve neden kullanilir
// - Generic siniflar
// - Generic metotlar
// - Generic constraint'ler (kisitlamalar)
// - Generic interface
// - Covariance ve Contravariance temeli
// =============================================================

using System;
using System.Collections.Generic;

namespace LowCodeLogic
{
    // -----------------------------------------------
    // 1) GENERIC SINIF
    // -----------------------------------------------
    // T: Tip parametresi - kullanim aninda belirlenir
    // <T> yerine <TItem>, <TKey, TValue> gibi isimler de kullanilabilir

    class Box<T>
    {
        private T _content;
        public bool HasContent { get; private set; }

        public void Put(T item)
        {
            _content = item;
            HasContent = true;
            Console.WriteLine($"  Stored: {item}");
        }

        public T Get()
        {
            if (!HasContent) throw new InvalidOperationException("Box is empty!");
            HasContent = false;
            return _content;
        }
    }

    // Birden fazla tip parametresi
    class Pair<TFirst, TSecond>
    {
        public TFirst First { get; set; }
        public TSecond Second { get; set; }

        public Pair(TFirst first, TSecond second)
        {
            First = first;
            Second = second;
        }

        public override string ToString() => $"({First}, {Second})";
    }

    // -----------------------------------------------
    // 2) GENERIC CONSTRAINT (Kisitlamalar)
    // -----------------------------------------------
    // where T : struct       -> T deger tipi olmali (int, double, bool vb.)
    // where T : class        -> T referans tipi olmali (string, siniflar vb.)
    // where T : new()        -> T parametresiz constructor'a sahip olmali
    // where T : BaseClass    -> T belirtilen siniftan turetilmis olmali
    // where T : IInterface   -> T belirtilen interface'i uygulamali
    // where T : notnull       -> T null olamaz

    interface IIdentifiable
    {
        int Id { get; }
        string Name { get; }
    }

    class Product : IIdentifiable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public Product() { }  // new() constraint icin parametresiz constructor gerekli
        public Product(int id, string name, decimal price)
        { Id = id; Name = name; Price = price; }

        public override string ToString() => $"[{Id}] {Name} - {Price:C}";
    }

    class Employee : IIdentifiable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }

        public Employee() { }
        public Employee(int id, string name, string dept)
        { Id = id; Name = name; Department = dept; }

        public override string ToString() => $"[{Id}] {Name} ({Department})";
    }

    // Constraint'li generic sinif: T, IIdentifiable olmak zorunda
    class Repository<T> where T : IIdentifiable, new()
    {
        private List<T> _items = new List<T>();

        public void Add(T item)
        {
            _items.Add(item);
            Console.WriteLine($"  Added: {item}");
        }

        public T FindById(int id)
        {
            return _items.Find(i => i.Id == id);
        }

        public List<T> GetAll() => new List<T>(_items);

        public bool Remove(int id)
        {
            var item = FindById(id);
            if (item != null) { _items.Remove(item); return true; }
            return false;
        }

        public int Count => _items.Count;
    }

    // -----------------------------------------------
    // 3) GENERIC INTERFACE
    // -----------------------------------------------
    interface IMapper<TSource, TDestination>
    {
        TDestination Map(TSource source);
    }

    class ProductToStringMapper : IMapper<Product, string>
    {
        public string Map(Product source) => $"{source.Name}: {source.Price:C}";
    }

    // -----------------------------------------------
    // 4) GENERIC STACK ORNEGI
    // -----------------------------------------------
    class SimpleStack<T>
    {
        private List<T> _items = new List<T>();

        public int Count => _items.Count;
        public bool IsEmpty => _items.Count == 0;

        public void Push(T item) => _items.Add(item);

        public T Pop()
        {
            if (IsEmpty) throw new InvalidOperationException("Stack is empty!");
            T item = _items[_items.Count - 1];
            _items.RemoveAt(_items.Count - 1);
            return item;
        }

        public T Peek()
        {
            if (IsEmpty) throw new InvalidOperationException("Stack is empty!");
            return _items[_items.Count - 1];
        }
    }

    class Lesson14_Generics
    {
        // -----------------------------------------------
        // GENERIC METOTLAR
        // -----------------------------------------------
        static T FindMax<T>(T a, T b) where T : IComparable<T>
        {
            return a.CompareTo(b) >= 0 ? a : b;
        }

        static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

        static void PrintAll<T>(IEnumerable<T> items, string title = "Items")
        {
            Console.WriteLine($"  {title}:");
            foreach (T item in items)
                Console.WriteLine($"    {item}");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("=== LESSON 14: Generics ===\n");

            // 1) Generic sinif
            Console.WriteLine("--- Generic Class ---");
            Box<int> intBox = new Box<int>();
            intBox.Put(42);
            Console.WriteLine($"  Got: {intBox.Get()}");

            Box<string> strBox = new Box<string>();
            strBox.Put("Hello Generics!");
            Console.WriteLine($"  Got: {strBox.Get()}");

            // Pair
            var pair = new Pair<string, int>("Age", 25);
            Console.WriteLine($"  Pair: {pair}");

            // 2) Generic metotlar
            Console.WriteLine("\n--- Generic Methods ---");
            Console.WriteLine($"  Max(10, 20): {FindMax(10, 20)}");
            Console.WriteLine($"  Max(\"Apple\", \"Banana\"): {FindMax("Apple", "Banana")}");

            int x = 5, y = 10;
            Console.WriteLine($"  Before swap: x={x}, y={y}");
            Swap(ref x, ref y);
            Console.WriteLine($"  After swap:  x={x}, y={y}");

            // 3) Constraint'li generic
            Console.WriteLine("\n--- Constrained Generics (Repository) ---");

            var productRepo = new Repository<Product>();
            productRepo.Add(new Product(1, "Laptop", 999.99m));
            productRepo.Add(new Product(2, "Mouse", 29.99m));
            productRepo.Add(new Product(3, "Keyboard", 79.99m));

            var found = productRepo.FindById(2);
            Console.WriteLine($"  Found: {found}");
            PrintAll(productRepo.GetAll(), "All Products");

            var empRepo = new Repository<Employee>();
            empRepo.Add(new Employee(1, "Alice", "Engineering"));
            empRepo.Add(new Employee(2, "Bob", "Marketing"));
            PrintAll(empRepo.GetAll(), "All Employees");

            // 4) Generic Stack
            Console.WriteLine("\n--- Generic Stack ---");
            var stack = new SimpleStack<double>();
            stack.Push(1.1); stack.Push(2.2); stack.Push(3.3);
            Console.WriteLine($"  Peek: {stack.Peek()}");
            Console.WriteLine($"  Pop: {stack.Pop()}");
            Console.WriteLine($"  Pop: {stack.Pop()}");
            Console.WriteLine($"  Count: {stack.Count}");

            // 5) Generic interface
            Console.WriteLine("\n--- Generic Interface ---");
            IMapper<Product, string> mapper = new ProductToStringMapper();
            foreach (var p in productRepo.GetAll())
                Console.WriteLine($"  Mapped: {mapper.Map(p)}");

            // -----------------------------------------------
            // PRATIK
            // -----------------------------------------------
            // 1. Generic Queue<T> sinifi yapin (Enqueue, Dequeue, Peek)
            // 2. Generic Cache<TKey, TValue> sinifi olusturun
            // 3. IComparable<T> implement eden bir "Score" sinifi yapin
            // 4. Generic extension method (ornegin: list.Shuffle())

            Console.WriteLine("\nLesson 14 completed! Press any key to exit...");
            Console.ReadKey();
        }
    }
}
