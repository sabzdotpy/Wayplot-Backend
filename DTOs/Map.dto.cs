using Wayplot_Backend.Constants;
using Wayplot_Backend.Models;

namespace Wayplot_Backend.DTOs
{

    public class MapWithAnalyticsDto
    {
        public required Map Map { get; set; }
        public int ViewCount { get; set; }
        public int DownloadCount { get; set; }
    }

    public class MapResponseDTO
    {
        public required bool IsSuccess { get; set; }
        public required bool IsError { get; set; }
        public string? Message { get; set; }
        public string? ErrorMessage { get; set; }
        public dynamic? data { get; set; }
    }

    public class CreateMapDTO
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required string GpxUrl { get; set; }
        public required string JsonUrl { get; set; }
        public MapStatus? Status { get; set; } = MapStatus.ACTIVE;
        public MapVisibility? Visibility { get; set; } = MapVisibility.PRIVATE;
    }

    public class EditMapDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? GpxUrl { get; set; }
        public string? JsonUrl { get; set; }

        public MapStatus? Status { get; set; } = MapStatus.ACTIVE;
        public MapVisibility? Visibility { get; set; } = MapVisibility.PRIVATE;
    }

    public class ChangeMapVisibilityDTO
    {
        public MapVisibility visibility { get; set; }
    }

    public class ChangeMapStatusDTO
    {
        public MapStatus status { get; set; }
    }

    public class GetMapUrlResponseDTO
    {
        public required string GpxUrl { get; set; }
        public required string JsonUrl { get; set; }
    }

    public class LogDownloadDTO
    {
        public required Guid actorId { get; set; }
        public required Guid mapId { get; set; }
    }
}
