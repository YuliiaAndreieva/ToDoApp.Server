using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using ToDoApp.BLL.DTO;

namespace ToDoApp.BLL.Validation;

public class BaseUserTaskValidator : AbstractValidator<BaseUserTaskDto>
{
    public BaseUserTaskValidator()
    {
        RuleFor(c => c.Name)
            .NotNull()
            .MinimumLength(3)
            .WithMessage("The name must be longer")
            .MaximumLength(100)
            .WithMessage("The name must be shorter ");
        
        RuleFor(c => c.DueDate)
            .NotNull()
            .GreaterThanOrEqualTo(DateTime.Now.AddDays(-1))
            .WithMessage("The due date must be from yesterday or later.");
        
        When(ut => !ut.Description.IsNullOrEmpty(), () =>
        {
            RuleFor(us => us.Description)
                .MinimumLength(3)
                .WithMessage("The description must be longer")
                .MaximumLength(300)
                .WithMessage("The description must be shorter ");;
        });
    }
}