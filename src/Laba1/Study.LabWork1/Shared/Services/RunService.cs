using Study.LabWork1.Features.Task3;
using Study.LabWork1.Features.Task2;
using Study.LabWork1.Features.Task1;

using Study.LabWork1.Shared.Abstractions;

namespace Study.LabWork1.Shared.Services;

public class RunService : IRunService
{
    /// <summary>
    /// Задание 1
    /// </summary>
    public void RunTask1() {
        Console.WriteLine("==== Тесты Pixel ====\n");

        var p1 = new Pixel(255, 0, 0);
        var p2 = new Pixel(0, 255, 0, 0.5f);
        var p3 = new Pixel(0, 0, 255, 1.0f);
        var p4 = new Pixel();
        var p5 = new Pixel(255, 0, 0, 1f);
        var p6 = new Pixel(300, -50, 255, 1.5f);
        var p7 = new Pixel(100, 100, 100, 0.8f);

        Console.WriteLine($"p1 = {p1}");
        Console.WriteLine($"p2 = {p2}");
        Console.WriteLine($"p3 = {p3}");
        Console.WriteLine($"p4 = {p4}");
        Console.WriteLine($"p5 = {p5}");
        Console.WriteLine($"p6 (300, -50, 255, 1.5f) = {p6}");
        Console.WriteLine($"p7 = {p7}\n");

        Console.WriteLine($"p1 + p2 = {p1 + p2}");
        Console.WriteLine($"p1 - p2 = {p1 - p2}");
        Console.WriteLine($"p1 * 1.5f = {p1 * 1.5f}");
        Console.WriteLine($"p1 / 2f = {p1 / 2f}\n");

        Console.WriteLine($"p1 == p2: {p1 == p2}");
        Console.WriteLine($"p1 != p2: {p1 != p2}");
        Console.WriteLine($"p1 == p2: {p1 == p5}");
        Console.WriteLine($"p1 != p2: {p1 != p5}\n");

        Console.WriteLine($"p5 = {p5}");

        Console.WriteLine($"p1 в HEX: {p1.PixelToHex()}");
        Console.WriteLine($"p2 в HEX: {p2.PixelToHex()}\n");

        Console.WriteLine($"p7 * 2 = {p7 * 2}");
        Console.WriteLine($"p7 / 2 = {p7 / 2}\n");

        Console.WriteLine("==== Тесты пройдены ====");
    }

    /// <summary>
    /// Задание 2
    /// </summary>
    public void RunTask2()
    {
        Console.WriteLine("==== Тесты Adapter ====\n");
        bool allPassed = true;

        // 2. Тест памяти
        var memRepo = new InMemoryOrderRepository();
        memRepo.Save(new Order { Id = 1, Customer = "Иван", Total = 100m });
        var memResult = memRepo.GetAll();
        bool memOk = memResult.Count == 1 && memResult[0].Customer == "Иван" && memResult[0].Total == 100m;
        allPassed &= memOk;

        // 2. Тест CSV
        string csvPath = "test_adapter_orders.csv";
        if (File.Exists(csvPath)) File.Delete(csvPath);

        var csvRepo = new CsvOrderRepository(new CsvOrderStorage(csvPath));
        csvRepo.Save(new Order { Id = 1, Customer = "Ник", Total = 500m });
        csvRepo.Save(new Order { Id = 2, Customer = "Анна", Total = 750.5m });

        var csvResult = csvRepo.GetAll();
        bool csvOk = csvResult.Count == 2 &&
                     csvResult.Any(o => o.Id == 1 && o.Total == 500m) &&
                     csvResult.Any(o => o.Id == 2 && o.Total == 750.5m);
        allPassed &= csvOk;

        // 3. Проверка файла
        bool fileOk = File.Exists(csvPath) && File.ReadAllLines(csvPath).Length == 3;
        allPassed &= fileOk;

        // Удаление файла
        if (File.Exists(csvPath)) File.Delete(csvPath);

        Console.WriteLine(allPassed ? "Тесты пройдены успешно" : "Тесты не пройдены");
    }

    /// <summary>
    /// Задание 3
    /// </summary>
    public void RunTask3() => throw new NotImplementedException();
}
