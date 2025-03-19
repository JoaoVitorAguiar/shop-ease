using MediatR;
using Products.Domain.Entities;
using Products.Domain.Exceptions;
using Products.Domain.Repositories;
using static System.Text.RegularExpressions.Regex;

namespace Products.Application.UseCases.CreateCategory;

public class CreateCategoryHandler(ICategoryRepository categoryRepository): IRequestHandler<CreateCategoryCommand, Unit>
{
    public async Task<Unit> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var categoryWithSameName = await categoryRepository.GetByNameAsync(request.Name);
        
        if (categoryWithSameName is not null)
            throw new CategoryWithNameAlreadyExistsException(request.Name);
        
        var slug = GenerateSlug(request.Name);
        var category = new Category(request.Name, slug);
        await categoryRepository.AddAsync(category);
        return Unit.Value;
    }
    
    private string GenerateSlug(string name)
    {
        var slug = name.ToLowerInvariant();
        
        slug = Replace(slug, @"[^a-z0-9\s-]", ""); 
        slug = Replace(slug, @"\s+", " ").Trim(); 
        slug = slug.Replace(" ", "-"); 

        return slug;
    }
}