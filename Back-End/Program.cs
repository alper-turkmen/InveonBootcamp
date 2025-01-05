using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();

app.UseStaticFiles();


app.MapControllers();

app.Run(); 