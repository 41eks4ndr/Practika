namespace WorkTimeTracker.Models;

public class WorkTask
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public int ProjectId { get; set; }
    public Project? Project { get; set; }
}