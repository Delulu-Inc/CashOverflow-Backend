using CashOverflow.Application.Common.Interfaces;
using CashOverflow.Domain.Entities;
using CashOverflow.Infrastructure.Identity;
using CashOverflow.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IAuthService, AuthService>();

//Add ApplicationDbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
           .UseSnakeCaseNamingConvention());


// Register Identity with roles
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

//Add jwt

builder.Services.AddAuthentication(
     options =>
     {
         options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
         options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
     }

    ).AddJwtBearer(
     options =>
     {
         options.TokenValidationParameters = new TokenValidationParameters
         {
             ValidateIssuer = true,
             ValidateAudience = true,
             ValidateLifetime = true,
             ValidateIssuerSigningKey = true,

             ValidIssuer = builder.Configuration["Jwt:Issuer"],
             ValidAudience = builder.Configuration["Jwt:Audience"],

             IssuerSigningKey = new SymmetricSecurityKey(
                 Encoding.UTF8.GetBytes(
                     builder.Configuration["Jwt:SecretKey"]!
                 )
             )
         };

         //print the error related to jwt configuring in the console
         options.Events = new JwtBearerEvents
         {
             OnAuthenticationFailed = context =>
             {
                 Console.WriteLine("JWT ERROR: " + context.Exception.Message);
                 return Task.CompletedTask;
             }
         };
     }

    );

var app = builder.Build();

//DbInitializer

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        await DbInitializer.SeedAsync(context, userManager, roleManager);
    }
    catch (Exception ex)
    {
        Console.WriteLine("An error occurred during DB initialization: " + ex.Message);
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();