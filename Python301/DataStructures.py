#Data Structures
#1. Lists
#2. Tuples
#3. Sets
#4. Dictionaries

#1. Lists
#Lists are ordered, mutable (changeable), and can contain duplicate values.
#Syntax: my_list = [1, 2, 3, 4, 5]
#Example:

import numpy as np
my_list = [1, 2, 3, 4, 5]
print(my_list)
print(type(my_list))

np.array(my_list)
type(np.array(my_list))


print(np.array(my_list).max())


other_array = np.array([10,20,30,40,50])

print(other_array.max())

my_matrix = [[5,10],[15,20]]
print(np.array(my_matrix))

# --- List Operations (Devam) ---
# Listeye eleman ekleme (append)
my_list.append(6)
print(f"Append sonrası: {my_list}")

# Listeden eleman çıkarma (pop - son elemanı siler)
my_list.pop()
print(f"Pop sonrası: {my_list}")

# Indeksleme (Indexing)
print(f"İlk eleman: {my_list[0]}")


# 2. Tuples (Demetler)
# Tuples sıralıdır (ordered) ve değiştirilemez (immutable).
# Syntax: my_tuple = (1, 2, 3)
print("\n--- Tuples (Demetler) ---")
my_tuple = (10, 20, 30, 40, 50)
print(f"Tuple: {my_tuple}")
print(f"Tuple ilk eleman: {my_tuple[0]}")
# my_tuple[0] = 100  # HATA VERİR: Tuple elemanları değiştirilemez.

# Tuple metodları (count, index)
print(f"20 sayısı kaç kere var?: {my_tuple.count(20)}")


# 3. Sets (Kümeler)
# Sets sırasızdır (unordered) ve indekslenemez. Tekrar eden veri barındırmaz (unique).
# Syntax: my_set = {1, 2, 3}
print("\n--- Sets (Kümeler) ---")
my_set = {10, 20, 30, 40, 40, 40} # Tekrar edenler otomatik silinir
print(f"Set (tekrarlar silindi): {my_set}")

my_set.add(50) # Eleman ekleme
print(f"50 eklendi: {my_set}")

# Kümeler arası işlemler (Union, Intersection)
set1 = {1, 2, 3}
set2 = {3, 4, 5}
print(f"Birleşim (Union): {set1.union(set2)}")
print(f"Kesişim (Intersection): {set1.intersection(set2)}")


# 4. Dictionaries (Sözlükler)
# Key-Value (Anahtar-Değer) çiftlerinden oluşur.
# Sıralıdır (Python 3.7+) ve değiştirilebilir (mutable).
print("\n--- Dictionaries (Sözlükler) ---")
my_dict = {
    "isim": "Ahmet",
    "yas": 25,
    "meslek": "Mühendis",
    "lokasyon": "İstanbul"
}

print(f"Sözlük: {my_dict}")
print(f"Belli bir değeri alma (isim): {my_dict['isim']}")

# Yeni Key-Value ekleme
my_dict["maas"] = 50000
print(f"Maaş eklendi: {my_dict}")

# Keyleri ve Valueları ayrı ayrı alma
print(f"Anahtarlar (Keys): {my_dict.keys()}")
print(f"Değerler (Values): {my_dict.values()}")
