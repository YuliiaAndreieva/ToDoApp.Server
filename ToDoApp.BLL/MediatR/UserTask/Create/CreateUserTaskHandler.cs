using AutoMapper;
using FluentResults;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using ToDoApp.BLL.DTO;
using ToDoApp.DAL.Repositories;

namespace ToDoApp.BLL.MediatR.UserTask.Create;

public class CreateUserTaskHandler : IRequestHandler<CreateUserTaskCommand, Result<UserTaskFullDto>>
{
    private readonly IUserTaskRepository _userTaskRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateUserTaskHandler> _logger;
    private readonly IValidator<CreateUserTaskCommand> _taskValidator;

    public CreateUserTaskHandler(
        IUserTaskRepository userTaskRepository,
        IMapper mapper,
        ILogger<CreateUserTaskHandler> logger,
        IValidator<CreateUserTaskCommand> taskValidator)
    {
        _userTaskRepository = userTaskRepository;
        _mapper = mapper;
        _logger = logger;
        _taskValidator = taskValidator;
    }

    public async Task<Result<UserTaskFullDto>> Handle(CreateUserTaskCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Start handling CreateUserTaskCommand for Task: {@Request}", request);
            
            var validationResult = await _taskValidator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                _logger.LogWarning("Validation failed for CreateUserTaskCommand: {Errors}", string.Join("; ", errors));
                return Result.Fail(string.Join("; ", errors));
            }
            
            _logger.LogInformation("Mapping CreateUserTaskCommand to UserTask entity");
            var task = _mapper.Map<DAL.Entities.UserTask>(request.CreateUserTaskDto);
            
            await _userTaskRepository.Create(task);
            _logger.LogInformation("Task created successfully in the database. Task ID: {TaskId}", task.Id);
            
            var taskFullDto = _mapper.Map<UserTaskFullDto>(task);
            _logger.LogInformation("Task successfully mapped to UserTaskFullDto");

            return Result.Ok(taskFullDto);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error occurred while handling CreateUserTaskCommand. Exception: {Message}, Command: {@Request}", ex.Message, request);
            return Result.Fail(ex.Message);
        }
    }
}
