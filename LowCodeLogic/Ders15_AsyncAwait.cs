// =============================================================
// DERS 15 - ASYNC/AWAIT VE TASK YAPISI
// =============================================================
// Bu derste:
// - Asenkron programlama nedir ve neden gerekli
// - async/await anahtar kelimeleri
// - Task ve Task<T>
// - Paralel islemler (Task.WhenAll, Task.WhenAny)
// - CancellationToken
// - Async patternler
// =============================================================

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LowCodeLogic
{
    class Lesson15_AsyncAwait
    {
        // -----------------------------------------------
        // 1) SENKRON VS ASENKRON
        // -----------------------------------------------
        // Senkron: Her islem siradaki islemi bekler (bloklama olur)
        // Asenkron: Islem bitmesini beklemeden diger islemlere devam eder

        // Senkron metot (yavastir - thread'i bloklar)
        static void DownloadSync(string url)
        {
            Console.WriteLine($"  [SYNC] Downloading {url}...");
            Thread.Sleep(1000);     // 1 saniye bekle (bloklar!)
            Console.WriteLine($"  [SYNC] Downloaded {url}");
        }

        // -----------------------------------------------
        // 2) ASYNC METOTLAR
        // -----------------------------------------------
        // async: Metotun asenkron oldugunu belirtir
        // await: Asenkron islemi bekle ama thread'i bloklatma
        // Task: Void donen asenkron islem
        // Task<T>: T tipinde deger donen asenkron islem

        static async Task DownloadAsync(string url)
        {
            Console.WriteLine($"  [ASYNC] Downloading {url}...");
            await Task.Delay(1000);     // Thread'i bloklamadan 1 saniye bekle
            Console.WriteLine($"  [ASYNC] Downloaded {url}");
        }

        // Deger donen async metot
        static async Task<string> FetchDataAsync(string source)
        {
            Console.WriteLine($"  Fetching from {source}...");
            await Task.Delay(800);
            return $"Data from {source} [fetched at {DateTime.Now:HH:mm:ss}]";
        }

        // Agir hesaplama (CPU-bound) icin Task.Run
        static async Task<long> CalculateSumAsync(int count)
        {
            return await Task.Run(() =>
            {
                long sum = 0;
                for (int i = 0; i <= count; i++) sum += i;
                return sum;
            });
        }

        // -----------------------------------------------
        // 3) CANCELLATION TOKEN
        // -----------------------------------------------
        static async Task LongRunningTaskAsync(CancellationToken token)
        {
            for (int i = 1; i <= 10; i++)
            {
                // Her adimda iptal kontrolu yap
                token.ThrowIfCancellationRequested();

                Console.WriteLine($"  Step {i}/10...");
                await Task.Delay(500, token);
            }
            Console.WriteLine("  Task completed!");
        }

        // -----------------------------------------------
        // 4) RETRY PATTERN
        // -----------------------------------------------
        static async Task<T> RetryAsync<T>(Func<Task<T>> action, int maxRetries = 3)
        {
            for (int i = 0; i < maxRetries; i++)
            {
                try
                {
                    return await action();
                }
                catch (Exception ex) when (i < maxRetries - 1)
                {
                    Console.WriteLine($"  Attempt {i + 1} failed: {ex.Message}. Retrying...");
                    await Task.Delay(500 * (i + 1));    // Giderek artan bekleme
                }
            }
            throw new Exception("All retries failed.");
        }

        // -----------------------------------------------
        // MAIN (async Main C# 7.1+)
        // -----------------------------------------------
        static async Task Main(string[] args)
        {
            Console.WriteLine("=== LESSON 15: Async/Await ===\n");

            // 1) Senkron vs Asenkron
            Console.WriteLine("--- Sync vs Async ---");
            Console.WriteLine("Synchronous:");
            DownloadSync("file1.zip");

            Console.WriteLine("\nAsynchronous:");
            await DownloadAsync("file1.zip");

            // 2) Task<T> - Deger dondurme
            Console.WriteLine("\n--- Task<T> Return Value ---");
            string data = await FetchDataAsync("API");
            Console.WriteLine($"  Result: {data}");

            // 3) Paralel islemler - Task.WhenAll
            Console.WriteLine("\n--- Task.WhenAll (Parallel) ---");
            var watch = System.Diagnostics.Stopwatch.StartNew();

            // Birden fazla async islemi ayni anda baslat
            Task<string> task1 = FetchDataAsync("Database");
            Task<string> task2 = FetchDataAsync("Cache");
            Task<string> task3 = FetchDataAsync("FileSystem");

            // Hepsinin bitmesini bekle
            string[] results = await Task.WhenAll(task1, task2, task3);

            watch.Stop();
            Console.WriteLine($"  All completed in {watch.ElapsedMilliseconds}ms:");
            foreach (string r in results)
                Console.WriteLine($"  -> {r}");

            // 4) Task.WhenAny - Ilk biteni al
            Console.WriteLine("\n--- Task.WhenAny (First to Complete) ---");

            Task<string> fast = FetchDataAsync("FastServer");
            Task<string> slow = Task.Run(async () =>
            {
                await Task.Delay(2000);
                return "Data from SlowServer";
            });

            Task<string> firstDone = await Task.WhenAny(fast, slow);
            Console.WriteLine($"  First result: {await firstDone}");

            // 5) CancellationToken
            Console.WriteLine("\n--- CancellationToken ---");
            var cts = new CancellationTokenSource();

            // 2 saniye sonra iptal et
            cts.CancelAfter(2000);

            try
            {
                await LongRunningTaskAsync(cts.Token);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("  Task was cancelled!");
            }

            // 6) CPU-bound async
            Console.WriteLine("\n--- CPU-bound Task.Run ---");
            long sum = await CalculateSumAsync(1_000_000);
            Console.WriteLine($"  Sum(1..1M) = {sum:N0}");

            // 7) Retry pattern
            Console.WriteLine("\n--- Retry Pattern ---");
            int attempt = 0;
            try
            {
                string retryResult = await RetryAsync(async () =>
                {
                    attempt++;
                    if (attempt < 3) throw new Exception("Server timeout");
                    await Task.Delay(100);
                    return "Success!";
                });
                Console.WriteLine($"  Final result: {retryResult}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  Failed: {ex.Message}");
            }

            // -----------------------------------------------
            // ASYNC KURALLAR OZETI
            // -----------------------------------------------
            /*
                1. async metotlar Task veya Task<T> dondurur
                2. await sadece async metot icinde kullanilir
                3. async void SADECE event handler'larda kullanin (yoksa kacinin!)
                4. CPU-bound is icin Task.Run kullanin
                5. I/O-bound is icin dogrudan async/await kullanin
                6. Deadlock onlemek icin ConfigureAwait(false) kullanabilirsiniz
                7. Her zaman CancellationToken destekleyin
            */

            Console.WriteLine("\nLesson 15 completed! Press any key to exit...");
            Console.ReadKey();
        }
    }
}
