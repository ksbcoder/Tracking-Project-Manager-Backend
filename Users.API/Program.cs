using AutoMapper.Data;
using Users.API.AutoMapper;
using Users.API.Middlewares;
using Users.Business.Gateway;
using Users.Business.Gateway.Repositories.Commands;
using Users.Business.Gateway.Repositories.Queries;
using Users.Business.UseCases.Commands;
using Users.Business.UseCases.Queries;
using Users.Infrastructure;
using Users.Infrastructure.Interfaces;
using Users.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
//services cors
builder.Services.AddCors(p => p.AddPolicy("devcors", builder =>
{
    builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
    builder.WithOrigins("https://tracking-project-manager-4414f.firebaseapp.com").AllowAnyMethod().AllowAnyHeader();
    builder.WithOrigins("https://tracking-project-manager-4414f.web.app").AllowAnyMethod().AllowAnyHeader();
}));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(config => config.AddDataReaderMapping(), typeof(ConfigurationProfile));

builder.Services.AddScoped<IUserQueryUseCase, UserQueryUseCase>();
builder.Services.AddScoped<IUserCommandUseCase, UserCommandUseCase>();
builder.Services.AddScoped<IUserQueryRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandRepository, UserRepository>();

builder.Services.AddSingleton<IContext>(provider => new Context(builder.Configuration.GetConnectionString("urlConnectionMongo"), "TrackingProjectManager"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("devcors");

app.UseAuthorization();

app.UseMiddleware<ErrorHandleMiddleware>();

app.MapControllers();

app.Run();
