using AutoMapper;
using FluentResults;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using ToDoApp.BLL.DTO;
using ToDoApp.DAL.Repositories;

namespace ToDoApp.BLL.MediatR.UserTask.Update;

public class UpdateUserTaskHandler : IRequestHandler<UpdateUserTaskCommand, Result<UserTaskFullDto>>
{
    private readonly IUserTaskRepository _userTaskRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<UpdateUserTaskCommand> _taskValidator;
    private readonly ILogger<UpdateUserTaskHandler> _logger;

    public UpdateUserTaskHandler(
        IUserTaskRepository userTaskRepository,
        IMapper mapper,
        ILogger<UpdateUserTaskHandler> logger,
        IValidator<UpdateUserTaskCommand> taskValidator)
    {
        _userTaskRepository = userTaskRepository;
        _mapper = mapper;
        _logger = logger;
        _taskValidator = taskValidator;
    }

    public async Task<Result<UserTaskFullDto>> Handle(UpdateUserTaskCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Start handling UpdateUserTaskCommand for Task ID: {TaskId}", request.UpdateUserTaskDto.Id);
            
            var task = await _userTaskRepository.GetById(request.UpdateUserTaskDto.Id);
            if (task is null)
            {
                _logger.LogWarning("Task with ID: {TaskId} not found", request.UpdateUserTaskDto.Id);
                return Result.Fail("Task not found");
            }
            
            _logger.LogInformation("Validating UpdateUserTaskCommand for Task ID: {TaskId}", request.UpdateUserTaskDto.Id);
            var validationResult = await _taskValidator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                _logger.LogWarning("Validation failed for Task ID: {TaskId}. Errors: {Errors}", request.UpdateUserTaskDto.Id, string.Join("; ", errors));
                return Result.Fail(string.Join("; ", errors));
            }
            
            _logger.LogInformation("Updating task with ID: {TaskId}", request.UpdateUserTaskDto.Id);
            _mapper.Map(request.UpdateUserTaskDto, task);
            await _userTaskRepository.Update(task);
            
            _logger.LogInformation("Mapping updated task with ID: {TaskId} to UserTaskFullDto", request.UpdateUserTaskDto.Id);
            var taskFullDto = _mapper.Map<UserTaskFullDto>(task);

            _logger.LogInformation("Successfully updated task with ID: {TaskId}", request.UpdateUserTaskDto.Id);
            return Result.Ok(taskFullDto);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error occurred while handling UpdateUserTaskCommand. Exception: {Message}, Command: {@Request}", ex.Message, request);
            return Result.Fail(ex.Message);
        }
    }
}
