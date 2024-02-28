using Core.IRepositories;
using Core.Models;
using Infrastracture.Db;
using Infrastracture.Repositories;
using Infrastracture.Service;
using Infrastracture.Service.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookMvc
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("localDb") ?? throw new InvalidOperationException("Connection string 'localDb' not found.");
            builder.Services.AddDbContext<SqlDbContext>(options => options.UseSqlite(connectionString));

            builder.Services.AddIdentity<User, UserRole>(
                options =>
                {
                    options.Password.RequiredUniqueChars = 0;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 8;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireLowercase = false;
                })
                .AddEntityFrameworkStores<SqlDbContext>().AddDefaultTokenProviders();

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddControllersWithViews();
            builder.Services.AddMemoryCache();
            builder.Services.AddSession();

            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<IAuthorService, AuthorService>();
            builder.Services.AddScoped<IPublishingHouseService, PublishingHouseService>();

            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
            builder.Services.AddScoped<IPublishingHouseRepository, PublishingHouseRepository>();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<SqlDbContext>();
                var userManager = services.GetRequiredService<UserManager<User>>();
                var roleManager = services.GetRequiredService<RoleManager<UserRole>>();

                // Ensure database migrations are applied
                dbContext.Database.Migrate();

                // Seed default roles
                if (!await roleManager.RoleExistsAsync("Admin"))
                {
                    await roleManager.CreateAsync(new UserRole("Admin"));
                }
                if (!await roleManager.RoleExistsAsync("User"))
                {
                    await roleManager.CreateAsync(new UserRole("User"));
                }

                // Seed default users
                var adminUser = await userManager.FindByNameAsync("admin@example.com");
                if (adminUser == null)
                {
                    adminUser = new User { UserName = "admin@example.com", Email = "admin@example.com", Name = "Administrator" }; // Dodajemy wartoœæ dla `Name`
                    await userManager.CreateAsync(adminUser, "Admin123!"); // You can change the password here
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }

                var normalUser = await userManager.FindByNameAsync("user@example.com");
                if (normalUser == null)
                {
                    normalUser = new User { UserName = "user@example.com", Email = "user@example.com", Name = "Normal User" }; // Dodajemy wartoœæ dla `Name`
                    await userManager.CreateAsync(normalUser, "User123!"); // You can change the password here
                    await userManager.AddToRoleAsync(normalUser, "User");
                }
            }
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

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.MapRazorPages();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
