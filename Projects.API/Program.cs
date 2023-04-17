using AutoMapper.Data;
using Projects.API.AutoMapper;
using Projects.API.Middlewares;
using Projects.Business.Gateway;
using Projects.Business.Gateway.Repositories;
using Projects.Business.UseCases;
using Projects.Infrastructure;
using Projects.Infrastructure.Gateway;
using Projects.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

//services cors
builder.Services.AddCors(p => p.AddPolicy("devcors", builder =>
{
    builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
    builder.WithOrigins("https://tracking-project-manager-4414f.firebaseapp.com").AllowAnyMethod().AllowAnyHeader();
    builder.WithOrigins("https://tracking-project-manager-4414f.web.app").AllowAnyMethod().AllowAnyHeader();
    builder.WithOrigins("https://tracking-project-manage.firebaseapp.com").AllowAnyMethod().AllowAnyHeader();
    builder.WithOrigins("https://tracking-project-manage.web.app").AllowAnyMethod().AllowAnyHeader();
}));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(config => config.AddDataReaderMapping(), typeof(ConfigurationProfile));

builder.Services.AddScoped<IProjectUseCase, ProjectUseCase>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();

builder.Services.AddScoped<ITaskUseCase, TaskUseCase>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

builder.Services.AddScoped<IInscriptionUseCase, InscriptionUseCase>();
builder.Services.AddScoped<IInscriptionRepository, InscriptionRepository>();

builder.Services.AddScoped<ICommentUseCase, CommentUseCase>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

builder.Services.AddTransient<IDbConnectionBuilder>(e =>
{
    return new DbConnectionBuilder(builder.Configuration.GetConnectionString("urlConnectionSQL"));
});

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