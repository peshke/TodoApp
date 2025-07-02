using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;
using TodoApp.Application.Interfaces;

namespace TodoApp.Application.Commands.CompleteTask;

public class CompleteTaskCommandHandler : IRequestHandler<CompleteTaskCommand, bool>
{
    private readonly ITaskRepository _repository;

    public CompleteTaskCommandHandler(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(CompleteTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _repository.GetByIdAsync(request.Id);

        if (task is null)
            return false;

        task.Complete();
        await _repository.UpdateAsync(task);
        return true;
    }
}
