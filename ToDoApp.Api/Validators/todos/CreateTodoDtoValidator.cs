using FluentValidation;
using ToDoApp.DTOs.todos;

namespace ToDoApp.Validators.todos;

public class CreateTodoDtoValidator : AbstractValidator<CreateTodoDto>
{
    public CreateTodoDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(100);
        RuleFor(t => t.Description)
            .MaximumLength(500)
            .When(t => t.Description != null);
    }
}