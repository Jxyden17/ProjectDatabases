<<<<<<< HEAD
using MvcWhatsUp.Repositories;
=======
>>>>>>> 978efa45e8db67273df9b88ecfc78485bd6c7b9c
using ProjectDatabases.Repositories;

namespace ProjectDatabases
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
<<<<<<< HEAD
            builder.Services.AddSingleton<IUserRepository, DbUserRepository>();
            builder.Services.AddSingleton<IStudentRepository, StudentRepository>();
=======
            builder.Services.AddSingleton<IActivityRepository, DbActivityRepository>();
>>>>>>> 978efa45e8db67273df9b88ecfc78485bd6c7b9c

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
