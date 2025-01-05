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
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin() 
              .AllowAnyMethod() 
              .AllowAnyHeader();
    });
});


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<AuthenticationService>();

builder.Services.AddScoped<ProfileService>();

builder.Services.AddScoped<CourseService>();

builder.Services.AddScoped<VideoService>();

builder.Services.AddScoped<OrderService>();



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
        Title = "Kurs Projesi REST API",
        Description = "Kurs Projesi API. Bu API, kurslar, videolar, siparişler ve kullanıcıları yönetmek için kullanılır.",
        Version = "v1"
    });

   
    c.EnableAnnotations();

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Token giriniz. Başında 'Bearer' olmasına gerek yoktur.",
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

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true; 
});

var app = builder.Build();

app.UseRouting();
app.UseCors("AllowAll"); 
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
    c.RoutePrefix = string.Empty;
});

app.UseAuthentication(); 
app.UseAuthorization();  
app.MapControllers();
app.UseStaticFiles();

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

    if (await userManager.FindByEmailAsync("mehmetdemir@akademi.com") == null)
    {
        var teacher = new User
        {
            UserName = "mehmetDemir",
            Email = "mehmetdemir@akademi.com",
            EmailConfirmed = true,
            ProfilePicture = "/files/profiles/1.jpg",
            Name = "Mehmet",
            Surname = "Demir",
            About = "Merhaba ben Mehmet Demir. 10 yıldır yazılım geliştirme üzerine eğitim veriyorum."
        };

        var result = await userManager.CreateAsync(teacher, "Ogretmen123!");

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
    if (await userManager.FindByEmailAsync("ayseyilmaz@akademi.com") == null)
    {
        var teacher2 = new User
        {
            UserName = "ayseYilmaz",
            Email = "ayseyilmaz@akademi.com",
            EmailConfirmed = true,
            ProfilePicture = "/files/profiles/2.jpg",
            Name = "Ayşe",
            Surname = "Yılmaz",
            About = "Merhaba ben Ayşe Yılmaz. 7 yıldır yazılım geliştirme üzerine eğitim veriyorum."
        };

        var result = await userManager.CreateAsync(teacher2, "Ogretmen123!");

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(teacher2, "Teacher");
        }
        else
        {
            foreach (var error in result.Errors)
            {
                Console.WriteLine($"Error: {error.Description}");
            }
        }

    }

    if (await userManager.FindByEmailAsync("alicelik@akademi.com") == null)
    {
        var user = new User
        {
            UserName = "aliCelik",
            Email = "alicelik@akademi.com",
            EmailConfirmed = true,
            ProfilePicture = "/files/profiles/3.jpg",
            Name = "Ali",
            Surname = "Çelik",
            About = "Merhaba ben Ali Çelik. 5 yıldır yazılım geliştirme üzerine çalışıyorum."
        };

        var result = await userManager.CreateAsync(user, "Ogrenci123!");
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


   if (await userManager.FindByEmailAsync("elifkaya@akademi.com") == null)
    {
            var user2 = new User
        {
            UserName = "elifKaya",
            Email = "elifkaya@akademi.com",
            EmailConfirmed = true,
            ProfilePicture = "/files/profiles/4.jpg",
            Name = "Elif",
            Surname = "Kaya",
            About = "Merhaba ben Elif Kaya. 3 yıldır yazılım geliştirme üzerine çalışıyorum."
        };
        var result = await userManager.CreateAsync(user2, "Ogrenci123!");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user2, "User");
        }
        else
        {
            foreach (var error in result.Errors)
            {
                Console.WriteLine($"Error: {error.Description}");
            }
        }
    }

    var context = services.GetRequiredService<ApplicationDbContext>();
    if (!context.Courses.Any())
    {
        context.Courses.AddRange(
            new Course
            {
                Title = ".NET Core ile Ölçeklenebilir Uygulamalar Geliştirme",
                Description = "Bu kurs ile .NET Core ile ölçeklenebilir uygulamalar geliştirmeyi öğreneceksiniz.",
                Price = 100,
                CoverImage = "/files/courseimg/net.jpg",
                TeacherId = (await userManager.FindByEmailAsync("mehmetdemir@akademi.com")).Id,
                Videos = new List<Video>
                {
                    new Video { Title = "Giriş", Url = "/files/coursevideo/2.mp4", Duration = 120, IndexInCourse = 1 },
                    new Video { Title = "Temel Kavramlar", Url = "/files/coursevideo/1.mp4", Duration = 150, IndexInCourse = 2 },
                    new Video { Title = "Dependency Injection", Url = "/files/coursevideo/3.mp4", Duration = 180, IndexInCourse = 3 }
                }
            },
            new Course
            {
                Title = "Python ile Programlamaya Giriş",
                Description = "Python programlama diline giriş yapacağınız bu kurs ile temel Python bilgilerini öğreneceksiniz.",
                Price = 150,
                CoverImage = "/files/courseimg/python.jpg",
                TeacherId = (await userManager.FindByEmailAsync("mehmetdemir@akademi.com")).Id,
                Videos = new List<Video>
                {
                    new Video { Title = "Python'a Giriş", Url = "/files/coursevideo/4.mp4", Duration = 120, IndexInCourse = 1 },
                    new Video { Title = "Sanal Ortam Kurulumu", Url = "/files/coursevideo/2.mp4", Duration = 150, IndexInCourse = 2 },
                    new Video { Title = "Python'da Kütüphaneler", Url = "/files/coursevideo/3.mp4", Duration = 180, IndexInCourse = 3 }
                }
            },
            new Course
            {
                Title = "React ile Web Geliştirme",
                Description = "React ile web geliştirme yapacağınız bu kurs ile modern web geliştirme tekniklerini öğreneceksiniz.",
                Price = 200,
                CoverImage = "/files/courseimg/react.webp",
                TeacherId = (await userManager.FindByEmailAsync("ayseyilmaz@akademi.com")).Id,
                Videos = new List<Video>
                {
                    new Video { Title = "React'a Giriş", Url = "/files/coursevideo/3.mp4", Duration = 120, IndexInCourse = 1 },
                    new Video { Title = "React'ta Component Kavramı", Url = "/files/coursevideo/4.mp4", Duration = 150, IndexInCourse = 2 },
                    new Video { Title = "React'ta State ve Props", Url = "/files/coursevideo/1.mp4", Duration = 180, IndexInCourse = 3 }
                }
            },

            new Course
            {
                Title = "Linux Sistem Yönetimi",
                Description = "Linux sistem yönetimi yapacağınız bu kurs ile Linux işletim sistemini yönetmeyi öğreneceksiniz.",
                Price = 250,
                CoverImage = "/files/courseimg/linux.jpg",
                  TeacherId = (await userManager.FindByEmailAsync("ayseyilmaz@akademi.com")).Id,
                Videos = new List<Video>
                {
                    new Video { Title = "Linux'a Giriş", Url = "/files/coursevideo/1.mp4", Duration = 120, IndexInCourse = 1 },
                    new Video { Title = "Temel Komutlar", Url = "/files/coursevideo/3.mp4", Duration = 150, IndexInCourse = 2 },
                    new Video { Title = "Linux Güvenliği", Url = "/files/coursevideo/4.mp4", Duration = 180, IndexInCourse = 3 }
                }
            }
        );
        await context.SaveChangesAsync();
    }

    if (!context.Orders.Any())
    {
        var user = await userManager.FindByEmailAsync("elifkaya@akademi.com");
     
        var user2 = await userManager.FindByEmailAsync("alicelik@akademi.com");
        var course = context.Courses.First();
        var course2 = context.Courses.Skip(1).First();

    
         context.Orders.AddRange(
            new Order
        {
            UserId = user.Id,
            CourseId = course.Id,
        OrderDate = DateTime.UtcNow,
            Price = course.Price,
            PaymentStatus = "Tamamlandı"
        },
        new Order
        {
            UserId = user2.Id,
            CourseId = course2.Id,
        OrderDate = DateTime.UtcNow,
            Price = course2.Price,
            PaymentStatus = "Tamamlandı"
        }
        );
        await context.SaveChangesAsync();
        
            
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error during seeding: {ex.Message}");
}

app.Run();