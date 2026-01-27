using SmartParking;
using SmartParking.Core;
using SmartParking.Core.Repositories;
using SmartParking.Core.Services;
using SmartParking.Data.Repoistories;
using SmartParking.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddSingleton<IDataContext, DataContext>(); //אותו מופע עבור כל ההרצה
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<ICarRepository, CarsRepository>();


builder.Services.AddAutoMapper(typeof(MappingProfile));

//builder.Services.AddScoped<IParkingService, ParkingService>();
//builder.Services.AddScoped<IParkingRepository, ParkingsRepository>();
//builder.Services.AddScoped<ISpotService, SpotService>();
//builder.Services.AddScoped<ISpotRepository, SpotsRepository>();

builder.Services.AddDbContext<DataContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
