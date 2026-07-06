using System;

namespace WorkTimeTracker.Models;

public class TimeLog
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Hours { get; set; }
    public string Description { get; set; } = string.Empty;
    public int WorkTaskId { get; set; }
    public WorkTask? WorkTask { get; set; }
}