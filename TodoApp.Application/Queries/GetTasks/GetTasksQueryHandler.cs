using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;
using TodoApp.Application.Interfaces;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Queries.GetTasks;

public class GetTasksQueryHandler : IRequestHandler<GetTasksQuery, List<TaskItem>>
{
    private readonly ITaskRepository _repository;

    public GetTasksQueryHandler(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<TaskItem>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync();
    }
}
