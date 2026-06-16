using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using testwebapp.Entites;
using testwebapp.Models.Profiles;
using testwebapp.Repositories;
using testwebapp.Repositories.Interfaces;
using testwebapp.Services;
using testwebapp.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// test

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// add EF context
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("testdb")));

// profiles
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<ToDoProfile>());

// add repositories and services
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

builder.Services.AddScoped<ITaskService, TaskService>();

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
