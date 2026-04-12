using Company.Data;
using Company.Data.Data;
using Company.Data.Data.Seed;
using Company.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Company.Intranet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var app = BuildApplication(args);
            ApplyAutomaticMigrations(app);
            InitializeSeedData(app);
            ConfigureEnvironment(app);
            ConfigureHttpPipeline(app);
            app.Run();
        }

        private static void InitializeSeedData(WebApplication app)
        {
            SeedData.InitializeAsync(app.Services).GetAwaiter().GetResult();
        }

        private static WebApplication BuildApplication(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<MechanicDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("TurboMechanicContext")
                    ?? throw new InvalidOperationException("Connection string 'TurboMechanicContext' not found.")));

            builder.Services
                .AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                })
                .AddEntityFrameworkStores<MechanicDbContext>()
                .AddDefaultTokenProviders();

            DependencyInjectionFactory.Resolve(builder.Services, builder.Configuration);

            builder.Services.AddControllersWithViews();

            return builder.Build();
        }

        private static void ApplyAutomaticMigrations(WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            try
            {
                var databaseContext = scope.ServiceProvider.GetRequiredService<MechanicDbContext>();
                databaseContext.Database.Migrate();
            }
            catch (Exception exception)
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                logger.LogError(exception, "Error during database migration.");
            }
        }

        private static void ConfigureEnvironment(WebApplication app)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
        }

        private static void ConfigureHttpPipeline(WebApplication app)
        {
            var runningInContainer =
                Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";

            if (!runningInContainer)
            {
                app.UseHttpsRedirection();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();
        }
    }
}