using System;
using System.Linq;
using System.Threading.Tasks;

public class TaskManagerUI
{
    private readonly ITaskRepository _taskRepository;
    public TaskManagerUI(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task RunAsync()
    {
        bool isRunning = true;
        while (isRunning)
        {
            ShowMenu();
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await ShowAllTasks();
                    break;
                case "2":
                    await AddNewTask();
                    break;
                case "3":
                    await UpdateTaskStatus();
                    break;
                case "4":
                    await DeleteTask();
                    break;
                case "5":
                    isRunning = false;
                    Console.WriteLine("\nВыход из приложения...");
                    break;
                default:
                    Console.WriteLine("\nНеверный выбор. Пожалуйста, попробуйте снова.");
                    break;
            }

            if (isRunning)
            {
                Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                Console.ReadKey();
            }
        }
    }

    private void ShowMenu()
    {
        Console.Clear();

        Console.WriteLine(" 1. Показать все задачи");
        Console.WriteLine(" 2. Добавить новую задачу ");
        Console.WriteLine(" 3. Отметить задачу выполненной");
        Console.WriteLine(" 4. Удалить задачу ");
        Console.WriteLine(" 5. Выход");

        Console.Write("Выберите опцию: ");
    }

    private async Task ShowAllTasks()
    {
        Console.WriteLine("\n--- Список всех задач ---");
        var tasks = await _taskRepository.GetAllAsync();

        if (!tasks.Any())
        {
            Console.WriteLine("Задач пока нет. Добавьте первую!");
            return;
        }

        foreach (var task in tasks)
        {
            string status = task.IsCompleted ? "Выполнена" : "В работе";
            Console.WriteLine($"[ID: {task.Id}] {task.Title} - {status} Создана {task.CreatedAt}");
        }
    }

    private async Task AddNewTask()
    {
        Console.WriteLine("\n--- Добавление новой задачи ---");
        Console.Write("Введите название: ");
        string title = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine("Название не может быть пустым!");
            return;
        }

        Console.Write("Введите описание: ");
        string description = Console.ReadLine();

        var newTask = new TaskItem
        {
            Title = title,
            Description = description,
            CreatedAt = DateTime.Now,
            IsCompleted = false
        };

        await _taskRepository.AddAsync(newTask);
        Console.WriteLine("\nЗадача успешно добавлена!");
    }

    private async Task UpdateTaskStatus()
    {
        await ShowAllTasks();
        Console.Write("\nВведите ID задачи, которую нужно отметить как выполненную: ");

        if (int.TryParse(Console.ReadLine(), out int id))
        {
            bool success = await _taskRepository.UpdateStatusAsync(id, true);
            if (success)
            {
                Console.WriteLine($"\nЗадача с ID {id} успешно обновлена.");
            }
            else
            {
                Console.WriteLine($"\nЗадача с ID {id} не найдена.");
            }
        }
        else
        {
            Console.WriteLine("\nНеверный формат ID. Введите число.");
        }
    }

    private async Task DeleteTask()
    {
        await ShowAllTasks();
        Console.Write("\nВведите ID задачи, которую нужно удалить: ");

        if (int.TryParse(Console.ReadLine(), out int id))
        {
            bool success = await _taskRepository.DeleteAsync(id);
            if (success)
            {
                Console.WriteLine($"\n Задача с ID {id} успешно удалена.");
            }
            else
            {
                Console.WriteLine($"\n Задача с ID {id} не найдена.");
            }
        }
        else
        {
            Console.WriteLine("\n Неверный формат ID. Введите число.");
        }
    }
}