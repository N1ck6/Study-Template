using Study.LabWork2.Abstractions.Feature.Task2;
using Study.LabWork2.Abstractions.Feature.Task2.DtoModels;
using System.Diagnostics;

namespace Study.LabWork2.Feature.Task2;


/// <summary>
/// Асинхронная версия приложения (с использованием async/await)
/// </summary>
public sealed class AsynchronousServerRequestApp : IServerRequestApp
{
    private readonly IRequestService _requestService;

    public AsynchronousServerRequestApp(IRequestService requestService) => _requestService = requestService;

    public ExecutionResultDto<TResponse> ExecuteRequests<TResponse>(ServerConfigDto[] servers)
    {
        // Для асинхронной версии вызываем асинхронный метод
        return ExecuteRequestsAsync<TResponse>(servers).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Асинхронное выполнение запросов
    /// </summary>
    private async Task<ExecutionResultDto<TResponse>> ExecuteRequestsAsync<TResponse>(ServerConfigDto[] servers)
    {
        var sw = Stopwatch.StartNew();
        var responses = new List<TResponse>();

        try
        {
            var tasks = servers.Select(s => _requestService.FetchDataAsync(s.Url)).ToArray();
            var results = await Task.WhenAll(tasks);

            foreach (var data in results)
            {
                Console.WriteLine(data);
                responses.Add((TResponse)(object)data);
            }

            sw.Stop();
            return new ExecutionResultDto<TResponse>
            {
                Responses = responses,
                TotalExecutionTime = sw.Elapsed,
                SuccessfulRequest = true
            };
        }
        catch (Exception ex)
        {
            sw.Stop();
            Console.WriteLine($"Ошибка: {ex.Message}");
            return new ExecutionResultDto<TResponse>
            {
                TotalExecutionTime = sw.Elapsed,
                FailedRequest = true
            };
        }
    }

    public string GetVersion() => "Асинхронная версия";
}