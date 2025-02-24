using FluentValidation;
using ToDoApp.DTOs.todos;

namespace ToDoApp.Validators.todos;

public class UpdateTodoDtoValidator: AbstractValidator<UpdateTodoDto>
{
    public UpdateTodoDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(100);
        RuleFor(x => x.Description)
            .MaximumLength(500)
            .When(t => t.Description != null);
    }
}