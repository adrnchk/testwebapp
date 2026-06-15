using AutoMapper;
using testwebapp.Entites;
using testwebapp.Models;
using testwebapp.Repositories.Interfaces;
using testwebapp.Services.Interfaces;

namespace testwebapp.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;

        public TaskService(ITaskRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TodoTask>> GetAllTasksAsync()
        {
            return _mapper.Map<List<TodoTask>>(await _taskRepository.GetAllAsync()) ?? new List<TodoTask>();
        }

        public async Task<TodoTask> GetTaskByIdAsync(int id)
        {
            return _mapper.Map<TodoTask>(await _taskRepository.GetByIdAsync(id)) ?? throw new KeyNotFoundException("Task not found");
        }

        public async Task CreateTaskAsync(TodoTask task)
        {
            if (task == null) throw new ArgumentNullException(nameof(task));
            await _taskRepository.AddAsync(_mapper.Map<ToDoEntity>(task));
        }

        public async Task UpdateTaskAsync(TodoTask task)
        {
            if (task == null) throw new ArgumentNullException(nameof(task));
            await _taskRepository.UpdateAsync(_mapper.Map<ToDoEntity>(task));
        }

        public async Task DeleteTaskAsync(int id)
        {
            await _taskRepository.DeleteAsync(id);
        }
    }
}
