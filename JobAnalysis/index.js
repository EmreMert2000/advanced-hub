// Tab Switching Logic
document.querySelectorAll('.tab-btn').forEach(button => {
    button.addEventListener('click', () => {
        // Remove active class from all buttons and contents
        document.querySelectorAll('.tab-btn').forEach(btn => btn.classList.remove('active'));
        document.querySelectorAll('.tab-content').forEach(content => content.classList.remove('active'));

        // Add active class to clicked button and target content
        button.classList.add('active');
        const tabId = button.getAttribute('data-tab');
        document.getElementById(tabId).classList.add('active');
    });
});

// --- DATA STRUCTURES ---

// 1. Stack Implementation
class Stack {
    constructor() {
        this.items = [];
    }

    push(element) {
        this.items.push(element);
    }

    pop() {
        if (this.isEmpty()) return "Underflow";
        return this.items.pop();
    }

    peek() {
        return this.items[this.items.length - 1];
    }

    isEmpty() {
        return this.items.length === 0;
    }

    printStack() {
        return this.items.join(" <- ");
    }
}

function runStackDemo() {
    const stack = new Stack();
    let output = "Stack oluşturuldu.\\n";
    
    stack.push(10);
    output += "Push(10): " + stack.printStack() + "\\n";
    stack.push(20);
    output += "Push(20): " + stack.printStack() + "\\n";
    stack.push(30);
    output += "Push(30): " + stack.printStack() + "\\n";
    
    output += "Pop(): " + stack.pop() + " çıkarıldı. Kalan: " + stack.printStack() + "\\n";
    output += "Peek(): " + stack.peek();
    
    document.getElementById('stack-output').innerText = output;
}

// 2. Queue Implementation
class Queue {
    constructor() {
        this.items = [];
    }

    enqueue(element) {
        this.items.push(element);
    }

    dequeue() {
        if (this.isEmpty()) return "Underflow";
        return this.items.shift();
    }

    front() {
        if (this.isEmpty()) return "No elements in Queue";
        return this.items[0];
    }

    isEmpty() {
        return this.items.length === 0;
    }

    printQueue() {
        return this.items.join(" <- ");
    }
}

function runQueueDemo() {
    const queue = new Queue();
    let output = "Queue oluşturuldu.\\n";
    
    queue.enqueue("A");
    output += "Enqueue('A'): " + queue.printQueue() + "\\n";
    queue.enqueue("B");
    output += "Enqueue('B'): " + queue.printQueue() + "\\n";
    queue.enqueue("C");
    output += "Enqueue('C'): " + queue.printQueue() + "\\n";
    
    output += "Dequeue(): " + queue.dequeue() + " çıkarıldı. Kalan: " + queue.printQueue() + "\\n";
    output += "Front(): " + queue.front();
    
    document.getElementById('queue-output').innerText = output;
}

// 3. Linked List Implementation
class Node {
    constructor(element) {
        this.element = element;
        this.next = null;
    }
}

class LinkedList {
    constructor() {
        this.head = null;
        this.size = 0;
    }

    add(element) {
        const node = new Node(element);
        let current;

        if (this.head == null) {
            this.head = node;
        } else {
            current = this.head;
            while (current.next) {
                current = current.next;
            }
            current.next = node;
        }
        this.size++;
    }

    printList() {
        let curr = this.head;
        let str = "";
        while (curr) {
            str += curr.element + (curr.next ? " -> " : "");
            curr = curr.next;
        }
        return str;
    }
}

function runLinkedListDemo() {
    const ll = new LinkedList();
    let output = "Linked List oluşturuldu.\\n";
    
    ll.add(100);
    output += "Add(100): " + ll.printList() + "\\n";
    ll.add(200);
    output += "Add(200): " + ll.printList() + "\\n";
    ll.add(300);
    output += "Add(300): " + ll.printList();
    
    document.getElementById('ll-output').innerText = output;
}

// 4. Binary Search Tree Implementation
class TreeNode {
    constructor(data) {
        this.data = data;
        this.left = null;
        this.right = null;
    }
}

class BinarySearchTree {
    constructor() {
        this.root = null;
    }

    insert(data) {
        const newNode = new TreeNode(data);
        if (this.root === null) {
            this.root = newNode;
        } else {
            this.insertNode(this.root, newNode);
        }
    }

    insertNode(node, newNode) {
        if (newNode.data < node.data) {
            if (node.left === null) node.left = newNode;
            else this.insertNode(node.left, newNode);
        } else {
            if (node.right === null) node.right = newNode;
            else this.insertNode(node.right, newNode);
        }
    }

    inorder(node, result = []) {
        if (node !== null) {
            this.inorder(node.left, result);
            result.push(node.data);
            this.inorder(node.right, result);
        }
        return result;
    }
}

function runBSTDemo() {
    const bst = new BinarySearchTree();
    let output = "BST oluşturuldu.\\n";
    
    const values = [15, 25, 10, 7, 22, 17, 13];
    output += "Eklenenler: " + values.join(", ") + "\\n";
    
    values.forEach(v => bst.insert(v));
    
    output += "Inorder Traversal (Sıralı): " + bst.inorder(bst.root).join(" -> ");
    
    document.getElementById('bst-output').innerText = output;
}

// --- MOCK INTERVIEW QUESTIONS ---
const questions = {
    html: [
        { q: "DOCTYPE nedir ve neden kullanılır?", a: "Tarayıcıya belgenin hangi HTML sürümüyle yazıldığını bildirir. Standart modda çalışmasını sağlar." },
        { q: "Semantic Tag (Anlamsal Etiket) nedir?", a: "İçeriğin anlamını tarayıcı ve geliştiriciye daha iyi anlatan etiketlerdir. Örn: <header>, <footer>, <article>." },
        { q: "Meta tagleri ne işe yarar?", a: "Sayfa hakkında metadata (bilgi) sağlar. SEO, karakter seti, responsive ayarları için kritiktir." }
    ],
    css: [
        { q: "Box Model nedir?", a: "Her HTML elementinin bir kutu gibi davrandığı modeldir. İçerik (content), dolgu (padding), kenarlık (border) ve kenar boşluğu (margin) katmanlarından oluşur." },
        { q: "Flexbox ve Grid arasındaki fark nedir?", a: "Flexbox tek boyutlu (satır VEYA sütun) düzenler için, Grid iki boyutlu (satır VE sütun) düzenler için daha uygundur." },
        { q: "CSS öncelik sıralaması (Specificity) nasıldır?", a: "Inline Style > ID > Class > Tag. !important hepsini ezer." }
    ],
    js: [
        { q: "Closure nedir?", a: "Bir fonksiyonun, kendi kapsamı dışındaki değişkenlere erişebilme yeteneğidir." },
        { q: "Hoisting nedir?", a: "Değişken ve fonksiyon tanımlarının kod çalışmadan önce belleğe alınmasıdır. 'var' ile tanımlananlar undefined olur, 'let/const' temporal dead zone'dadır." },
        { q: "Event Loop nedir?", a: "JavaScript'in tek thread'li yapısında asenkron işlemleri yöneten mekanizmadır. Call Stack boşaldığında Callback Queue'daki işleri işler." }
    ],
    sql: [
        { q: "Primary Key vs Foreign Key?", a: "Primary Key tablo içindeki benzersiz kaydı tanımlar. Foreign Key başka bir tablodaki Primary Key'e referans vererek ilişki kurar." },
        { q: "Inner Join vs Left Join?", a: "Inner Join sadece eşleşen kayıtları getirir. Left Join sol tablodaki tüm kayıtları, sağdan ise eşleşenleri (yoksa NULL) getirir." },
        { q: "Normalization nedir?", a: "Veri tekrarını önlemek ve veri bütünlüğünü sağlamak için veritabanının belirli kurallara göre tasarlanmasıdır." }
    ],
    job: [
        { q: "POC (Proof of Concept) nedir?", a: "Yazılımın müşteri ihtiyacını karşıladığını kanıtlamak için yapılan kısa süreli ve kısıtlı kapsamlı pilot çalışmadır." },
        { q: "QMS (Kalite Yönetim Sistemi) ne işe yarar?", a: "Organizasyonun kalite standartlarını korumak için süreçleri, prosedürleri ve kaynakları yöneten sistemdir. Hata takibi (DÖF) gibi süreçleri içerir." },
        { q: "BPM (Süreç Yönetimi) nedir?", a: "İş süreçlerinin analiz edilmesi, modellenmesi, otomatize edilmesi ve iyileştirilmesidir. Amaç verimliliği artırmaktır." },
        { q: "Zor bir müşteriyi nasıl yönetirsiniz (Pre-sales)?", a: "Dinlerim, problemini anladığımı gösteririm. Beklentilerini gerçekçi bir zemine çekip alternatif çözümler sunarak güven kazanırım." },
        { q: "Kullanıcı Kabul Testi (UAT) nedir?", a: "Yazılımın canlıya alınmadan önce, son kullanıcılar tarafından gerçek senaryolarla test edilerek onaylanması sürecidir." }
    ]
};

// Populate Questions
function loadQuestions() {
    ['html', 'css', 'js', 'sql', 'job'].forEach(type => {
        const container = document.getElementById(`${type}-qa`);
        if(container) {
            questions[type].forEach(item => {
                const div = document.createElement('div');
                div.className = 'qa-item';
                div.innerHTML = `<div class="qa-question">${item.q}</div><div class="qa-answer">${item.a}</div>`;
                container.appendChild(div);
            });
        }
    });
}

// Init
window.onload = loadQuestions;
