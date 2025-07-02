using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TodoApp.Application.Interfaces;
using TodoApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace TodoApp.Infrastructure.Persistence;

public class TaskRepository : ITaskRepository
{
    private readonly TaskDbContext _context;

    public TaskRepository(TaskDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(TaskItem task)
    {
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
    }
    public async Task<List<TaskItem>> GetAllAsync()
    {
        return await _context.Tasks.ToListAsync();
    }
    public async Task<TaskItem?> GetByIdAsync(Guid id)
    {
        return await _context.Tasks.FindAsync(id);
    }

    public async Task UpdateAsync(TaskItem task)
    {
        _context.Tasks.Update(task);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(Guid id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task is not null)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }
    }


}
