using FluentResults;
using MediatR;
using ToDoApp.BLL.DTO;

namespace ToDoApp.BLL.MediatR.UserTask.Create;

public record CreateUserTaskCommand(CreateUserTaskDto CreateUserTaskDto) : IRequest<Result<UserTaskFullDto>>;
