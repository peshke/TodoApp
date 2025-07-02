using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

namespace TodoApp.Application.Commands.CompleteTask;

public record CompleteTaskCommand(Guid Id) : IRequest<bool>;
