using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.IO;
using ASP1.Backend.Data;
using ASP1.Backend.Data.Repositories;
using ASP1.Backend.Services.Interfaces;
using ASP1.Backend.Services.Implementations;

namespace ASP1.Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Get project root - find the directory containing the .csproj file
            // Use AppContext.BaseDirectory which is more reliable than GetCurrentDirectory
            var baseDir = AppContext.BaseDirectory;
            var projectRoot = baseDir;
            
            // Navigate up from bin/Debug/net8.0 to find project root
            var dir = new DirectoryInfo(baseDir);
            while (dir != null)
            {
                if (File.Exists(Path.Combine(dir.FullName, "ASP1.csproj")))
                {
                    projectRoot = dir.FullName;
                    break;
                }
                dir = dir.Parent;
            }
            
            // Fallback: if still not found, try current directory
            if (!File.Exists(Path.Combine(projectRoot, "ASP1.csproj")))
            {
                var currentDir = Directory.GetCurrentDirectory();
                if (File.Exists(Path.Combine(currentDir, "ASP1.csproj")))
                {
                    projectRoot = currentDir;
                }
                else if (currentDir.EndsWith("Backend") || currentDir.Contains("\\Backend"))
                {
                    projectRoot = Path.GetFullPath(Path.Combine(currentDir, ".."));
                }
            }
            
            var builder = WebApplication.CreateBuilder(args);
            
            // Configure to load appsettings from project root
            builder.Configuration
                .SetBasePath(projectRoot)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            // Add services to the container.
            builder.Services.AddControllersWithViews()
                .AddRazorOptions(options =>
                {
                    // Configure views to be in Frontend folder
                    options.ViewLocationFormats.Clear();
                    options.ViewLocationFormats.Add("/Frontend/Views/{1}/{0}.cshtml");
                    options.ViewLocationFormats.Add("/Frontend/Views/Shared/{0}.cshtml");
                    options.AreaViewLocationFormats.Clear();
                    options.AreaViewLocationFormats.Add("/Frontend/Areas/{2}/Views/{1}/{0}.cshtml");
                    options.AreaViewLocationFormats.Add("/Frontend/Areas/{2}/Views/Shared/{0}.cshtml");
                });

            // Database Context
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Repositories
            builder.Services.AddScoped<ICandidateRepository, CandidateRepository>();
            builder.Services.AddScoped<IPartyRepository, PartyRepository>();
            builder.Services.AddScoped<IElectionRepository, ElectionRepository>();
            builder.Services.AddScoped<ICountyRepository, CountyRepository>();
            builder.Services.AddScoped<IVoteRepository, VoteRepository>();
            builder.Services.AddScoped<IElectionCandidateRepository, ElectionCandidateRepository>();

            // Services
            builder.Services.AddScoped<ICandidateService, CandidateService>();
            builder.Services.AddScoped<IPartyService, PartyService>();
            builder.Services.AddScoped<IStatisticsService, StatisticsService>();
            builder.Services.AddScoped<IElectionService, ElectionService>();
            builder.Services.AddScoped<IVoteService, VoteService>();
            builder.Services.AddSingleton<ICandidateGeneratorService, CandidateGeneratorService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            // Configure static files from Frontend/wwwroot
            // Use the same project root we used for configuration
            var wwwrootPath = Path.Combine(projectRoot, "Frontend", "wwwroot");
            var fullWwwrootPath = Path.GetFullPath(wwwrootPath);
            
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(fullWwwrootPath),
                RequestPath = ""
            });

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
//it should send me to login not here