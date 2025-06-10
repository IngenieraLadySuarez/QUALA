using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Quala.Sucursales.Api.Auth;
using Quala.Sucursales.Api.Data;
using Quala.Sucursales.Api.Repositories;
using Quala.Sucursales.Api.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<DapperContext>();

// Registrar el repositorio y servicios
builder.Services.AddScoped<ISucursalRepository, SucursalRepository>();
builder.Services.AddScoped<IMonedaRepository, MonedaRepository>();

builder.Services.AddScoped<SucursalService>();
builder.Services.AddScoped<MonedaService>();
builder.Services.AddScoped<JwtService>();

// Configuración de CORS para permitir Angular localhost:4200
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDevClient",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Configuración de Swagger con soporte JWT
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Quala",
        Version = "v1",
        Description = "API para la gestión de sucursales"
    });

    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Introduce el token JWT como: Bearer {tu token}",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
});

var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();

if (jwtSettings == null ||
    string.IsNullOrWhiteSpace(jwtSettings.Secret) ||
    string.IsNullOrWhiteSpace(jwtSettings.Issuer) ||
    string.IsNullOrWhiteSpace(jwtSettings.Audience))
{
    throw new Exception("JwtSettings no está configurado correctamente en appsettings.json");
}

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
    };
});

builder.Services.AddControllers();

var app = builder.Build();

// Middleware de Swagger
app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<Quala.Sucursales.Api.Middleware.ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

// Usa CORS antes de Authentication y Authorization
app.UseCors("AllowAngularDevClient");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
