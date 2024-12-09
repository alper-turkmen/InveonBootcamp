using Bolum3.Repositories;
using Bolum3.Services;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Kutuphane Yonetim Sistemi Api",
        Version = "v1",
        Description = "Bu api kutuphane yonetim sistemi için gerekli olan bazi CRUD islemlerini gerceklestirir. (Redis Onbellegi Kullanir,  Redis calismiyorsa cokmemesi icin kontrol mevcuttur. ProblemDetails ve ServiceResult formatinda sonuc dondurur. GlobalExceptionHandler mevcuttur.)",
    });
});


builder.Services.AddScoped<IBookRepository, BookRepository>();

try
{
    var redis = ConnectionMultiplexer.Connect("localhost:6379");
    builder.Services.AddStackExchangeRedisCache(options =>
    {
        options.Configuration = "localhost:6379";
    });

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";
    options.InstanceName = "instance1";
    options.ConfigurationOptions = new StackExchange.Redis.ConfigurationOptions
    {
        EndPoints = { "localhost:6379" },
        AbortOnConnectFail = false,
        ConnectTimeout = 1000, 
    };
});

    builder.Services.AddScoped<IBookService, BookService>();
}
catch
{
      builder.Services.AddScoped<IBookService, BookServiceNoRedis>();
}


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<Bolum3.Middleware.ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

