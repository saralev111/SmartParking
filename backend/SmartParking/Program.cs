using Microsoft.AspNetCore.Authentication.JwtBearer; // הוספנו
using Microsoft.IdentityModel.Tokens; // הוספנו
using Microsoft.OpenApi.Models; // הוספנו עבור סוואגר
using SmartParking;
using SmartParking.Core;
using SmartParking.Core.Repositories;
using SmartParking.Core.Services;
using SmartParking.Data.Repoistories;
using SmartParking.Service;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:8080")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// --- הגדרות JWT ---
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
// ------------------

// --- הגדרות Swagger עם מנעול ---
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.Http
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});
// -------------------------------

// הזרקות התלויות שלך (Services & Repositories) - נשאר אותו דבר
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<ICarRepository, CarsRepository>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IParkingService, ParkingService>();
builder.Services.AddScoped<IParkingRepository, ParkingsRepository>();
builder.Services.AddScoped<ISpotService, SpotService>();
builder.Services.AddScoped<ISpotRepository, SpotsRepository>();
builder.Services.AddDbContext<DataContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");


// --- חשוב מאוד: הסדר כאן קריטי! ---
app.UseAuthentication(); // קודם כל תזדהה (מי אתה?)
app.UseAuthorization();  // אחר כך נבדוק אם מותר לך (מה מותר לך?)
// -----------------------------------

app.MapControllers();

app.Run();