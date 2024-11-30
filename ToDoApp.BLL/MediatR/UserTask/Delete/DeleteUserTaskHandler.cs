using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using ToDoApp.DAL.Repositories;

namespace ToDoApp.BLL.MediatR.UserTask.Delete;

public class DeleteUserTaskHandler : IRequestHandler<DeleteUserTaskCommand, Result>
{
    private readonly IUserTaskRepository _userTaskRepository;
    private readonly ILogger<DeleteUserTaskHandler> _logger;

    public DeleteUserTaskHandler(
        IUserTaskRepository userTaskRepository,
        ILogger<DeleteUserTaskHandler> logger)
    {
        _userTaskRepository = userTaskRepository;
        _logger = logger;
    }

    public async Task<Result> Handle(DeleteUserTaskCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Start handling DeleteUserTaskCommand for Task ID: {TaskId}", request.Id);
            
            var task = await _userTaskRepository.GetById(request.Id);
            if (task is null)
            {
                _logger.LogWarning("Task with ID: {TaskId} not found", request.Id);
                return Result.Fail("Task not found");
            }
            
            await _userTaskRepository.Delete(task);
            _logger.LogInformation("Task with ID: {TaskId} successfully deleted", request.Id);

            return Result.Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError("Error occurred while handling DeleteUserTaskCommand. Exception: {Message}, Command: {@Request}", ex.Message, request);
            return Result.Fail(ex.Message);
        }
    }
}
