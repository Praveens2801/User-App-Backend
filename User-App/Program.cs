using Microsoft.EntityFrameworkCore;
using User_App.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<UserappContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

// ✅ Add default GET endpoint for the root URL
app.MapGet("/", () => "Welcome to the User-App API!");

app.Run();
