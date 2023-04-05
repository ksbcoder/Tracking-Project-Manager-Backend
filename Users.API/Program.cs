using AutoMapper.Data;
using Users.API.AutoMapper;
using Users.API.Middlewares;
using Users.Business.Gateway;
using Users.Business.Gateway.Repositories;
using Users.Business.UseCases;
using Users.Infrastructure;
using Users.Infrastructure.Interfaces;
using Users.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(config => config.AddDataReaderMapping(), typeof(ConfigurationProfile));

builder.Services.AddScoped<IUserUseCase, UserUseCase>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddSingleton<IContext>(provider => new Context(builder.Configuration.GetConnectionString("urlConnection"), "TrackingProjectManager"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ErrorHandleMiddleware>();

app.MapControllers();

app.Run();
