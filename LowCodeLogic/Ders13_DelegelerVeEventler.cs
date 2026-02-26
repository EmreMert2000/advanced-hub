// =============================================================
// DERS 13 - DELEGELER, EVENTLER VE LAMBDA IFADELERI
// =============================================================
// Bu derste:
// - Delegate tanimlama ve kullanma
// - Action, Func, Predicate
// - Lambda ifadeleri (=> ok notasyonu)
// - Event tanimlama ve tetikleme
// - Observer pattern temeli
// =============================================================

using System;
using System.Collections.Generic;

namespace LowCodeLogic
{
    // -----------------------------------------------
    // 1) DELEGATE TANIMLAMA
    // -----------------------------------------------
    // Delegate: Metotlara referans tutan tip-guvenli fonksiyon isaretcisi
    // Bir metodu degisken gibi tasiyabilir, parametre olarak gecirebilirsiniz

    delegate int MathOperation(int a, int b);
    delegate void LogHandler(string message);
    delegate bool Validator(string input);

    // -----------------------------------------------
    // 2) EVENT ORNEGI (Observer Pattern)
    // -----------------------------------------------
    class StockMarket
    {
        // Event delegate tanimlama
        public delegate void PriceChangedHandler(string stock, decimal oldPrice, decimal newPrice);

        // Event tanimlama
        public event PriceChangedHandler OnPriceChanged;

        private Dictionary<string, decimal> _prices = new Dictionary<string, decimal>();

        public void UpdatePrice(string stock, decimal newPrice)
        {
            decimal oldPrice = _prices.ContainsKey(stock) ? _prices[stock] : 0;
            _prices[stock] = newPrice;

            // Event'i tetikle (null kontrolu ile)
            OnPriceChanged?.Invoke(stock, oldPrice, newPrice);
        }

        public decimal GetPrice(string stock) =>
            _prices.ContainsKey(stock) ? _prices[stock] : 0;
    }

    // Event dinleyici siniflar
    class TradeBot
    {
        public string Name { get; }
        public TradeBot(string name) { Name = name; }

        public void OnPriceChanged(string stock, decimal oldP, decimal newP)
        {
            string direction = newP > oldP ? "UP" : "DOWN";
            Console.WriteLine($"  [{Name}] {stock}: {oldP:C} -> {newP:C} ({direction})");
        }
    }

    // -----------------------------------------------
    // 3) GENERIC EVENT (EventHandler<T>)
    // -----------------------------------------------
    class OrderEventArgs : EventArgs
    {
        public string ProductName { get; }
        public int Quantity { get; }
        public decimal TotalPrice { get; }

        public OrderEventArgs(string product, int qty, decimal price)
        {
            ProductName = product; Quantity = qty; TotalPrice = price;
        }
    }

    class OnlineStore
    {
        // .NET standart event pattern
        public event EventHandler<OrderEventArgs> OrderPlaced;

        public void PlaceOrder(string product, int qty, decimal unitPrice)
        {
            decimal total = qty * unitPrice;
            Console.WriteLine($"  Order: {qty}x {product} = {total:C}");

            // Event tetikle
            OrderPlaced?.Invoke(this, new OrderEventArgs(product, qty, total));
        }
    }

    class Lesson13_DelegatesAndEvents
    {
        // Delegate'e atanacak metotlar
        static int Add(int a, int b) => a + b;
        static int Subtract(int a, int b) => a - b;
        static int Multiply(int a, int b) => a * b;

        static void LogToConsole(string msg) => Console.WriteLine($"  [CONSOLE] {msg}");
        static void LogToFile(string msg) => Console.WriteLine($"  [FILE] {msg}");

        static void Main(string[] args)
        {
            Console.WriteLine("=== LESSON 13: Delegates, Events & Lambda ===\n");

            // -----------------------------------------------
            // 1) DELEGATE KULLANIMI
            // -----------------------------------------------
            Console.WriteLine("--- Delegates ---");

            MathOperation operation;

            operation = Add;
            Console.WriteLine($"Add(10, 5)      = {operation(10, 5)}");

            operation = Subtract;
            Console.WriteLine($"Subtract(10, 5) = {operation(10, 5)}");

            operation = Multiply;
            Console.WriteLine($"Multiply(10, 5) = {operation(10, 5)}");

            // Delegate'i parametre olarak gecme
            Console.WriteLine($"\nCalculate(Add, 20, 8) = {Calculate(Add, 20, 8)}");
            Console.WriteLine($"Calculate(Multiply, 20, 8) = {Calculate(Multiply, 20, 8)}");

            // Multicast delegate: Birden fazla metot atanabilir
            Console.WriteLine("\n--- Multicast Delegate ---");
            LogHandler logger = LogToConsole;
            logger += LogToFile;               // iki metot da calisir
            logger("Application started");

            // -----------------------------------------------
            // 2) LAMBDA IFADELERI
            // -----------------------------------------------
            Console.WriteLine("\n--- Lambda Expressions ---");

            // Lambda: Anonim (isimsiz) fonksiyon tanimlama
            // (parametreler) => ifade

            MathOperation addLambda = (a, b) => a + b;
            MathOperation multiplyLambda = (a, b) => a * b;

            Console.WriteLine($"Lambda Add: {addLambda(7, 3)}");
            Console.WriteLine($"Lambda Mul: {multiplyLambda(7, 3)}");

            // Cok satirlik lambda
            MathOperation complexOp = (a, b) =>
            {
                int result = a * b + a + b;
                return result;
            };
            Console.WriteLine($"Complex: {complexOp(5, 3)}");

            // -----------------------------------------------
            // 3) ACTION, FUNC, PREDICATE
            // -----------------------------------------------
            Console.WriteLine("\n--- Action, Func, Predicate ---");

            // Action: Void donen delegate (deger dondurmez)
            // Action<T1, T2, ...>
            Action<string> printMsg = msg => Console.WriteLine($"  Action: {msg}");
            printMsg("Hello from Action!");

            Action<string, int> printRepeat = (msg, count) =>
            {
                for (int i = 0; i < count; i++) Console.Write($"  {msg}");
                Console.WriteLine();
            };
            printRepeat("Hi! ", 3);

            // Func: Deger donduren delegate
            // Func<T1, T2, ..., TResult> (son tip donus tipi)
            Func<int, int, int> addFunc = (a, b) => a + b;
            Func<double, double> square = x => x * x;
            Func<string, string> toUpper = s => s.ToUpper();
            Func<int, bool> isPositive = n => n > 0;

            Console.WriteLine($"Func Add: {addFunc(4, 6)}");
            Console.WriteLine($"Square(7): {square(7)}");
            Console.WriteLine($"ToUpper: {toUpper("hello")}");

            // Predicate: bool donduren delegate (test/filtre icin)
            // Predicate<T> = Func<T, bool> ile ayni
            Predicate<int> isEven = n => n % 2 == 0;
            Predicate<string> isLong = s => s.Length > 5;

            Console.WriteLine($"IsEven(4): {isEven(4)}");
            Console.WriteLine($"IsLong(\"Hi\"): {isLong("Hi")}");

            // List ile kullanimi
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            List<int> evens = numbers.FindAll(isEven);
            Console.WriteLine($"\nEven numbers: {string.Join(", ", evens)}");

            numbers.ForEach(n => Console.Write($"{n * n} "));
            Console.WriteLine("<- squared");

            // -----------------------------------------------
            // 4) HOF - Higher Order Functions
            // -----------------------------------------------
            Console.WriteLine("\n--- Higher Order Functions ---");

            // Fonksiyon donduren fonksiyon
            Func<int, Func<int, int>> multiplier = x => y => x * y;

            var double_ = multiplier(2);
            var triple = multiplier(3);

            Console.WriteLine($"Double(5): {double_(5)}");
            Console.WriteLine($"Triple(5): {triple(5)}");

            // -----------------------------------------------
            // 5) EVENT KULLANIMI
            // -----------------------------------------------
            Console.WriteLine("\n--- Events ---");

            StockMarket market = new StockMarket();

            TradeBot bot1 = new TradeBot("AlphaBot");
            TradeBot bot2 = new TradeBot("BetaBot");

            // Event'e abone ol (subscribe)
            market.OnPriceChanged += bot1.OnPriceChanged;
            market.OnPriceChanged += bot2.OnPriceChanged;

            // Lambda ile de abone olunabilir
            market.OnPriceChanged += (stock, oldP, newP) =>
                Console.WriteLine($"  [Logger] {stock} price changed!");

            market.UpdatePrice("AAPL", 150.0m);
            market.UpdatePrice("AAPL", 155.5m);

            // Abonelikten cik (unsubscribe)
            market.OnPriceChanged -= bot2.OnPriceChanged;
            Console.WriteLine("\n  (BetaBot unsubscribed)");
            market.UpdatePrice("AAPL", 148.0m);

            // 6) Generic Event Pattern
            Console.WriteLine("\n--- Generic EventHandler ---");

            OnlineStore store = new OnlineStore();

            store.OrderPlaced += (sender, e) =>
                Console.WriteLine($"  [Email] Order confirmed: {e.Quantity}x {e.ProductName}");

            store.OrderPlaced += (sender, e) =>
                Console.WriteLine($"  [SMS] Total: {e.TotalPrice:C}");

            store.PlaceOrder("Laptop", 1, 999.99m);
            store.PlaceOrder("Mouse", 2, 29.99m);

            // -----------------------------------------------
            // PRATIK
            // -----------------------------------------------
            // 1. Func<int, int, int> ile hesap makinesi yapin (+, -, *, /)
            // 2. Bir event sistemi yapin: Button.OnClick event'i
            // 3. List<T>.Sort() icin Comparison<T> delegate kullanin
            // 4. Pipeline pattern: Func zincirleme ile veri donusumu

            Console.WriteLine("\nLesson 13 completed! Press any key to exit...");
            Console.ReadKey();
        }

        // Delegate parametreli metot
        static int Calculate(MathOperation op, int a, int b)
        {
            return op(a, b);
        }
    }
}
