using Wayplot_Backend.Constants;

namespace Wayplot_Backend.Models
{
    public class Map
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required Guid UploadedBy { get; set; }
        public required string GpxUrl { get; set; }
        public required string JsonUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
        public MapStatus Status { get; set; } = MapStatus.ACTIVE;
        public MapVisibility Visibility { get; set; } = MapVisibility.PRIVATE;
        public ICollection<User> SharedWithUsers { get; set; } = [];
    }
}
