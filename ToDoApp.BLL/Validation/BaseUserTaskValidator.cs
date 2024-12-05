using FluentValidation;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using ToDoApp.BLL.DTO;

namespace ToDoApp.BLL.Validation;

public class BaseUserTaskValidator : AbstractValidator<BaseUserTaskDto>
{
    private readonly ILogger<BaseUserTaskValidator> _logger;
    public BaseUserTaskValidator(
        ILogger<BaseUserTaskValidator> logger)
    {
        _logger = logger;
        RuleFor(c => c.Name)
            .NotNull()
            .MinimumLength(3)
            .WithMessage("The name must be longer")
            .MaximumLength(20)
            .WithMessage("The name must be shorter ");
        
        RuleFor(c => c.DueDate)
            .NotNull()
            .WithMessage("Due date is required.")
            .Must(BeWithinCurrentWeek)
            .WithMessage("The due date must be within the current week.");
        
        When(ut => !ut.Description.IsNullOrEmpty(), () =>
        {
            RuleFor(us => us.Description)
                .MinimumLength(3)
                .WithMessage("The description must be longer")
                .MaximumLength(50)
                .WithMessage("The description must be shorter ");;
        });
        
        RuleFor(c => c.Status)
            .NotNull()
            .Must(status => new[] { "Planned", "InProgress", "Done" }.Contains(status))
            .WithMessage("Task status must be one of the following: Planned, InProgress, Done.");
    }
    private bool BeWithinCurrentWeek(DateTime dueDate)
    {
        var today = DateTime.Now.Date;
        var startOfWeek = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);
        var endOfWeek = startOfWeek.AddDays(7);
        _logger.LogInformation($"START DAY:{startOfWeek.Date}, END DAY: {endOfWeek.Date}");
        return dueDate >= startOfWeek.Date && dueDate <= endOfWeek.Date;
    }
}