using System;
using static System.Threading.Thread;
class File
{
    public string ProcessData(string dataName)
    {
        Thread.Sleep(3000);
        return $"Обработка '{dataName}' завершена за 3 секунды";
    }
    async public Task<string> ProcessDataAsync(string dataName)
    {
        await Task.Delay(3000);
        return $"Обработка '{dataName}' завершена за 3 секунды";
    }
}

class Program
{
    static async Task Main()
    {

        File file1 = new File();
        File file2 = new File();
        File file3 = new File();
        Console.WriteLine(file1.ProcessData("Файл 1"));
        Console.WriteLine(file2.ProcessData("Файл 2"));
        Console.WriteLine(file3.ProcessData("Файл 3"));


        File file4 = new File();
        File file5 = new File();
        File file6 = new File();

        Task<string> task1 = file4.ProcessDataAsync("Файл 4");
        Task<string> task2 = file5.ProcessDataAsync("Файл 5");
        Task<string> task3 = file6.ProcessDataAsync("Файл 6");

        string[] results = await Task.WhenAll(task1, task2, task3);

        foreach (var result in results)
        {
            Console.WriteLine(result);
        }
    }
}

