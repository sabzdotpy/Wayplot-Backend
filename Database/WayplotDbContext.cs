using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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
            var scopesConverter = new ValueConverter<List<string>, string>(
               v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
               v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions?)null) ?? new List<string>()
           );

            var scopesComparer = new ValueComparer<List<string>>(
                (a, b) => a.SequenceEqual(b),
                v => v.Aggregate(0, (h, x) => HashCode.Combine(h, x.GetHashCode())),
                v => v.ToList()
            );

            _ = modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity
                    .Property(u => u.Scopes)
                    .HasConversion(scopesConverter)
                    .Metadata.SetValueComparer(scopesComparer);
            });

            modelBuilder.Entity<Map>()
                    .HasMany(m => m.SharedWithUsers)
                    .WithMany(u => u.SharedMaps)
                    .UsingEntity<Dictionary<string, object>>(
                        "MapSharedUsers",
                        j => j
                            .HasOne<User>()
                            .WithMany()
                            .HasForeignKey("UserId")
                            .OnDelete(DeleteBehavior.Cascade),
                        j => j
                            .HasOne<Map>()
                            .WithMany()
                            .HasForeignKey("MapId")
                            .OnDelete(DeleteBehavior.Cascade),
                        j =>
                        {
                            j.HasKey("MapId", "UserId");
                            j.ToTable("MapSharedUsers");
                        }
                    );
        }
    }
}
