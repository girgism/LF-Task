using Application.app.ServicesDependencyInjection;
using Domain.app.Entities;
using ElectronicsApp.Server.ServicesDependencyInjection;
using Infrastructure.app.DBConfigurations;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAPIDependencyInjection();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddServicesForApplicationLayer();

// Identity Configuration
builder.Services.AddDataProtection();
builder.Services.AddIdentityApiEndpoints<User>()
                .AddRoles<Role>()
                .AddEntityFrameworkStores<ElectronicsContext>()
                .AddSignInManager()
                .AddApiEndpoints();

builder.Services.Configure<BearerTokenOptions>(IdentityConstants.BearerScheme, opt =>
{
    opt.BearerTokenExpiration = TimeSpan.FromHours(1);
    opt.RefreshTokenExpiration = TimeSpan.FromMinutes(55500);
});
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.SignIn.RequireConfirmedEmail = false;
});

builder.Services.AddCors(options =>
{
    var corsOrigins = builder.Configuration.GetValue<string>("Cors")?.Split(",") ?? Array.Empty<string>();
    options.AddDefaultPolicy(config =>
    {
        config.WithOrigins(corsOrigins)
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
builder.Services.AddSwaggerGen();

builder.Services.AddOpenApiDocument(document =>
{
    document.Title = "Electronics APIs";
});

var app = builder.Build();

try
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<ElectronicsContext>();
        await dbContext.Database.MigrateAsync();
        await SeedDefaultUser(scope);
    }

    async Task SeedDefaultUser(IServiceScope scope)
    {
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

        var defaultUser = await userManager.FindByNameAsync("admin");
        if (defaultUser == null)
        {
            var user = new User
            {
                UserName = "admin",
                Email = "admin@electronics.com",
            };
            var result = await userManager.CreateAsync(user, "password!11");
        }
    }
    app.UseDefaultFiles();
    app.UseStaticFiles();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseOpenApi();
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    else
    {
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseCors();
    app.UseRouting();
    app.UseAuthorization();
    app.MapControllers();
    app.MapIdentityApi<User>();

    app.MapFallbackToFile("/index.html");

    app.Run();
}
catch (Exception)
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
}

