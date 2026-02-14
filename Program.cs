using Microsoft.EntityFrameworkCore;
using RoomBooking.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlite("Data Source=roombooking.db"));

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact", policy =>
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyMethod()
              .AllowAnyHeader());
});

var app = builder.Build();

app.UseCors("AllowReact");

app.UseAuthorization();
app.MapControllers();

app.Run();