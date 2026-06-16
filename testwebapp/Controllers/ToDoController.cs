using Microsoft.AspNetCore.Mvc;
using testwebapp.Models;
using testwebapp.Services.Interfaces;

namespace testwebapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public ToDoController(ITaskService taskService)
        {
            _taskService = taskService;
        }
        [HttpGet("health")]
        public async Task<ActionResult<IEnumerable<TodoTask>>> HealthCheck()
        {
            return Ok("Everything is ok");
        }


        // GET: api/todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoTask>>> GetAll()
        {
            var items = await _taskService.GetAllTasksAsync();
            return Ok(items);
        }

        // GET: api/todo/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoTask>> GetById(int id)
        {
            var item = await _taskService.GetTaskByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        // POST: api/todo
        [HttpPost]
        public async Task<IActionResult> Create(TodoTask item)
        {
            try
            {
                await _taskService.CreateTaskAsync(item);
                return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
            }
            catch(ArgumentNullException ex)
            {
                // Log the exception (not implemented here)
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                return StatusCode(500, "An error occurred while creating the task.");
            }
        }

        // PUT: api/todo/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(TodoTask item)
        {
            try
            {
                await _taskService.UpdateTaskAsync(item);
                return Ok();
            }
            catch (ArgumentNullException ex)
            {
                // Log the exception (not implemented here)
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                return StatusCode(500, "An error occurred while creating the task.");
            }
        }

        // DELETE: api/todo/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _taskService.DeleteTaskAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                return StatusCode(500, "An error occurred while creating the task.");
            }
        }
    }
}
