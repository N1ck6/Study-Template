using Study.LabWork2.Abstractions.Feature.Task1.SubTask1;
using Study.LabWork2.Abstractions.Feature.Task1.SubTask1.DtoModels;
using System.Diagnostics;

namespace Study.LabWork2.Feature.Task1.SubTask1;

/// <summary>
/// Версия 2. Использует Mutex для синхронизации
/// </summary>
public sealed class MutexService : IPrimeCounter
{
    public PrimeCountResultDto CountPrimes(int start, int end, int threadCount) {
        var swatch = new Stopwatch();
        int count = 0;
        var mutex = new Mutex();
        var primes = new List<int>[threadCount];
        var threads = new Thread[threadCount];
        int range = (end - start) / threadCount;

        for (int i = 0; i < threadCount; ++i)
        {
            int id = i;
            primes[id] = new List<int>();
            int min = start + id * range;
            int max = (id == threadCount - 1) ? end : start + range - 1;
            threads[id] = new Thread(() => {
                for (int j = min; j <= max; ++j)
                {
                    Console.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId}: {j}");
                    if (IsPrime(j))
                    {
                        primes[id].Add(j);
                        mutex.WaitOne();
                        count++;
                        mutex.ReleaseMutex();
                    }
                }
            });
            threads[id].Start();
        }

        foreach (var t in threads) t.Join();
        mutex.Dispose();
        swatch.Stop();

        var allPrimes = new List<int>();
        foreach (var p in primes) allPrimes.AddRange(p);

        return new PrimeCountResultDto
        {
            PrimeCount = count,
            ExecutionTime = swatch.Elapsed,
            ThreadCount = threadCount,
            SynchronizationType = "Mutex",
            FoundPrimes = allPrimes
        };
    }

    public string GetVersionName() => "Mutex";

    private static bool IsPrime(int n)
    {
        if (n < 2) return false;
        for (int i = 2; i * i <= n; i++)
            if (n % i == 0) return false;
        return true;
    }
}
