using System.Diagnostics;
using Api.ApplicationLogic.Repositories;
using Api.ApplicationLogic.Services;
using Api.Core;
using Api.Infrastructure;
using Api.Infrastructure.Interface;
using Api.Infrastructure.IService;
using Api.Infrastructure.Mapper;
using Api.Presentation.Middlewares;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration.Get<AppConfiguration>() ?? new AppConfiguration();
builder.Services.AddSingleton(configuration);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(o =>
    o.UseNpgsql(configuration.ConnectionStrings.DatabaseConnection)
);
// register services
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();

builder.Services.AddScoped<IUserReadService, UserReadService>();
// builder.Services.AddScoped<IUserWriteService, UserWriteService>();
builder.Services.AddScoped<IBookReadService, BookReadService>();
builder.Services.AddScoped<IBookWriteService, BookWriteService>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddAutoMapper(typeof(MapProfile));

// Middleware
builder.Services.AddSingleton<GlobalExceptionMiddleware>();
builder.Services.AddSingleton<PerformanceMiddleware>();
builder.Services.AddSingleton<Stopwatch>();
builder.Services.AddHealthChecks();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapHealthChecks("/hc");

app.UseAuthorization();

app.MapControllers();

app.Run();
