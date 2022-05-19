using FluentValidation;

namespace DapperTest.Models.Validator
{
    public class TodoItemsValidator : AbstractValidator<TodoItems>
    {
        public TodoItemsValidator()
        {
            RuleFor(c => c.Title).MinimumLength(2).WithMessage("Min 2 karakter olmalı");
            RuleFor(c => c.Description).MinimumLength(2).WithMessage("Min 2 karakter olmalı");
        }
    }
}
