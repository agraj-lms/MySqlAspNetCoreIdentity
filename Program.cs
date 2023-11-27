using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MySqlIdentity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Replace with your connection string.
var connectionString = "server=localhost;user=root;password=toor;database=cinemass";

// Replace 'YourDbContext' with the name of your own DbContext derived class.
builder.Services.AddDbContext<ApplicationDBContext>(
    dbContextOptions => dbContextOptions
        .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
        // The following three options help with debugging, but should
        // be changed or removed for production.
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors()
);

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
       .AddEntityFrameworkStores<ApplicationDBContext>()
       .AddDefaultTokenProviders();

// Configure authentication cookie
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromDays(1);
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.SlidingExpiration = true;
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
