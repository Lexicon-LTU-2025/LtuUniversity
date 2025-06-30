using LtuUniversity.Data;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace LtuUniversity.Extensions;

public static class WebApplicationExtensions
{
    public static async Task SeedDataAsync(this IApplicationBuilder app)
    {
        using(var scope = app.ApplicationServices.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;
            var context = serviceProvider.GetRequiredService<UniversityContext>();

            //await context.Database.EnsureDeletedAsync();
            //await context.Database.MigrateAsync();

            try
            {
                await SeedData.InitAsync(context);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }
    }
}
