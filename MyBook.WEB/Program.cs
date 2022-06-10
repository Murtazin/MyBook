using System.IO.Compression;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using MyBook.BLL.EmailServices;
using MyBook.DAL.Contexts;
using MyBook.DAL.Entities;
using MyBook.DAL.Identity;
using MyBook.WEB.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Email service
builder.Services.AddSingleton(builder.Configuration.
    GetSection("EmailConfiguration").Get<EmailConfiguration>());
builder.Services.AddScoped<IEmailService,EmailService>();

builder.Services.AddControllersWithViews();
// Database context

// var provider = builder.Configuration.GetValue("Provider", "Pgsql");
// builder.Services.AddDbContext<ApplicationContext>(
//     options => _ = provider switch
//     {
//         "Pgsql" => options.UseNpgsql(
//             builder.Configuration.GetConnectionString("sqlConnection"),
//             x => x.MigrationsAssembly("PostgresMigrations")),
//
//         "Mssql" => options.UseSqlServer(
//             builder.Configuration.GetConnectionString("sqlConnection"),
//             x => x.MigrationsAssembly("SqlServerMigrations")),
//
//         _ => throw new Exception($"Unsupported provider: {provider}")
//     });
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(
    options => options.UseNpgsql(connectionString, b => b.MigrationsAssembly("MyBook.WEB"))
);

// Identity
builder.Services.AddIdentity<User, Role>(option=>option.SignIn.RequireConfirmedEmail=true)
    .AddEntityFrameworkStores<ApplicationContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
        {
            options.LoginPath = new PathString("/Auth/Login");
        }
    );
    // .AddGoogle(options =>
    // {
    //     options.ClientId = "555333035392-hlv99ej08vggopmquhg3m8cvgfoukb75.apps.googleusercontent.com";
    //     options.ClientSecret = "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";
    // });

// // SignalR
// builder.Services.AddSignalR();
// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policyBuilder =>
        {
             policyBuilder.WithOrigins("https://outsiders.somee.com");
        });
});
// сжатие ответов
builder.Services.AddResponseCompression(options=>options.EnableForHttps = true);
builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Optimal;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = new PathString("/Home/AccessDenied");
});

builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Auto-migrations
// using (var scope = app.Services.CreateScope())
// {
//     #region migrations
//
//     var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
//     db.Database.Migrate();
//
//     #endregion
//
//     #region roles
//
//     var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
//     if (!roleManager.Roles.Any())
//     {
//         await roleManager.CreateAsync(new Role {Name = "Admin"});
//         await roleManager.CreateAsync(new Role {Name = "User"});
//         await roleManager.CreateAsync(new Role {Name = "UserSub"});
//     }
//
//     #endregion
// }

// сжатие ответов
app.UseResponseCompression();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.Use(async (context, next) =>
{
    await next();
    switch (context.Response.StatusCode)
    {
        case 404:
            context.Request.Path = "/Home/PageNotFound";
            await next();
            break;
        case 403:
            context.Request.Path = "/Home/AccessDenied";
            await next();
            break;
    }
});

// URL Rewriting
var options = new RewriteOptions()
    .AddRedirect("(.*)/$", "$1")                // удаление концевого слеша
    .AddRedirect("(?i)catalog[/]?$", "home") // переадресация с catalog на home
    .AddRedirect("(?i)auth[/]?$", "home"); // переадресация с auth на home
app.UseRewriter(options);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSubscription();

// CORS
app.UseCors("AllowAll");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();