using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkTimeTracker;
using WorkTimeTracker.Models;

namespace WorkTimeTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeLogsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TimeLogsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetLogs(DateTime? date, int? month)
        {
            var query = _context.TimeLogs.Include(l => l.WorkTask).AsQueryable();

            if (date.HasValue)
                query = query.Where(l => l.Date.Date == date.Value.Date);

            if (month.HasValue)
                query = query.Where(l => l.Date.Month == month.Value);

            var logs = await query.ToListAsync();

            if (date.HasValue)
            {
                var totalHours = logs.Sum(l => l.Hours);
                string stickerColor = totalHours < 8 ? "Yellow" : (totalHours == 8 ? "Green" : "Red");

                return Ok(new { TotalHours = totalHours, Status = stickerColor, Data = logs });
            }

            return Ok(logs);
        }

        [HttpPost]
        public async Task<ActionResult> PostLog(TimeLog log)
        {
            // Валидация: Количество часов от 0 до 24
            if (log.Hours <= 0 || log.Hours > 24)
                return BadRequest("Часы должны быть от 0 до 24.");

            // Валидация: Неактивные задачи нельзя выбрать
            var task = await _context.Tasks.FindAsync(log.WorkTaskId);
            if (task == null || !task.IsActive)
                return BadRequest("Задача не существует или неактивна.");

            // Валидация: Суммарно за день не более 24 часов
            var existingHours = await _context.TimeLogs
                .Where(l => l.Date.Date == log.Date.Date)
                .SumAsync(l => l.Hours);

            if (existingHours + log.Hours > 24)
                return BadRequest($"Превышен лимит 24 часа. Уже внесено: {existingHours}");

            _context.TimeLogs.Add(log);
            await _context.SaveChangesAsync();
            return Ok(log);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutLog(int id, TimeLog log)
        {
            if (id != log.Id) return BadRequest();

            var oldLog = await _context.TimeLogs.AsNoTracking().FirstOrDefaultAsync(l => l.Id == id);
            if (oldLog == null) return NotFound();

            var task = await _context.Tasks.FindAsync(oldLog.WorkTaskId);

            if (task != null && !task.IsActive && oldLog.WorkTaskId != log.WorkTaskId)
            {
                return BadRequest("Нельзя изменить задачу, так как она стала неактивной.");
            }

            _context.Entry(log).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}