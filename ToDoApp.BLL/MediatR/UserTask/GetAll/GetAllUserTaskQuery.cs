using FluentResults;
using MediatR;
using ToDoApp.BLL.DTO;

namespace ToDoApp.BLL.MediatR.UserTask.GetAll;

public record GetAllUserTaskQuery() : IRequest<Result<IEnumerable<UserTaskFullDto>>>;