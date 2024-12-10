using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.BLL.DTO;
using ToDoApp.BLL.MediatR.UserTask.Create;
using ToDoApp.BLL.MediatR.UserTask.Delete;
using ToDoApp.BLL.MediatR.UserTask.GetAll;
using ToDoApp.BLL.MediatR.UserTask.GetById;
using ToDoApp.BLL.MediatR.UserTask.Update;
using ToDoApp.WebApi.Helpers;

namespace ToDoApp.WebApi.Controllers;

[Route("api/task")]
[ApiController]
public class UserTaskController(
    IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllTasks()
    {
        var result = await mediator.Send(new GetAllUserTaskQuery());
        var resources = result.Value.Select(task => task.WithLinks(task.Id));
        return Ok(resources.ToList());
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTaskById(int id)
    {
        var result = await mediator.Send(new GetUserTaskByIdQuery(id));
        if (!result.IsSuccess) 
            return NotFound(result.Errors);
        
        return Ok(result.Value.WithLinks(id));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateTask(CreateUserTaskDto createUserTaskDto)
    {
        var result = await mediator.Send(new CreateUserTaskCommand(createUserTaskDto));
        return result.IsSuccess ? CreatedAtAction(nameof(CreateTask), new { id = result.Value.Id }, result.Value) : BadRequest(result.Errors);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateTask([FromBody] UpdateUserTaskDto updateUserTaskDto)
    {
        var result = await mediator.Send(new UpdateUserTaskCommand(updateUserTaskDto));
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Errors);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var result = await mediator.Send(new DeleteUserTaskCommand(id));
        return result.IsSuccess ? NoContent() : NotFound(result.Errors);
    }
}