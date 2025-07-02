using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;
using TodoApp.Domain.Entities;
using TodoApp.Application.Interfaces;

namespace TodoApp.Application.Commands.CreateTask;

public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Guid>
{
    private readonly ITaskRepository _repository;

    public CreateTaskCommandHandler(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = new TaskItem
        {
            Title = request.Title,
            Description = request.Description,
            DueDate = request.DueDate
        };

        await _repository.AddAsync(task);
        return task.Id;
    }
}
