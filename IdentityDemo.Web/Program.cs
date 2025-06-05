using IdentityDemo.Application.Users;
using IdentityDemo.Infrastructure.Persistence;
using IdentityDemo.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace IdentityDemo.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllersWithViews();
        builder.Services.AddTransient<IUserService, UserService>();
        builder.Services.AddTransient<IIdentityUserService, IdentityUserService>();

        builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = true;
        })
            .AddEntityFrameworkStores<ApplicationContext>() // DENNA RAD FUNKAR EJ
            .AddDefaultTokenProviders();

        builder.Services.ConfigureApplicationCookie(
            x => x.LoginPath = "/login");

        // Konfigurera EF
        builder.Services.AddDbContext<ApplicationContext>(
            o => o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(option =>
            {
                option.LoginPath = "/Account/Login";
            });

        var app = builder.Build();

        app.UseHttpsRedirection();
        app.MapControllers();
        app.UseAuthorization();

        // Fix for CS0120: Use the HttpContext from the current request pipeline
        //app.Use(async (context, next) =>
        //{
        //    Claim[] claims = new Claim[]
        //    {
        //        new Claim(ClaimTypes.Name, "Student"),
        //    };

        //    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        //    await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
        //    await next();
        //});

        app.Run();
    }
}