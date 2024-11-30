using FluentValidation;
using ToDoApp.BLL.MediatR.UserTask.Create;

namespace ToDoApp.BLL.Validation;

public class CreateUserTaskValidator : AbstractValidator<CreateUserTaskCommand>
{
    public CreateUserTaskValidator(BaseUserTaskValidator baseUserTaskValidator)
    {
        RuleFor(c => c.CreateUserTaskDto).SetValidator(baseUserTaskValidator);
    }
}
