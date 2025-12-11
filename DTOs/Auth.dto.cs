using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Text.Json;
using Wayplot_Backend.Constants;

namespace Wayplot_Backend.DTOs
{
    public class LoginRequestDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }

    public class LoginResponseDto
    {
        public required bool IsSuccess { get; set; }
        public string? Token { get; set; }
        public string? ErrorMessage { get; set; }
    }

    public sealed class SignupRequestDto
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required AuthType AuthType { get; set; }
        public required UserRole UserRole { get; set; }
        public List<string> Scopes { get; set; } = [];
        public string GetScopesJsonFromArray() => JsonSerializer.Serialize(Scopes);
        public void LoadScopesFromJson(string json) => Scopes = JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>();
    }

}
