using Company.Data;
using Microsoft.EntityFrameworkCore;

namespace Company.PortalWWW
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var app = BuildApplication(args);
            ConfigureEnvironment(app);
            ConfigureHttpPipeline(app);
            app.Run();
        }

        private static WebApplication BuildApplication(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<MechanicDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("TurboMechanicContext")
                    ?? throw new InvalidOperationException("Connection string 'TurboMechanicContext' not found.")));

            builder.Services.AddControllersWithViews();

            return builder.Build();
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

            app.UseAuthorization();

            app.MapStaticAssets();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();
        }
    }
}