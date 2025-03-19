using FluentValidation;

namespace Products.Application.UseCases.CreateProduct;

public class CreateProductValidator: AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name is required.")
            .MaximumLength(255).WithMessage("Product name cannot exceed 255 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Product description is required.")
            .MaximumLength(1000).WithMessage("Product description cannot exceed 1000 characters.");

        RuleFor(x => x.Price)
            .NotEmpty().WithMessage("Product price is required.")
            .GreaterThan(0).WithMessage("Product price must be greater than 0.");

        RuleFor(x => x.Stock)
            .NotEmpty().WithMessage("Product stock is required.")
            .GreaterThanOrEqualTo(0).WithMessage("Product stock cannot be negative.");

        RuleFor(x => x.Sku)
            .NotEmpty().WithMessage("Product SKU is required.")
            .MaximumLength(50).WithMessage("Product SKU cannot exceed 50 characters.")
            .Matches("^[a-zA-Z0-9\\-]+$").WithMessage("Product SKU can only contain letters, numbers, and hyphens.");

        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("Category ID is required.");
    }
}