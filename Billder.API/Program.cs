using Billder.Application.Interfaces;
using Billder.Application.Repository;
using Billder.Application.Repository.Interfaces;
using Billder.Application.Services;
using Billder.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using AspNetCoreRateLimit;
using DotNetEnv;

Env.Load();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ITrabajoRepository, TrabajoRepository>();
builder.Services.AddScoped<ITrabajoInterface, TrabajoService>();
builder.Services.AddScoped<TrabajoService>();

builder.Services.AddScoped<IPresupuestoRepository, PresupuestoRepository>();
builder.Services.AddScoped<IPresupuestoService, PresupuestoService>();

builder.Services.AddScoped<IURegistradoRepository, URegistradoRepository>();
builder.Services.AddScoped<IURegistradoInterface, UsuarioRegistradoService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



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

app.UseAuthorization();

app.MapControllers();

app.Run();
