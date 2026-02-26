// =============================================================
// DERS 16 - REFLECTION VE ATTRIBUTE
// =============================================================
// Bu derste:
// - Reflection nedir: Calisma zamaninda tip bilgisi okuma
// - Type sinifi ve uyeleri
// - MethodInfo, PropertyInfo, FieldInfo
// - Custom Attribute tanimlama
// - Attribute okuma (runtime)
// - Dinamik nesne olusturma (Activator)
// Bu kavramlar low-code motorlari icin kritik oneme sahiptir!
// =============================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LowCodeLogic
{
    // -----------------------------------------------
    // 1) CUSTOM ATTRIBUTE TANIMLAMA
    // -----------------------------------------------
    // Attribute: Koda metadata (ust veri) eklemek icin kullanilir
    // Sinif, metot, property vb. uzerine [Attribute] olarak yazilir

    [AttributeUsage(AttributeTargets.Class)]
    class NodeInfoAttribute : Attribute
    {
        public string DisplayName { get; }
        public string Category { get; }
        public string Description { get; }

        public NodeInfoAttribute(string displayName, string category, string description = "")
        {
            DisplayName = displayName;
            Category = category;
            Description = description;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    class InputPortAttribute : Attribute
    {
        public string Label { get; }
        public bool Required { get; }

        public InputPortAttribute(string label, bool required = true)
        {
            Label = label;
            Required = required;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    class OutputPortAttribute : Attribute
    {
        public string Label { get; }
        public OutputPortAttribute(string label) { Label = label; }
    }

    // -----------------------------------------------
    // 2) ATTRIBUTE KULLANAN SINIFLAR
    // -----------------------------------------------

    [NodeInfo("Add Numbers", "Math", "Adds two numbers together")]
    class AddNode
    {
        [InputPort("First Number")]
        public double InputA { get; set; }

        [InputPort("Second Number")]
        public double InputB { get; set; }

        [OutputPort("Result")]
        public double Result => InputA + InputB;

        public void Execute()
        {
            Console.WriteLine($"  {InputA} + {InputB} = {Result}");
        }
    }

    [NodeInfo("Text Concat", "String", "Concatenates two strings")]
    class ConcatNode
    {
        [InputPort("Text A")]
        public string TextA { get; set; } = "";

        [InputPort("Text B", required: false)]
        public string TextB { get; set; } = "";

        [OutputPort("Combined")]
        public string Combined => TextA + TextB;

        public void Execute()
        {
            Console.WriteLine($"  \"{TextA}\" + \"{TextB}\" = \"{Combined}\"");
        }
    }

    // Basit bir sinif (Reflection ornekleri icin)
    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        private string _secret = "hidden";

        public Person() { }
        public Person(string name, int age) { Name = name; Age = age; }

        public void Greet() => Console.WriteLine($"  Hello, I'm {Name}!");
        public string GetInfo() => $"{Name}, Age: {Age}";
        private void PrivateMethod() => Console.WriteLine("  (private method called)");
    }

    class Lesson16_ReflectionAndAttribute
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== LESSON 16: Reflection & Attributes ===\n");

            // -----------------------------------------------
            // 1) TYPE BILGISI OKUMA
            // -----------------------------------------------
            Console.WriteLine("--- Type Information ---");

            Type personType = typeof(Person);
            // veya: Type personType = new Person().GetType();

            Console.WriteLine($"  Name:      {personType.Name}");
            Console.WriteLine($"  FullName:  {personType.FullName}");
            Console.WriteLine($"  Namespace: {personType.Namespace}");
            Console.WriteLine($"  IsClass:   {personType.IsClass}");
            Console.WriteLine($"  IsAbstract: {personType.IsAbstract}");
            Console.WriteLine($"  BaseType:  {personType.BaseType?.Name}");

            // -----------------------------------------------
            // 2) PROPERTY BILGILERI
            // -----------------------------------------------
            Console.WriteLine("\n--- Properties ---");

            PropertyInfo[] properties = personType.GetProperties();
            foreach (PropertyInfo prop in properties)
            {
                Console.WriteLine($"  {prop.Name}: {prop.PropertyType.Name} " +
                    $"(CanRead: {prop.CanRead}, CanWrite: {prop.CanWrite})");
            }

            // -----------------------------------------------
            // 3) METOT BILGILERI
            // -----------------------------------------------
            Console.WriteLine("\n--- Methods ---");

            MethodInfo[] methods = personType.GetMethods(
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            foreach (MethodInfo method in methods)
            {
                var parameters = method.GetParameters();
                string paramStr = string.Join(", ",
                    parameters.Select(p => $"{p.ParameterType.Name} {p.Name}"));
                Console.WriteLine($"  {method.ReturnType.Name} {method.Name}({paramStr})");
            }

            // -----------------------------------------------
            // 4) DINAMIK NESNE OLUSTURMA
            // -----------------------------------------------
            Console.WriteLine("\n--- Dynamic Object Creation ---");

            // Activator ile runtime'da nesne olusturma
            object dynamicPerson = Activator.CreateInstance(personType);

            // Property'ye deger atama (reflection ile)
            personType.GetProperty("Name").SetValue(dynamicPerson, "Alice");
            personType.GetProperty("Age").SetValue(dynamicPerson, 25);

            // Metot cagirma (reflection ile)
            MethodInfo greetMethod = personType.GetMethod("Greet");
            greetMethod.Invoke(dynamicPerson, null);

            MethodInfo getInfoMethod = personType.GetMethod("GetInfo");
            string info = (string)getInfoMethod.Invoke(dynamicPerson, null);
            Console.WriteLine($"  GetInfo: {info}");

            // Private metot cagirma
            MethodInfo privateMethod = personType.GetMethod("PrivateMethod",
                BindingFlags.NonPublic | BindingFlags.Instance);
            privateMethod?.Invoke(dynamicPerson, null);

            // -----------------------------------------------
            // 5) CUSTOM ATTRIBUTE OKUMA
            // -----------------------------------------------
            Console.WriteLine("\n--- Reading Custom Attributes ---");

            // NodeInfo attribute'unu oku
            Type addNodeType = typeof(AddNode);
            var nodeAttr = addNodeType.GetCustomAttribute<NodeInfoAttribute>();

            if (nodeAttr != null)
            {
                Console.WriteLine($"  Display Name: {nodeAttr.DisplayName}");
                Console.WriteLine($"  Category:     {nodeAttr.Category}");
                Console.WriteLine($"  Description:  {nodeAttr.Description}");
            }

            // Input/Output portlarini oku
            Console.WriteLine("\n  Ports:");
            foreach (var prop in addNodeType.GetProperties())
            {
                var input = prop.GetCustomAttribute<InputPortAttribute>();
                var output = prop.GetCustomAttribute<OutputPortAttribute>();

                if (input != null)
                    Console.WriteLine($"    INPUT:  {input.Label} (Required: {input.Required}) -> {prop.Name}");
                if (output != null)
                    Console.WriteLine($"    OUTPUT: {output.Label} -> {prop.Name}");
            }

            // -----------------------------------------------
            // 6) OTOMATIK NODE KESFETME (Auto-Discovery)
            // -----------------------------------------------
            Console.WriteLine("\n--- Auto-Discovery (Node Scanner) ---");

            // Bu assembly'deki tum NodeInfo attribute'une sahip siniflari bul
            var nodeTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.GetCustomAttribute<NodeInfoAttribute>() != null)
                .ToList();

            Console.WriteLine($"  Found {nodeTypes.Count} node types:");
            foreach (Type nodeType in nodeTypes)
            {
                var attr = nodeType.GetCustomAttribute<NodeInfoAttribute>();
                Console.WriteLine($"    [{attr.Category}] {attr.DisplayName}: {attr.Description}");

                // Dinamik olarak olustur ve calistir
                dynamic node = Activator.CreateInstance(nodeType);

                // Input portlarini doldur
                foreach (var prop in nodeType.GetProperties())
                {
                    var inputAttr = prop.GetCustomAttribute<InputPortAttribute>();
                    if (inputAttr != null && prop.CanWrite)
                    {
                        if (prop.PropertyType == typeof(double))
                            prop.SetValue(node, 10.0);
                        else if (prop.PropertyType == typeof(string))
                            prop.SetValue(node, "Hello ");
                    }
                }

                // Execute metotunu dynamik cagir
                nodeType.GetMethod("Execute")?.Invoke(node, null);
            }

            /*
                BU KONU NEDEN ONEMLI?
                Low-code motorlarinda:
                - Node'lar attribute ile isaretlenir
                - Reflection ile otomatik kesfedilir
                - Input/Output portlari attribute ile tanimlanir
                - Runtime'da dinamik olarak olusturulur ve baglanir
                - Ders 18'de bu teknikleri kullanarak gercek bir motor yapacagiz!
            */

            Console.WriteLine("\nLesson 16 completed! Press any key to exit...");
            Console.ReadKey();
        }
    }
}
