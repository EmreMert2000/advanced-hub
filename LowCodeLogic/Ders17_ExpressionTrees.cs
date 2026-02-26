// =============================================================
// DERS 17 - EXPRESSION TREES VE DINAMIK KOD
// =============================================================
// Bu derste:
// - Expression Tree nedir
// - Expression sinifi ile dinamik ifade olusturma
// - Lambda -> Expression donusumu
// - Dinamik filtreler ve sorgular
// - Low-code motorlari icin dinamik kural motoru
// =============================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LowCodeLogic
{
    // Ornek veri sinifi
    class Product
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public override string ToString() =>
            $"{Name} ({Category}) - {Price:C} [Stock: {Stock}]";
    }

    // -----------------------------------------------
    // DINAMIK FILTRE BUILDER
    // -----------------------------------------------
    // Low-code platformlarinda kullanici arayuzunden gelen
    // filtreleri dinamik olarak Expression Tree'ye cevirir

    class DynamicFilterBuilder<T>
    {
        private readonly ParameterExpression _parameter;
        private Expression _currentFilter;

        public DynamicFilterBuilder()
        {
            _parameter = Expression.Parameter(typeof(T), "item");
        }

        // Property == deger filtresi
        public DynamicFilterBuilder<T> WhereEquals(string propertyName, object value)
        {
            var property = Expression.Property(_parameter, propertyName);
            var constant = Expression.Constant(value);
            var converted = Expression.Convert(constant, property.Type);
            var comparison = Expression.Equal(property, converted);
            AddCondition(comparison);
            return this;
        }

        // Property > deger filtresi
        public DynamicFilterBuilder<T> WhereGreaterThan(string propertyName, object value)
        {
            var property = Expression.Property(_parameter, propertyName);
            var constant = Expression.Constant(Convert.ChangeType(value, property.Type));
            var comparison = Expression.GreaterThan(property, constant);
            AddCondition(comparison);
            return this;
        }

        // Property < deger filtresi
        public DynamicFilterBuilder<T> WhereLessThan(string propertyName, object value)
        {
            var property = Expression.Property(_parameter, propertyName);
            var constant = Expression.Constant(Convert.ChangeType(value, property.Type));
            var comparison = Expression.LessThan(property, constant);
            AddCondition(comparison);
            return this;
        }

        // String Contains filtresi
        public DynamicFilterBuilder<T> WhereContains(string propertyName, string value)
        {
            var property = Expression.Property(_parameter, propertyName);
            var method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            var constant = Expression.Constant(value);
            var call = Expression.Call(property, method, constant);
            AddCondition(call);
            return this;
        }

        private void AddCondition(Expression condition)
        {
            _currentFilter = _currentFilter == null
                ? condition
                : Expression.AndAlso(_currentFilter, condition);
        }

        // Derlenebilir filtre uret
        public Func<T, bool> Build()
        {
            if (_currentFilter == null)
                return _ => true;

            var lambda = Expression.Lambda<Func<T, bool>>(_currentFilter, _parameter);
            Console.WriteLine($"  Generated: {lambda}");
            return lambda.Compile();
        }
    }

    // -----------------------------------------------
    // DINAMIK KURAL MOTORU (Rule Engine)
    // -----------------------------------------------
    class Rule
    {
        public string PropertyName { get; set; }
        public string Operator { get; set; }    // ==, >, <, contains
        public object Value { get; set; }
        public string LogicalOp { get; set; }   // AND, OR

        public override string ToString() =>
            $"{PropertyName} {Operator} {Value}";
    }

    class RuleEngine<T>
    {
        public Func<T, bool> BuildFromRules(List<Rule> rules)
        {
            if (rules == null || rules.Count == 0)
                return _ => true;

            var param = Expression.Parameter(typeof(T), "x");
            Expression combined = null;

            foreach (var rule in rules)
            {
                var property = Expression.Property(param, rule.PropertyName);
                Expression condition;

                switch (rule.Operator)
                {
                    case "==":
                        var eqVal = Expression.Constant(Convert.ChangeType(rule.Value, property.Type));
                        condition = Expression.Equal(property, eqVal);
                        break;
                    case ">":
                        var gtVal = Expression.Constant(Convert.ChangeType(rule.Value, property.Type));
                        condition = Expression.GreaterThan(property, gtVal);
                        break;
                    case "<":
                        var ltVal = Expression.Constant(Convert.ChangeType(rule.Value, property.Type));
                        condition = Expression.LessThan(property, ltVal);
                        break;
                    case "contains":
                        var method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                        var strVal = Expression.Constant(rule.Value.ToString());
                        condition = Expression.Call(property, method, strVal);
                        break;
                    default:
                        throw new NotSupportedException($"Operator {rule.Operator} not supported");
                }

                if (combined == null)
                {
                    combined = condition;
                }
                else
                {
                    combined = rule.LogicalOp == "OR"
                        ? Expression.OrElse(combined, condition)
                        : Expression.AndAlso(combined, condition);
                }
            }

            var lambda = Expression.Lambda<Func<T, bool>>(combined, param);
            Console.WriteLine($"  Rule Expression: {lambda}");
            return lambda.Compile();
        }
    }

    class Lesson17_ExpressionTrees
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== LESSON 17: Expression Trees ===\n");

            // Ornek veri
            var products = new List<Product>
            {
                new Product { Name="Laptop",    Category="Electronics", Price=999.99m, Stock=15 },
                new Product { Name="Mouse",     Category="Electronics", Price=29.99m,  Stock=100},
                new Product { Name="Desk",      Category="Furniture",   Price=249.99m, Stock=30 },
                new Product { Name="Chair",     Category="Furniture",   Price=199.99m, Stock=25 },
                new Product { Name="Keyboard",  Category="Electronics", Price=79.99m,  Stock=60 },
                new Product { Name="Lamp",      Category="Furniture",   Price=49.99m,  Stock=80 },
                new Product { Name="Monitor",   Category="Electronics", Price=399.99m, Stock=20 },
            };

            // -----------------------------------------------
            // 1) EXPRESSION TREE TEMELI
            // -----------------------------------------------
            Console.WriteLine("--- Expression Tree Basics ---");

            // Normal lambda
            Func<int, int, int> addFunc = (a, b) => a + b;

            // Ayni islemi Expression Tree olarak
            var paramA = Expression.Parameter(typeof(int), "a");
            var paramB = Expression.Parameter(typeof(int), "b");
            var addExpr = Expression.Add(paramA, paramB);
            var addLambda = Expression.Lambda<Func<int, int, int>>(addExpr, paramA, paramB);

            Console.WriteLine($"  Expression: {addLambda}");
            Console.WriteLine($"  Body:       {addLambda.Body}");
            Console.WriteLine($"  Params:     {string.Join(", ", addLambda.Parameters)}");

            // Derle ve calistir
            var compiledAdd = addLambda.Compile();
            Console.WriteLine($"  Result: {compiledAdd(10, 20)}");

            // -----------------------------------------------
            // 2) LAMBDA -> EXPRESSION DONUSUMU
            // -----------------------------------------------
            Console.WriteLine("\n--- Lambda to Expression ---");

            // Expression<Func<...>> ile lambda'yi agac olarak yakalayabiliriz
            Expression<Func<Product, bool>> expensiveExpr = p => p.Price > 100;

            Console.WriteLine($"  Expression: {expensiveExpr}");
            Console.WriteLine($"  Body type:  {expensiveExpr.Body.NodeType}");

            // Derleyip kullanma
            var expensiveFilter = expensiveExpr.Compile();
            var expensive = products.Where(expensiveFilter).ToList();
            Console.WriteLine("  Expensive products:");
            expensive.ForEach(p => Console.WriteLine($"    {p}"));

            // -----------------------------------------------
            // 3) DINAMIK FILTRE BUILDER
            // -----------------------------------------------
            Console.WriteLine("\n--- Dynamic Filter Builder ---");

            // Senaryo: Kullanici arayuzunden gelen filtreler
            var filter = new DynamicFilterBuilder<Product>()
                .WhereEquals("Category", "Electronics")
                .WhereGreaterThan("Price", 50m)
                .Build();

            var filtered = products.Where(filter).ToList();
            Console.WriteLine("  Filtered (Electronics, Price > 50):");
            filtered.ForEach(p => Console.WriteLine($"    {p}"));

            // Baska bir filtre
            Console.WriteLine();
            var filter2 = new DynamicFilterBuilder<Product>()
                .WhereContains("Name", "a")
                .WhereLessThan("Stock", 50)
                .Build();

            var filtered2 = products.Where(filter2).ToList();
            Console.WriteLine("  Filtered (Name contains 'a', Stock < 50):");
            filtered2.ForEach(p => Console.WriteLine($"    {p}"));

            // -----------------------------------------------
            // 4) KURAL MOTORU (Rule Engine)
            // -----------------------------------------------
            Console.WriteLine("\n--- Rule Engine ---");

            // JSON/veritabanindan gelen kurallar gibi dusunun
            var rules = new List<Rule>
            {
                new Rule { PropertyName="Category", Operator="==", Value="Furniture", LogicalOp="AND" },
                new Rule { PropertyName="Price", Operator="<", Value=200m, LogicalOp="AND" },
            };

            Console.WriteLine("  Rules:");
            rules.ForEach(r => Console.WriteLine($"    {r}"));

            var engine = new RuleEngine<Product>();
            var ruleFilter = engine.BuildFromRules(rules);

            var ruleResults = products.Where(ruleFilter).ToList();
            Console.WriteLine("  Results:");
            ruleResults.ForEach(p => Console.WriteLine($"    {p}"));

            /*
                LOW-CODE BAGLANTISI:
                Expression Tree'ler low-code platformlarinda sunlarin temelini olusturur:
                1. Kullanici arayuzunden filtre/kural olusturma
                2. Dinamik sorgular (veritabani sorgulari dahil - EF Core/LINQ)
                3. Is kurali motorlari (business rule engines)
                4. Kosullu akis yonetimi (conditional flow)
                Ders 18'de bunlari bir workflow motoru icinde kullanacagiz!
            */

            Console.WriteLine("\nLesson 17 completed! Press any key to exit...");
            Console.ReadKey();
        }
    }
}
