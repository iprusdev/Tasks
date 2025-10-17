
using System.Collections.Generic;
using System.Threading.Tasks;
public interface ITaskRepository
{
    Task<IEnumerable<TaskItem>> GetAllAsync();
    Task<int> AddAsync(TaskItem task);
    Task<bool> UpdateStatusAsync(int id, bool isCompleted);
    Task<bool> DeleteAsync(int id);
}