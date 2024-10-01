using Billder.Application.Interfaces;
using Billder.Application.Repository;
using Billder.Application.Repository.Interfaces;
using Billder.Application.Services;
using Billder.Infrastructure.Data;
using Billder.Application.Custom;
using Microsoft.EntityFrameworkCore;
using AspNetCoreRateLimit;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

Env.Load();
var builder = WebApplication.CreateBuilder(args);

//manejador de excepciones centralizado
builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ExceptionHandler)); 
});
// Add services to the container.
builder.Services.AddScoped<ITrabajoRepository, TrabajoRepository>();
builder.Services.AddScoped<ITrabajoInterface, TrabajoService>();
builder.Services.AddScoped<TrabajoService>();

builder.Services.AddScoped<IPresupuestoRepository, PresupuestoRepository>();
builder.Services.AddScoped<IPresupuestoService, PresupuestoService>();

builder.Services.AddScoped<IURegistradoRepository, URegistradoRepository>();
builder.Services.AddScoped<IURegistradoInterface, UsuarioRegistradoService>();

builder.Services.AddScoped<IMaterialRepository, MaterialRepository>();
builder.Services.AddScoped<IMaterialService, MaterialService>();

builder.Services.AddScoped<IEmpleadoRepository, EmpleadoRepository>();
builder.Services.AddScoped<IEmpleadoService, EmpleadoService>();

builder.Services.AddScoped<IPresupuestoEmpleadoRepository, PresupuestoEmpleadoRepository>();
builder.Services.AddScoped<IPresupuestoEmpleadoService, PresupuestoEmpleadoService>();

builder.Services.AddScoped<IPresupuestoMaterialRepository, PresupuestoMaterialRepository>();
builder.Services.AddScoped<IPresupuestoMaterialService, PresupuestoMaterialService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token in the format 'Bearer {token}'",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});




// in-memory cache is using to store rate limit counters
builder.Services.AddMemoryCache();
// carga la config de appsettings.json
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
// inject counter and rules stores
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddInMemoryRateLimiting();
// the clientId/clientIp resolvers use IHttpContextAccessor.
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var connectionString = Environment.GetEnvironmentVariable("LOCAL_DB_CONNECTION_STRING");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddScoped<TrabajoRepository>();
builder.Services.AddSingleton<Utilidades>();

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"]!))
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("NewPolicy", app =>
    {
        app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});
var app = builder.Build();

// habilita AspNetCoreRateLimit Middleware
app.UseIpRateLimiting();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("NewPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
