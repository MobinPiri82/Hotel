using Hotel.Data;
using Microsoft.EntityFrameworkCore;
using Hotel.Services;
using Hotel.Contract;
using Hotel.MappingProfiles;

var builder = WebApplication.CreateBuilder(args);


var connectionstring = builder.Configuration.GetConnectionString("HotelDbConnectionString");


builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(connectionstring)
);

builder.Services.AddScoped<ICountryInterface, CountryServices>();
builder.Services.AddScoped<IHotelServices, HotelServices>();
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<HotelMappingProfile>();
    cfg.AddProfile<CountryMappingProfile>();
});


builder.Services.AddControllers().AddJsonOptions(opt =>
    opt.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve
);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.MapOpenApi();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();