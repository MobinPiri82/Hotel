using Hotel.Data;
using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Tool;
using static System.Runtime.InteropServices.JavaScript.JSType;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//}

var connectionstring = builder.Configuration.GetConnectionString("HotelDbConnectionString");
builder.Services.AddDbContext<Hotelcontext>(options => options.UseSqlServer(connectionstring));

app.UseAuthorization();

app.MapControllers();

app.Run();
