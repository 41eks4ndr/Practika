using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkTimeTracker;
using WorkTimeTracker.Models;

namespace WorkTimeTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TasksController(AppDbContext context)
        {
            _context = context;
        }

        // 1. Получить все задачи
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkTask>>> GetTasks()
        {
            return await _context.Tasks.Include(t => t.Project).ToListAsync();
        }

        // 2. Добавить задачу
        [HttpPost]
        public async Task<ActionResult<WorkTask>> PostTask(WorkTask task)
        {
            var project = await _context.Projects.FindAsync(task.ProjectId);
            if (project == null) return BadRequest("Такого проекта не существует.");

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return Ok(task);
        }

        // 3. Изменить задачу
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask(int id, WorkTask task)
        {
            if (id != task.Id) return BadRequest();

            _context.Entry(task).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // 4. Удалить задачу
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound();

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}