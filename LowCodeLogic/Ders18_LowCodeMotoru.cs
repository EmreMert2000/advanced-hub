// =============================================================
// DERS 18 - LOW-CODE MOTORU: WORKFLOW ENGINE
// =============================================================
// Bu derste onceki tum derslerde ogrendiklerimizi birlestiriyoruz:
// - Interface/Abstract  -> Node sistemi (Ders 10)
// - Generics           -> Tip-guvenli portlar (Ders 14)
// - Reflection         -> Node otomatik kesfetme (Ders 16)
// - Attribute          -> Node metadata (Ders 16)
// - Expression Trees   -> Dinamik kosullar (Ders 17)
// - Events/Delegates   -> Olay yonetimi (Ders 13)
// - Collections        -> Graph yapisi (Ders 6)
//
// SONUC: Blok tabanli bir is akisi (workflow) motoru
// Drag & drop arayuzlerinin arkasindaki mantik budur!
// =============================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LowCodeLogic
{
    // =============================================================
    // 1) NODE SISTEMI - Temel Yapilar
    // =============================================================

    // Her node'un bir attribute'u olacak
    [AttributeUsage(AttributeTargets.Class)]
    class WorkflowNodeAttribute : Attribute
    {
        public string Name { get; }
        public string Category { get; }
        public string Icon { get; }
        public WorkflowNodeAttribute(string name, string category, string icon = "⚙")
        { Name = name; Category = category; Icon = icon; }
    }

    // Port (giris/cikis noktasi) tanimlari
    class Port
    {
        public string Name { get; set; }
        public Type DataType { get; set; }
        public object Value { get; set; }
        public bool IsConnected { get; set; }

        public override string ToString() => $"{Name}({DataType.Name})={Value}";
    }

    // -----------------------------------------------
    // TEMEL NODE INTERFACE'I
    // -----------------------------------------------
    interface IWorkflowNode
    {
        string Id { get; }
        string DisplayName { get; }
        Dictionary<string, Port> Inputs { get; }
        Dictionary<string, Port> Outputs { get; }
        NodeStatus Status { get; }

        void Execute();
        void Reset();
    }

    enum NodeStatus { Idle, Running, Completed, Error }

    // -----------------------------------------------
    // ABSTRACT BASE NODE
    // -----------------------------------------------
    abstract class BaseNode : IWorkflowNode
    {
        public string Id { get; private set; }
        public string DisplayName { get; protected set; }
        public Dictionary<string, Port> Inputs { get; } = new Dictionary<string, Port>();
        public Dictionary<string, Port> Outputs { get; } = new Dictionary<string, Port>();
        public NodeStatus Status { get; protected set; } = NodeStatus.Idle;

        protected BaseNode()
        {
            Id = Guid.NewGuid().ToString("N").Substring(0, 8);
            var attr = GetType().GetCustomAttribute<WorkflowNodeAttribute>();
            DisplayName = attr?.Name ?? GetType().Name;
        }

        protected void AddInput(string name, Type type, object defaultValue = null)
        {
            Inputs[name] = new Port { Name = name, DataType = type, Value = defaultValue };
        }

        protected void AddOutput(string name, Type type)
        {
            Outputs[name] = new Port { Name = name, DataType = type };
        }

        protected T GetInput<T>(string name) =>
            Inputs.ContainsKey(name) ? (T)Convert.ChangeType(Inputs[name].Value, typeof(T)) : default;

        protected void SetOutput(string name, object value)
        {
            if (Outputs.ContainsKey(name)) Outputs[name].Value = value;
        }

        public abstract void Execute();

        public void Reset()
        {
            Status = NodeStatus.Idle;
            foreach (var o in Outputs.Values) o.Value = null;
        }

        public override string ToString()
        {
            string icon = GetType().GetCustomAttribute<WorkflowNodeAttribute>()?.Icon ?? "?";
            return $"{icon} [{DisplayName}] ({Status})";
        }
    }

    // =============================================================
    // 2) HAZIR NODE KUTUPHANESI (Built-in Nodes)
    // =============================================================

    // --- MATEMATIK NODE'LARI ---

    [WorkflowNode("Add", "Math", "➕")]
    class AddNode : BaseNode
    {
        public AddNode()
        {
            AddInput("A", typeof(double), 0.0);
            AddInput("B", typeof(double), 0.0);
            AddOutput("Result", typeof(double));
        }

        public override void Execute()
        {
            Status = NodeStatus.Running;
            double a = GetInput<double>("A");
            double b = GetInput<double>("B");
            SetOutput("Result", a + b);
            Status = NodeStatus.Completed;
        }
    }

    [WorkflowNode("Multiply", "Math", "✖")]
    class MultiplyNode : BaseNode
    {
        public MultiplyNode()
        {
            AddInput("A", typeof(double), 0.0);
            AddInput("B", typeof(double), 0.0);
            AddOutput("Result", typeof(double));
        }

        public override void Execute()
        {
            Status = NodeStatus.Running;
            double result = GetInput<double>("A") * GetInput<double>("B");
            SetOutput("Result", result);
            Status = NodeStatus.Completed;
        }
    }

    [WorkflowNode("Compare", "Logic", "⚖")]
    class CompareNode : BaseNode
    {
        public CompareNode()
        {
            AddInput("A", typeof(double), 0.0);
            AddInput("B", typeof(double), 0.0);
            AddInput("Operator", typeof(string), ">");
            AddOutput("Result", typeof(bool));
        }

        public override void Execute()
        {
            Status = NodeStatus.Running;
            double a = GetInput<double>("A");
            double b = GetInput<double>("B");
            string op = GetInput<string>("Operator");

            bool result = op switch
            {
                ">"  => a > b,
                "<"  => a < b,
                ">=" => a >= b,
                "<=" => a <= b,
                "==" => Math.Abs(a - b) < 0.0001,
                "!=" => Math.Abs(a - b) >= 0.0001,
                _ => false
            };

            SetOutput("Result", result);
            Status = NodeStatus.Completed;
        }
    }

    // --- METIN NODE'LARI ---

    [WorkflowNode("Text Template", "String", "📝")]
    class TextTemplateNode : BaseNode
    {
        public TextTemplateNode()
        {
            AddInput("Template", typeof(string), "Hello, {name}!");
            AddInput("name", typeof(string), "World");
            AddOutput("Result", typeof(string));
        }

        public override void Execute()
        {
            Status = NodeStatus.Running;
            string template = GetInput<string>("Template");
            string name = GetInput<string>("name");
            string result = template.Replace("{name}", name);
            SetOutput("Result", result);
            Status = NodeStatus.Completed;
        }
    }

    // --- AKIS NODE'LARI ---

    [WorkflowNode("Print", "Output", "🖨")]
    class PrintNode : BaseNode
    {
        public PrintNode()
        {
            AddInput("Message", typeof(string), "");
        }

        public override void Execute()
        {
            Status = NodeStatus.Running;
            string msg = GetInput<string>("Message");
            Console.WriteLine($"    >>> OUTPUT: {msg}");
            Status = NodeStatus.Completed;
        }
    }

    [WorkflowNode("If/Else", "Flow", "🔀")]
    class IfElseNode : BaseNode
    {
        public IfElseNode()
        {
            AddInput("Condition", typeof(bool), false);
            AddOutput("TruePort", typeof(bool));
            AddOutput("FalsePort", typeof(bool));
        }

        public override void Execute()
        {
            Status = NodeStatus.Running;
            bool condition = GetInput<bool>("Condition");
            SetOutput("TruePort", condition);
            SetOutput("FalsePort", !condition);
            Status = NodeStatus.Completed;
        }
    }

    [WorkflowNode("Number", "Input", "🔢")]
    class NumberNode : BaseNode
    {
        public NumberNode()
        {
            AddInput("Value", typeof(double), 0.0);
            AddOutput("Output", typeof(double));
        }

        public override void Execute()
        {
            Status = NodeStatus.Running;
            SetOutput("Output", GetInput<double>("Value"));
            Status = NodeStatus.Completed;
        }
    }

    // =============================================================
    // 3) BAGLANTI SISTEMI (Connections / Wires)
    // =============================================================

    class Connection
    {
        public IWorkflowNode SourceNode { get; set; }
        public string SourcePort { get; set; }
        public IWorkflowNode TargetNode { get; set; }
        public string TargetPort { get; set; }

        // Veriyi kaynak -> hedef'e aktar
        public void Transfer()
        {
            if (SourceNode.Outputs.ContainsKey(SourcePort) &&
                TargetNode.Inputs.ContainsKey(TargetPort))
            {
                var value = SourceNode.Outputs[SourcePort].Value;
                TargetNode.Inputs[TargetPort].Value = value;
                TargetNode.Inputs[TargetPort].IsConnected = true;
            }
        }

        public override string ToString() =>
            $"  {SourceNode.DisplayName}.{SourcePort} --> {TargetNode.DisplayName}.{TargetPort}";
    }

    // =============================================================
    // 4) WORKFLOW ENGINE (Is Akisi Motoru)
    // =============================================================

    class WorkflowEngine
    {
        private List<IWorkflowNode> _nodes = new List<IWorkflowNode>();
        private List<Connection> _connections = new List<Connection>();

        // Event: Node calistiginda tetiklenir
        public event Action<IWorkflowNode, string> OnNodeExecuted;

        // Node ekle
        public T AddNode<T>() where T : IWorkflowNode, new()
        {
            var node = new T();
            _nodes.Add(node);
            return node;
        }

        // Baglanti ekle
        public void Connect(IWorkflowNode source, string sourcePort,
                           IWorkflowNode target, string targetPort)
        {
            _connections.Add(new Connection
            {
                SourceNode = source, SourcePort = sourcePort,
                TargetNode = target, TargetPort = targetPort
            });
        }

        // Topolojik siralama ile calistirma sirasi belirleme
        private List<IWorkflowNode> TopologicalSort()
        {
            var sorted = new List<IWorkflowNode>();
            var visited = new HashSet<string>();

            void Visit(IWorkflowNode node)
            {
                if (visited.Contains(node.Id)) return;
                visited.Add(node.Id);

                // Bu node'a veri saglayan node'lari once calistir
                foreach (var conn in _connections.Where(c => c.TargetNode.Id == node.Id))
                    Visit(conn.SourceNode);

                sorted.Add(node);
            }

            foreach (var node in _nodes) Visit(node);
            return sorted;
        }

        // Workflow'u calistir
        public void Execute()
        {
            Console.WriteLine("\n  ====== WORKFLOW EXECUTION START ======");

            var executionOrder = TopologicalSort();

            foreach (var node in executionOrder)
            {
                // Baglantilardan veri aktar
                foreach (var conn in _connections.Where(c => c.TargetNode.Id == node.Id))
                    conn.Transfer();

                // Node'u calistir
                try
                {
                    node.Execute();
                    string inputs = string.Join(", ", node.Inputs.Select(i => $"{i.Key}={i.Value.Value}"));
                    string outputs = string.Join(", ", node.Outputs.Select(o => $"{o.Key}={o.Value.Value}"));
                    Console.WriteLine($"  {node} | In:[{inputs}] Out:[{outputs}]");
                    OnNodeExecuted?.Invoke(node, "Success");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"  ❌ {node.DisplayName} FAILED: {ex.Message}");
                    OnNodeExecuted?.Invoke(node, $"Error: {ex.Message}");
                }
            }

            Console.WriteLine("  ====== WORKFLOW EXECUTION END ======\n");
        }

        // Otomatik node kesfetme
        public static List<Type> DiscoverNodes()
        {
            return Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.GetCustomAttribute<WorkflowNodeAttribute>() != null
                         && !t.IsAbstract)
                .ToList();
        }

        // Workflow'u gorsellestir
        public void PrintGraph()
        {
            Console.WriteLine("\n  ╔══════════════════════════════╗");
            Console.WriteLine("  ║     WORKFLOW GRAPH           ║");
            Console.WriteLine("  ╚══════════════════════════════╝");

            Console.WriteLine("\n  Nodes:");
            foreach (var node in _nodes)
            {
                Console.WriteLine($"    {node}");
                foreach (var input in node.Inputs)
                    Console.WriteLine($"      IN:  {input.Value}");
                foreach (var output in node.Outputs)
                    Console.WriteLine($"      OUT: {output.Value}");
            }

            Console.WriteLine("\n  Connections:");
            foreach (var conn in _connections)
                Console.WriteLine($"  {conn}");
        }
    }

    // =============================================================
    // MAIN - Her seyi birlestir
    // =============================================================
    class Lesson18_LowCodeEngine
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== LESSON 18: Low-Code Workflow Engine ===\n");

            // -----------------------------------------------
            // 1) NODE KESFETME (Auto-Discovery)
            // -----------------------------------------------
            Console.WriteLine("--- Available Nodes (Auto-Discovered) ---");
            var availableNodes = WorkflowEngine.DiscoverNodes();
            foreach (var nodeType in availableNodes)
            {
                var attr = nodeType.GetCustomAttribute<WorkflowNodeAttribute>();
                Console.WriteLine($"  {attr.Icon} {attr.Name} [{attr.Category}]");
            }

            // -----------------------------------------------
            // 2) WORKFLOW 1: Basit Hesaplama
            // -----------------------------------------------
            Console.WriteLine("\n--- Workflow 1: (5 + 3) * 2 ---");

            var engine1 = new WorkflowEngine();

            // Node'lari olustur (drag & drop gibi dusunun)
            var num5 = engine1.AddNode<NumberNode>();
            num5.Inputs["Value"].Value = 5.0;

            var num3 = engine1.AddNode<NumberNode>();
            num3.Inputs["Value"].Value = 3.0;

            var num2 = engine1.AddNode<NumberNode>();
            num2.Inputs["Value"].Value = 2.0;

            var add = engine1.AddNode<AddNode>();
            var mul = engine1.AddNode<MultiplyNode>();

            // Baglantilar (wire'lar)
            engine1.Connect(num5, "Output", add, "A");     // 5 -> Add.A
            engine1.Connect(num3, "Output", add, "B");     // 3 -> Add.B
            engine1.Connect(add, "Result", mul, "A");      // Add.Result -> Mul.A
            engine1.Connect(num2, "Output", mul, "B");     // 2 -> Mul.B

            engine1.PrintGraph();
            engine1.Execute();

            Console.WriteLine($"  Final Result: {mul.Outputs["Result"].Value}");  // 16

            // -----------------------------------------------
            // 3) WORKFLOW 2: Kosullu Akis (If/Else)
            // -----------------------------------------------
            Console.WriteLine("\n--- Workflow 2: Conditional Flow ---");

            var engine2 = new WorkflowEngine();

            var score = engine2.AddNode<NumberNode>();
            score.Inputs["Value"].Value = 85.0;

            var threshold = engine2.AddNode<NumberNode>();
            threshold.Inputs["Value"].Value = 70.0;

            var compare = engine2.AddNode<CompareNode>();
            compare.Inputs["Operator"].Value = ">=";

            var template = engine2.AddNode<TextTemplateNode>();
            template.Inputs["Template"].Value = "Score {name} passed the exam!";
            template.Inputs["name"].Value = "Alice";

            var printer = engine2.AddNode<PrintNode>();

            // Baglantilar
            engine2.Connect(score, "Output", compare, "A");
            engine2.Connect(threshold, "Output", compare, "B");
            engine2.Connect(template, "Result", printer, "Message");

            engine2.PrintGraph();
            engine2.Execute();

            // -----------------------------------------------
            // 4) EVENT MONITORING
            // -----------------------------------------------
            Console.WriteLine("\n--- Workflow 3: With Event Monitoring ---");

            var engine3 = new WorkflowEngine();

            // Event'e abone ol
            engine3.OnNodeExecuted += (node, status) =>
            {
                Console.WriteLine($"    [MONITOR] {node.DisplayName}: {status}");
            };

            var a = engine3.AddNode<NumberNode>();
            a.Inputs["Value"].Value = 100.0;

            var b = engine3.AddNode<NumberNode>();
            b.Inputs["Value"].Value = 50.0;

            var addNode = engine3.AddNode<AddNode>();

            engine3.Connect(a, "Output", addNode, "A");
            engine3.Connect(b, "Output", addNode, "B");

            engine3.Execute();

            // -----------------------------------------------
            // OZET: LOW-CODE MANTIGININ TEMELLERI
            // -----------------------------------------------
            Console.WriteLine("===========================================");
            Console.WriteLine("      LOW-CODE MIMARISI OZETI");
            Console.WriteLine("===========================================");
            Console.WriteLine();
            Console.WriteLine("  1. NODE SISTEMI");
            Console.WriteLine("     Interface + Abstract -> BaseNode");
            Console.WriteLine("     Her islem bir node olarak kapsullenir");
            Console.WriteLine();
            Console.WriteLine("  2. PORT SISTEMI");
            Console.WriteLine("     Input/Output portlari ile veri akisi");
            Console.WriteLine("     Generics ile tip guvenligi");
            Console.WriteLine();
            Console.WriteLine("  3. BAGLANTI SISTEMI");
            Console.WriteLine("     Wire/Connection ile node'lar arasi veri transferi");
            Console.WriteLine("     Drag & drop arayuzune karsilik gelir");
            Console.WriteLine();
            Console.WriteLine("  4. EXECUTION ENGINE");
            Console.WriteLine("     Topolojik siralama ile dogru calistirma sirasi");
            Console.WriteLine("     Event sistemi ile izleme/loglama");
            Console.WriteLine();
            Console.WriteLine("  5. AUTO-DISCOVERY");
            Console.WriteLine("     Reflection + Attribute ile node otomatik kesfetme");
            Console.WriteLine("     Plugin sistemi icin temel");
            Console.WriteLine();
            Console.WriteLine("  6. KURAL MOTORU (Ders 17)");
            Console.WriteLine("     Expression Trees ile dinamik kosullar");
            Console.WriteLine("     Kullanici tanimlari is kurallari");
            Console.WriteLine();
            Console.WriteLine("  Bu temellerle:");
            Console.WriteLine("  - Power Automate");
            Console.WriteLine("  - Node-RED");
            Console.WriteLine("  - Unreal Blueprints");
            Console.WriteLine("  - Zapier");
            Console.WriteLine("  gibi platformlarin mantigini anlamis olursunuz!");

            Console.WriteLine("\n=== ALL LESSONS COMPLETED! ===");
            Console.WriteLine("Tebrikler! C# sifirdan low-code mantigina kadar");
            Console.WriteLine("tum temelleri ogrendiniz. Press any key to exit...");
            Console.ReadKey();
        }
    }
}
