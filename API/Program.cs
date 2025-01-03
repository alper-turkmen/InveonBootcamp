using Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Core.Entities;
using Microsoft.OpenApi.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<AuthenticationService>();

builder.Services.AddScoped<ProfileService>();

builder.Services.AddScoped<CourseService>();

builder.Services.AddScoped<VideoService>();


var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ClockSkew = TimeSpan.Zero 
    };
});

builder.Services.AddControllers();

builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Kurs Projesi API",
        Description = "Kurs Projesi API, ASP.NET Core Web API ile geliştirilmiştir. Bu API, kurslar, videolar ve kullanicilari yönetmek için kullanılır.",
        Version = "v1"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseRouting();
app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
    c.RoutePrefix = string.Empty;
});

app.UseAuthentication(); 
app.UseAuthorization();  
app.MapControllers();

var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var userManager = services.GetRequiredService<UserManager<User>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    if (!await roleManager.RoleExistsAsync("Teacher"))
        await roleManager.CreateAsync(new IdentityRole("Teacher"));

    if (!await roleManager.RoleExistsAsync("User"))
        await roleManager.CreateAsync(new IdentityRole("User"));

    if (await userManager.FindByEmailAsync("teacher@example.com") == null)
    {
        var teacher = new User
        {
            UserName = "teacher",
            Email = "teacher@example.com",
            EmailConfirmed = true
        };
        var result = await userManager.CreateAsync(teacher, "Teacher123!");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(teacher, "Teacher");
        }
        else
        {
            foreach (var error in result.Errors)
            {
                Console.WriteLine($"Error: {error.Description}");
            }
        }
    }

    if (await userManager.FindByEmailAsync("user@example.com") == null)
    {
        var user = new User
        {
            UserName = "user",
            Email = "user@example.com",
            EmailConfirmed = true
        };
        var result = await userManager.CreateAsync(user, "User123!");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "User");
        }
        else
        {
            foreach (var error in result.Errors)
            {
                Console.WriteLine($"Error: {error.Description}");
            }
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error during seeding: {ex.Message}");
}

app.Run();