using Microsoft.AspNetCore.Mvc;
using MediatR;
using TodoApp.Application.Commands.CreateTask;
using TodoApp.Application.Queries.GetTasks;
namespace TodoApp.API.Controllers;
using TodoApp.Application.Commands.CompleteTask;



[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly IMediator _mediator;

    public TasksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTaskCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        return Ok(new { Id = id });
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tasks = await _mediator.Send(new GetTasksQuery());
        return Ok(tasks);
    }
    [HttpPut("{id}/complete")]
    public async Task<IActionResult> Complete(Guid id)
    {
        var result = await _mediator.Send(new CompleteTaskCommand(id));

        if (!result)
            return NotFound();

        return NoContent(); // 204 success with no body
    }


}
