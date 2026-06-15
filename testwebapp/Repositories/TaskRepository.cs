using Microsoft.EntityFrameworkCore;
using testwebapp.Entites;
using testwebapp.Models;
using testwebapp.Repositories.Interfaces;

namespace testwebapp.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly Context _context;

    public TaskRepository(Context context)
    {
        _context = context;
    }

    public async Task AddAsync(ToDoEntity task)
    {
        await _context.Set<ToDoEntity>().AddAsync(task);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var task = await GetByIdAsync(id);
        if (task != null)
        {
            _context.Set<ToDoEntity>().Remove(task);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<ToDoEntity>> GetAllAsync()
    {
        return await _context.Set<ToDoEntity>().ToListAsync();
    }

    public async Task<ToDoEntity?> GetByIdAsync(int id)
    {
        return await _context.Set<ToDoEntity>().FindAsync(id);
    }

    public async Task UpdateAsync(ToDoEntity task)
    {
        _context.Set<ToDoEntity>().Update(task);
        await _context.SaveChangesAsync();
    }
}
