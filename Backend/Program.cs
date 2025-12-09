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
            var builder = WebApplication.CreateBuilder(args);

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

            // Services
            builder.Services.AddScoped<ICandidateService, CandidateService>();
            builder.Services.AddScoped<IPartyService, PartyService>();
            builder.Services.AddScoped<IStatisticsService, StatisticsService>();
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
            var wwwrootPath = Path.Combine(app.Environment.ContentRootPath, "Frontend", "wwwroot");
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(wwwrootPath),
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