using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Queries.GetTasks;

public record GetTasksQuery() : IRequest<List<TaskItem>>;
