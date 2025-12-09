using System.Text.Json;
using Wayplot_Backend.Constants;

namespace Wayplot_Backend.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }
        public AuthType AuthType { get; set; }
        public UserRole Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public UserStatus Status { get; set; }
        public List<string> Scopes {  get; set; }

        public string GetScopesJsonFromArray() => JsonSerializer.Serialize(Scopes);
        public void LoadScopesFromJson(string json) => Scopes = JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>();

    }
}
