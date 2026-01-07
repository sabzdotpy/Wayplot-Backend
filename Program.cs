
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Wayplot_Backend.Database;
using Wayplot_Backend.Middleware;
using Wayplot_Backend.Repositories;
using Wayplot_Backend.Services;
using Wayplot_Backend.Utilities;
using Microsoft.AspNetCore.Authentication.BearerToken;

namespace Wayplot_Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<WayplotDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IAuthRepository, AuthRepository>();
            builder.Services.AddScoped<IMapRepository, MapRepository>();
            builder.Services.AddScoped<IAnalyticRepository, AnalyticRepository>();

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IMapService, MapService>();
            builder.Services.AddScoped<IAnalyticRecordService, AnalyticRecordService>();

            builder.Services.AddScoped<IJwtUtil, JwtUtility>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontendDev",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:4200")
                              .AllowAnyHeader()
                              .AllowCredentials()
                              .AllowAnyMethod();
                    });
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Wayplot API",
                    Version = "v1"
                });

                var jwtScheme = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Description = "Paste JWT token only (no 'Bearer ' prefix)",

                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                };

                options.AddSecurityDefinition("Bearer", jwtScheme);
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        jwtScheme,
                        Array.Empty<string>()
                    }
                });
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                using (var scope = app.Services.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<WayplotDbContext>();
                    DbSeeder.Seed(db);
                }

                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();
            app.UseWhen(
                ctx =>
                    ctx.Request.Path.StartsWithSegments("/User") ||
                    ctx.Request.Path.StartsWithSegments("/Map"),
                appBuilder =>
                {
                    appBuilder.UseMiddleware<JwtMiddleware>();
                    appBuilder.UseMiddleware<RbacMiddleware>();
                }
            );

            app.UseCors("AllowFrontendDev");
            app.UseHttpsRedirection();
            //app.UseAuthorization();
            app.MapGet("/", () => "OK");
            app.MapControllers();
            app.Run();
        }
    }
}
