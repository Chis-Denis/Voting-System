using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ASP1.Backend.Data;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        // EF Tools run from project root, so look for appsettings.json in current directory
        var basePath = Directory.GetCurrentDirectory();
        
        // Try to find the project root by looking for ASP1.csproj
        var dir = new DirectoryInfo(basePath);
        while (dir != null)
        {
            if (File.Exists(Path.Combine(dir.FullName, "ASP1.csproj")))
            {
                basePath = dir.FullName;
                break;
            }
            dir = dir.Parent;
        }

        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}
