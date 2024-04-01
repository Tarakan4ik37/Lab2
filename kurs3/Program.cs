using kurs3.Data;
using kurs3.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using kurs.Services.Email;
using Org.BouncyCastle.Asn1;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<Kyrsach2Context>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddRazorPages();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>() // Добавление ролей
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddTransient<IEmailSender, EmailSender>(i =>
    new EmailSender(i.GetRequiredService<IConfiguration>()));

var app = builder.Build();


// Инициализация ролей
InitializeRoles(app.Services).Wait();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}




app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

// Функция для инициализации ролей
async Task InitializeRoles(IServiceProvider serviceProvider)
{
    using (var scope = serviceProvider.CreateScope())
    {
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

        // Создание ролей, если они еще не существуют
        string[] roleNames = { "Администратор", "Пользователь" };
        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        // Создание администратора, если он еще не создан
        var adminUser = new IdentityUser { UserName = "admin@example.com", Email = "admin@example.com" };
        var adminPassword = "Admin123*";
        var user = await userManager.FindByEmailAsync(adminUser.Email);
        if (user == null)
        {
            var createAdmin = await userManager.CreateAsync(adminUser, adminPassword);
            if (createAdmin.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Администратор");
            }
        }
    }
}
