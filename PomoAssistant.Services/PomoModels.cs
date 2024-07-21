using PomoAssistant.Core;

namespace PomoAssistant.Services;

public static class PomoModels
{
    public sealed class ReadModel
    {
        public Guid Id { get; set; }
        public DateTime CompletedAt { get; set; }
        public Intent Intent { get; set; }
        public string Description { get; set; } = string.Empty;
        public int DurationInMinutes { get; set; }
    }
    
    public sealed class CreateRequest
    {
        public DateTime CompletedAt { get; set; }
        public Intent Intent { get; set; }
        public string Description { get; set; } = string.Empty;
    }

    public sealed class UpdateRequest
    {
        public Guid Id { get; set; }
        public DateTime CompletedAt { get; set; }
        public Intent Intent { get; set; }
        public string Description { get; set; } = string.Empty;
    }

    public sealed class PatchRequest
    {
        public Guid Id { get; set; }
        public DateTime? CompletedAt { get; set; }
        public Intent? Intent { get; set; }
        public string? Description { get; set; }
    }

    public sealed class SearchRequest
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}