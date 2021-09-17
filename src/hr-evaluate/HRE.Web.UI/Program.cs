using System;
using HRE.Application;
using HRE.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HRE.Core.DI;
using HRE.Core.Extensions;
using HRE.Core.Identity;
using HRE.Core.Repositories;
using HRE.Web.UI.Customization;
using HRE.Web.UI.Seeds;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Console.OutputEncoding = Encoding.UTF8;
Console.Title = "HR Evaluate";

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

builder.Services.AddAutoMapper(options =>
{
    options.AddProfile<ModelMapping>();
});

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<HrDbContext>(options =>
{
    options.UseSqlServer(connectionString);
    options.UseLazyLoadingProxies();
});
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services
       .AddDefaultIdentity<CustomUser>(options =>
       {
           options.Stores.MaxLengthForKeys = 128;

           options.SignIn.RequireConfirmedAccount = false;
           options.SignIn.RequireConfirmedEmail = false;
           options.SignIn.RequireConfirmedPhoneNumber = false;

           options.User.RequireUniqueEmail = true;
            // options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_";

            options.Password.RequireDigit = false;
           options.Password.RequireLowercase = false;
           options.Password.RequireNonAlphanumeric = false;
           options.Password.RequireUppercase = false;
           options.Password.RequiredLength = 3;
           options.Password.RequiredUniqueChars = 0;
       })
       .AddRoles<CustomRole>()
       .AddUserManager<CustomUserManager>()
       .AddRoleManager<CustomRoleManager>()
       .AddEntityFrameworkStores<HrDbContext>()
       .AddDefaultTokenProviders();

builder.Services.AddScoped<DbContext, HrDbContext>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
Assembly.GetAssembly(typeof(BaseSeed)).GetTypesAssignableFrom<ITransientDependency>().ForEach(t => builder.Services.AddTransient(t));
Assembly.GetAssembly(typeof(HrAppConsts)).GetTypesAssignableWithInterfacesFrom<IScopedDependency>().ForEach(pair => pair.Value.ForEach(i => builder.Services.AddScoped(i, pair.Key)));
Assembly.GetAssembly(typeof(HrAppConsts)).GetTypesAssignableWithInterfacesFrom<ITransientDependency>().ForEach(pair => pair.Value.ForEach(i => builder.Services.AddTransient(i, pair.Key)));
Assembly.GetAssembly(typeof(HrAppConsts)).GetTypesAssignableWithInterfacesFrom<ISingletonDependency>().ForEach(pair => pair.Value.ForEach(i => builder.Services.AddSingleton(i, pair.Key)));

builder.Services.Configure<BrotliCompressionProviderOptions>(options => { options.Level = CompressionLevel.Optimal; });
builder.Services.Configure<GzipCompressionProviderOptions>(options => { options.Level = CompressionLevel.Optimal; });
builder.Services.AddResponseCaching();
builder.Services.AddResponseCompression(options =>
{
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
    options.EnableForHttps = true;
    // options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "image/svg+xml" });
});

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    //options.Cookie.HttpOnly = true;
    //options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

    options.LoginPath = "/account/login";
    options.LogoutPath = "/account/logout";
    options.AccessDeniedPath = "/account/access-denied";
    options.ExpireTimeSpan = TimeSpan.FromDays(3);
    options.SlidingExpiration = true;
    options.Cookie.Name = "HR.Identity";
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

UpgradeDatabase(app);

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

app.UseAuthentication();
app.UseAuthorization();

app.UseResponseCaching();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}",
    constraints: new { area = "Admin" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Switcher}/{id?}");
//app.MapRazorPages();

app.Run();


void UpgradeDatabase(IApplicationBuilder app)
{
    using (var serviceScope = app.ApplicationServices.CreateScope())
    {
        var dbContext = serviceScope.ServiceProvider.GetRequiredService<HrDbContext>();
        var pendingMigrations = dbContext.Database.GetPendingMigrations();
        if (pendingMigrations.Any())
        {
            dbContext.Database.Migrate();
        }

        dbContext.Database.EnsureCreated();

        var seeds = new BaseSeed[]
        {
            serviceScope.ServiceProvider.GetRequiredService<RoleSeed>(),
            serviceScope.ServiceProvider.GetRequiredService<UserSeed>()
        };
        var cancellationToken = CancellationToken.None;
        foreach (var seed in seeds)
        {
            var task = seed.RunAsync(cancellationToken);
            Task.WaitAll(task);
        }
    }
}