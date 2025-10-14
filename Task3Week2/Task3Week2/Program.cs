using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TaskManager;Integrated Security=True;";
        IDbConnectionFactory connectionFactory = new SqlConnectionFactory(connectionString);
        ITaskRepository taskRepository = new TaskItemRepository(connectionFactory);

        var ui = new TaskManagerUI(taskRepository);
        await ui.RunAsync();
    }
}