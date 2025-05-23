using Microsoft.EntityFrameworkCore;
using OneTimeSecretClone.Services;
namespace OneTimeSecretClone
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // adding Connection String
            builder.Services.AddDbContext<SecretDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnectionString"))) ;
//Achieving Dependency Injection
            builder.Services.AddScoped<ISecretService, SecretServiceImpl>();



            // public IConfiguration Configuration { get; }
            //
            // public void ConfigureServices(IServiceCollection services)
            // {
            //     services.AddDbContext<SecretDbContext>(options =>
            //         options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            //
            //     services.AddControllersWithViews();
            // }


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
