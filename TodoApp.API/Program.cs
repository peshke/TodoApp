using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoApp.Application.Commands.CreateTask;
using TodoApp.Application.Interfaces;
using TodoApp.Infrastructure.Persistence;
using TodoApp.Infrastructure.Workers;




var builder = WebApplication.CreateBuilder(args);


builder.Services.AddMediatR(cfg => {cfg.RegisterServicesFromAssembly(typeof(CreateTaskCommandHandler).Assembly);});


builder.Services.AddValidatorsFromAssemblyContaining<CreateTaskCommandValidator>();


builder.Services.AddDbContext<TaskDbContext>(options =>
    options.UseInMemoryDatabase("TaskDb"));
builder.Services.AddHostedService<OverdueTaskCleanupWorker>();

builder.Services.AddScoped<ITaskRepository, TaskRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
