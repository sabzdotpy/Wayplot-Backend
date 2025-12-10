using Wayplot_Backend.Constants;

namespace Wayplot_Backend.Utilities
{
    public interface IJwtUtil
    {
        string GenerateJwtToken(Guid id, string email, UserRole role, List<string> scopes);
    }
}
