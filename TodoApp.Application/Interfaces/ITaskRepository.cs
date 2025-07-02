using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TodoApp.Domain.Entities;

namespace TodoApp.Application.Interfaces;

public interface ITaskRepository
{
    Task AddAsync(TaskItem task);
    Task<List<TaskItem>> GetAllAsync();
    Task<TaskItem?> GetByIdAsync(Guid id);
    Task UpdateAsync(TaskItem task);
    Task DeleteAsync(Guid id);


}
