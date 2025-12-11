using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Wayplot_Backend.Models;

namespace Wayplot_Backend.Database
{
    public class WayplotDbContext : DbContext
    {
        public WayplotDbContext(DbContextOptions<WayplotDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Map> Maps { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Scopes)
                    .HasConversion(
                        v => System.Text.Json.JsonSerializer.Serialize(v, (System.Text.Json.JsonSerializerOptions?)null),
                        v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions?)null) ?? new List<string>()
                    );
            });
        }
    }
}
