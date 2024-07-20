namespace PomoAssistant.Core;

public sealed class Pomo(DateTime completedAt, Intent intent)
{
    public Guid Id { get; } = Guid.NewGuid();
    public DateTime CompletedAt { get; set; } = completedAt;
    public Intent Intent { get; set; } = intent;
    public string Description { get; set; } = string.Empty;
    public int DurationInMinutes => 25;
}