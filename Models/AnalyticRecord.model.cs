using Wayplot_Backend.Constants;

namespace Wayplot_Backend.Models
{
    public class AnalyticRecord
    {
        public Guid Id { get; set; }
        public required string Type { get; set; } // 'view', 'download', 'create', 'login', 'signup'
        public Guid ActorId { get; set; }
        public Guid EntityId { get; set; }
        public required AnalyticRecordStatus Status { get; set; } = AnalyticRecordStatus.ACTIVE;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
    }
}
