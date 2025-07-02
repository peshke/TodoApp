using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Application.Interfaces;
using TodoApp.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;


namespace TodoApp.Infrastructure.Workers;

public class OverdueTaskCleanupWorker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<OverdueTaskCleanupWorker> _logger;

    public OverdueTaskCleanupWorker(IServiceProvider serviceProvider, ILogger<OverdueTaskCleanupWorker> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("⏳ OverdueTaskCleanupWorker started");

        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _serviceProvider.CreateScope();
            var repo = scope.ServiceProvider.GetRequiredService<ITaskRepository>();

            var allTasks = await repo.GetAllAsync();
            var overdue = allTasks
                .Where(t => !t.IsCompleted && t.DueDate < DateTime.Now)
                .ToList();

            foreach (var task in overdue)
            {
                _logger.LogInformation($"🗑 Deleting overdue task: {task.Title}");
                await repo.DeleteAsync(task.Id);
            }

            await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken); // check every 30s
        }
    }
}
