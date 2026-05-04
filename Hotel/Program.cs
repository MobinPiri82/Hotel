using Hotel.Data;
using Microsoft.EntityFrameworkCore;
using Hotel.Services;
using Hotel.Contract;

var builder = WebApplication.CreateBuilder(args);

// دریافت ConnectionString
var connectionstring = builder.Configuration.GetConnectionString("HotelDbConnectionString");

// ثبت DbContext
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(connectionstring)
);

// ثبت سرویس‌های سفارشی (قبل از Build)
builder.Services.AddScoped<ICountryInterface, CountryServices>();
builder.Services.AddScoped<IHotelServices, HotelServices>();

// Add services to the container
builder.Services.AddControllers().AddJsonOptions(opt =>
    opt.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve
);

// افزودن Swagger و Endpoints
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseAuthorization();
app.MapControllers();
app.Run();