using Study.LabWork2.Abstractions.Feature.Task1.SubTask1;
using Study.LabWork2.Abstractions.Feature.Task1.SubTask2;
using Study.LabWork2.Abstractions.Feature.Task2;
using Study.LabWork2.Abstractions.Feature.Task2.DtoModels;
using Study.LabWork2.Feature.Task1.SubTask1;
using Study.LabWork2.Feature.Task1.SubTask2;
using Study.LabWork2.Feature.Task2;
using System.Threading.Tasks;
using System;

namespace Study.LabWork2;

public static class Program
{
    static void Main()
    {
        const int task = 3;
        switch (task)
        {
            case 1:
                Console.WriteLine("=== Task 1. SubTask 1 ===");
                IPrimeCounter[] counters = [new MonitorService(), new MutexService(), new SemaphoreService()];
                foreach (var c in counters)
                {
                    Console.WriteLine($"\n--- {c.GetVersionName()} ---");
                    Console.WriteLine(c.CountPrimes(1, 10000, 4));
                }
                break;

            case 2:
                Console.WriteLine("=== Task 1. SubTask 2 ===");
                INumberSetProcessor processor = new NumberSetProcessor();
                processor.Process();
                var result = processor.GetResult();

                foreach (var r in result.Results) Console.WriteLine(r);
                Console.WriteLine($"Общий итог: {result.TotalSum}");
                Console.WriteLine($"Время: {result.ExecutionTime.TotalMilliseconds:F0} мс");
                break;

            case 3:
                {
                    Console.WriteLine("=== Task 2 ===");
                    using var httpClient = new HttpClient();
                    IRequestService requestService = new RequestService(httpClient);

                    var servers = new[]
                    {
                        new ServerConfigDto { Url = "https://jsonplaceholder.typicode.com/posts/1" },
                        new ServerConfigDto { Url = "https://jsonplaceholder.typicode.com/users/1" },
                        new ServerConfigDto { Url = "https://jsonplaceholder.typicode.com/todos/1" }
                    };

                    Console.WriteLine("=== Синхронная версия ===");
                    IServerRequestApp syncApp = new SynchronousServerRequestApp(requestService);
                    Console.WriteLine($"Версия: {syncApp.GetVersion()}");
                    var syncResult = syncApp.ExecuteRequests<string>(servers);
                    Console.WriteLine($"Время: {syncResult.TotalExecutionTime.TotalMilliseconds:F0} мс");

                    Console.WriteLine("\n=== Асинхронная версия ===");
                    IServerRequestApp asyncApp = new AsynchronousServerRequestApp(requestService);
                    Console.WriteLine($"Версия: {asyncApp.GetVersion()}");
                    var asyncResult = asyncApp.ExecuteRequests<string>(servers);
                    Console.WriteLine($"Время: {asyncResult.TotalExecutionTime.TotalMilliseconds:F0} мс");
                }
                break;
        }
    }
}
