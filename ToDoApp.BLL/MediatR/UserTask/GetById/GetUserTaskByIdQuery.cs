using FluentResults;
using MediatR;
using ToDoApp.BLL.DTO;

namespace ToDoApp.BLL.MediatR.UserTask.GetById;

public record GetUserTaskByIdQuery(int Id) :  IRequest<Result<UserTaskFullDto>>;