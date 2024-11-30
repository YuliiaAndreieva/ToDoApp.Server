using FluentResults;
using MediatR;

namespace ToDoApp.BLL.MediatR.UserTask.Delete;

public record DeleteUserTaskCommand(int Id) : IRequest<Result>;
