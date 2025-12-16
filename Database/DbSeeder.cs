using Wayplot_Backend.Models;
using Wayplot_Backend.Constants;

namespace Wayplot_Backend.Database
{
    public static class DbSeeder
    {
        public static void Seed(WayplotDbContext db)
        {
            Console.WriteLine("Executing seed script; Ensure this is on Development only.");

            if (db.Users.Any() || db.Maps.Any())
                return;

            var user1 = new User
            {
                Id = Guid.NewGuid(),
                Name = "Sabari",
                Email = "sabari@wayplot.com",
                Password = "password",
                AuthType = AuthType.PASSWORD,
                Role = UserRole.ADMIN
            };

            var user2 = new User
            {
                Id = Guid.NewGuid(),
                Name = "Joe",
                Email = "joe@wayplot.com",
                Password = "password",
                AuthType = AuthType.PASSWORD,
                Role = UserRole.REGULAR
            };

            var user3 = new User
            {
                Id = Guid.NewGuid(),
                Name = "Suresh",
                Email = "suresh@wayplot.com",
                AuthType = AuthType.PASSWORD,
                Role = UserRole.ADMIN
            };

            var user4 = new User
            {
                Id = Guid.NewGuid(),
                Name = "Subash",
                Email = "subash@wayplot.com",
                AuthType = AuthType.PASSWORD,
                Role = UserRole.ADMIN
            };

            var map1 = new Map
            {
                Id = Guid.NewGuid(),
                Name = "Kalasalingam Campus Map v2",
                UploadedBy = user1.Id,
                GpxUrl = "https://res.cloudinary.com/dezwo04ym/raw/upload/v1765508403/gpx_graphs/raw_gpx/KLU_Campus_All_Roads_42014d5c",
                JsonUrl = "https://res.cloudinary.com/dezwo04ym/raw/upload/v1765508404/gpx_graphs/json_graph/KLU_Campus_All_Roads",
                Visibility = MapVisibility.PUBLIC
            };

            
            map1.SharedWithUsers.Add(user2);
            map1.SharedWithUsers.Add(user3);

            db.Users.AddRange(user1, user2, user3);
            db.Maps.AddRange(map1);

            db.SaveChanges();
        }
    }
}
