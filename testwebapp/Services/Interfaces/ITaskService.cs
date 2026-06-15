using testwebapp.Models;

namespace testwebapp.Services.Interfaces
{
    public interface ITaskService
    {
        Task CreateTaskAsync(TodoTask task);
        Task DeleteTaskAsync(int id);
        Task<IEnumerable<TodoTask>> GetAllTasksAsync();
        Task<TodoTask> GetTaskByIdAsync(int id);
        Task UpdateTaskAsync(TodoTask task);
    }
}
