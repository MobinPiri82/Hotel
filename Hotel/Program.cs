using Hotel.Data;
using Microsoft.EntityFrameworkCore;
using Hotel.Services;
using Hotel.Contract;
//using Microsoft.EntityFrameworkCore.Tool;
using static System.Runtime.InteropServices.JavaScript.JSType;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(opt => 
opt.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve);
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services();
builder.Services.AddScoped<IHotelServices, HotelServices>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    //app.UseSwagger();
    //app.UseSwaggerUI() ;
  
}

var connectionstring = builder.Configuration.GetConnectionString("HotelDbConnectionString");
builder.Services.AddDbContext<Hotelcontext>(options => options.UseSqlServer(connectionstring));

app.UseAuthorization();

app.MapControllers();

app.Run();
