using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

public class TaskItemRepository : ITaskRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public TaskItemRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<int> AddAsync(TaskItem taskItem)
    {
        string sql = @"INSERT INTO Tasks (Title, Description, IsCompleted, CreatedAt) 
                       VALUES (@Title, @Description, @IsCompleted, @CreatedAt);";

        using (var connection = _connectionFactory.CreateConnection())
        {
            return await connection.ExecuteAsync(sql, taskItem);
        }
    }

    public async Task<IEnumerable<TaskItem>> GetAllAsync()
    {
        string sql = "SELECT * FROM Tasks;";
        using (var connection = _connectionFactory.CreateConnection())
        {
            return await connection.QueryAsync<TaskItem>(sql);
        }
    }

    public async Task<bool> UpdateStatusAsync(int id, bool isCompleted)
    {
        string sql = "UPDATE Tasks SET IsCompleted = @IsCompleted WHERE Id = @Id;";
        using (var connection = _connectionFactory.CreateConnection())
        {
            int affectedRows = await connection.ExecuteAsync(sql, new { IsCompleted = isCompleted, Id = id });
            return affectedRows > 0;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        string sql = "DELETE FROM Tasks WHERE Id = @Id;";
        using (var connection = _connectionFactory.CreateConnection())
        {
            int affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
            return affectedRows > 0;
        }
    }
}