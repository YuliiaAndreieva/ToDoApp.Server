using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using ToDoApp.BLL.DTO;
using ToDoApp.DAL.Repositories;

namespace ToDoApp.BLL.MediatR.UserTask.GetById;

public class GetUserTaskByIdHandler : IRequestHandler<GetUserTaskByIdQuery, Result<UserTaskFullDto>>
{
    private readonly IUserTaskRepository _userTaskRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetUserTaskByIdHandler> _logger;

    public GetUserTaskByIdHandler(
        IUserTaskRepository userTaskRepository,
        IMapper mapper,
        ILogger<GetUserTaskByIdHandler> logger)
    {
        _userTaskRepository = userTaskRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<UserTaskFullDto>> Handle(GetUserTaskByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Start handling GetUserTaskByIdQuery for Task ID: {TaskId}", request.Id);
            var task = await _userTaskRepository.GetById(request.Id);
            if (task is null)
            {
                _logger.LogWarning("Task with ID: {TaskId} not found", request.Id);
                return Result.Fail("Task not found");
            }
            
            _logger.LogInformation("Mapping task with ID: {TaskId} to UserTaskFullDto", request.Id);
            var taskDto = _mapper.Map<UserTaskFullDto>(task);

            _logger.LogInformation("Successfully retrieved task with ID: {TaskId}", request.Id);
            return Result.Ok(taskDto);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error occurred while handling GetUserTaskByIdQuery. Exception: {Message}, Query: {@Request}", ex.Message, request);
            return Result.Fail(ex.Message);
        }
    }

}
