using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using ToDoApp.BLL.DTO;
using ToDoApp.DAL.Repositories;

namespace ToDoApp.BLL.MediatR.UserTask.GetAll;

public class GetAllUserTaskHandler : IRequestHandler<GetAllUserTaskQuery, Result<IEnumerable<UserTaskFullDto>>>
{
    private readonly IUserTaskRepository _userTaskRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllUserTaskHandler> _logger;

    public GetAllUserTaskHandler(
        IUserTaskRepository userTaskRepository,
        IMapper mapper,
        ILogger<GetAllUserTaskHandler> logger)
    {
        _userTaskRepository = userTaskRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<UserTaskFullDto>>> Handle(
        GetAllUserTaskQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Start handling GetAllUserTaskQuery");
            
            var tasks = await _userTaskRepository.GetAll();
            if (!tasks.Any())
            {
                _logger.LogWarning("No tasks found in the database");
                return Result.Ok(Enumerable.Empty<UserTaskFullDto>());
            }
            
            _logger.LogInformation("Mapping tasks to UserTaskFullDto");
            var tasksDto = _mapper.Map<IEnumerable<UserTaskFullDto>>(tasks);

            _logger.LogInformation("Successfully retrieved {TaskCount} tasks", tasksDto.Count());
            return Result.Ok(tasksDto);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error occurred while handling GetAllUserTaskQuery. Exception: {Message}, Query: {@Request}", ex.Message, request);
            return Result.Fail(ex.Message);
        }
    }
}