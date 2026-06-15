using testwebapp.Entites;

namespace testwebapp.Repositories.Interfaces;

public interface ITaskRepository
{
    Task<IEnumerable<ToDoEntity>> GetAllAsync();
    Task<ToDoEntity?> GetByIdAsync(int id);
    Task AddAsync(ToDoEntity task);
    Task UpdateAsync(ToDoEntity task);
    Task DeleteAsync(int id);
}
