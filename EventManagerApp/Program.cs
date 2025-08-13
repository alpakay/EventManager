using Repositories;
using Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Services.Contracts;
using Services;
using Entities.Models;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var cs = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<RepositoryContext>(options =>
    options.UseSqlServer(
        cs,
        sql => sql.MigrationsAssembly("EventManagerApp")
    )
);

//repositoryManager
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IKeyRepository, KeyRepository>();

//serviceManager
builder.Services.AddScoped<IServiceManager, ServiceManager>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEncryptionService, EncryptionService>();
builder.Services.AddScoped<IFileService, FileService>();

builder.Services.AddHttpContextAccessor();

// Add AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Auth
builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", options =>
    {
        options.LoginPath = "/Management/User/Login";
        options.AccessDeniedPath = "/Management/User/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    });

var app = builder.Build();

if (app.Configuration.GetValue<bool>("RUN_MIGRATIONS", false))
{
    using (var scope = builder.Services.BuildServiceProvider().CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<RepositoryContext>();
        db.Database.Migrate();
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();


app.MapControllerRoute(
    name: "management",
    pattern: "Management/{controller=Home}/{action=Index}/{id?}",
    defaults: new { area = "Management" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();



app.Run();
