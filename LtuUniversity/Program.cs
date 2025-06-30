using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using LtuUniversity.Data;
using LtuUniversity.Extensions;

namespace LtuUniversity
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            // Add services to the container.
            
            builder.Services.AddDbContext<UniversityContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("UniversityContext") ?? throw new InvalidOperationException("Connection string 'UniversityContext' not found.")));


            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();





            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                await app.SeedDataAsync();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
