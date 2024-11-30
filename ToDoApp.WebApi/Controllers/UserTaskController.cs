using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.BLL.DTO;
using ToDoApp.BLL.MediatR.UserTask.Create;
using ToDoApp.BLL.MediatR.UserTask.Delete;
using ToDoApp.BLL.MediatR.UserTask.GetAll;
using ToDoApp.BLL.MediatR.UserTask.GetById;
using ToDoApp.BLL.MediatR.UserTask.Update;

namespace ToDoApp.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserTaskController(
    IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<UserTaskFullDto>> GetAllTasks()
    {
        var result = await mediator.Send(new GetAllUserTaskQuery());
        return result.Value;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTaskById(int id)
    {
        var result = await mediator.Send(new GetUserTaskByIdQuery(id));
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Errors);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask(CreateUserTaskDto createUserTaskDto)
    {
        var result = await mediator.Send(new CreateUserTaskCommand(createUserTaskDto));
        return result.IsSuccess ? CreatedAtAction(nameof(GetTaskById), new { id = result.Value.Id }, result.Value) : BadRequest(result.Errors);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTask([FromBody] UpdateUserTaskDto updateUserTaskDto)
    {
        var result = await mediator.Send(new UpdateUserTaskCommand(updateUserTaskDto));
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Errors);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var result = await mediator.Send(new DeleteUserTaskCommand(id));
        return result.IsSuccess ? NoContent() : NotFound(result.Errors);
    }
}