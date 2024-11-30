using FluentValidation;
using ToDoApp.BLL.MediatR.UserTask.Update;

namespace ToDoApp.BLL.Validation;

public class UpdateUserTaskValidator : AbstractValidator<UpdateUserTaskCommand>
{
    public UpdateUserTaskValidator(BaseUserTaskValidator baseUserTaskValidator)
    {
        RuleFor(c => c.UpdateUserTaskDto).SetValidator(baseUserTaskValidator);
    }
}