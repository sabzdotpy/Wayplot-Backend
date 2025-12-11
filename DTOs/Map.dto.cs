using Wayplot_Backend.Constants;

namespace Wayplot_Backend.DTOs
{
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
        public required string Url { get; set; }
        public MapStatus? Status { get; set; } = MapStatus.ACTIVE;
        public MapVisibility? Visibility { get; set; } = MapVisibility.PRIVATE;
    }

    public class EditMapDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Url { get; set; }
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
}
