using FluentResults;
using MediatR;
using ToDoApp.BLL.DTO;

namespace ToDoApp.BLL.MediatR.UserTask.Update;

public record UpdateUserTaskCommand(UpdateUserTaskDto UpdateUserTaskDto) : IRequest<Result<UserTaskFullDto>>;
