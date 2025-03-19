using FluentValidation;

namespace Products.Application.UseCases.CreateCategory;

public class CreateCategoryValidator: AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Category name is required.") 
            .MaximumLength(255).WithMessage("Category name cannot exceed 255 characters.");
    }
}