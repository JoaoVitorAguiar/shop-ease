using MediatR;
using Products.Domain.Entities;
using Products.Domain.Exceptions;
using Products.Domain.Repositories;

namespace Products.Application.UseCases.CreateProduct;

public class CreateProductHandler(
    IProductRepository productRepository,
    ICategoryRepository categoryRepository): IRequestHandler<CreateProductCommand, Unit>
{
    public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var productWithSameSku = await productRepository.GetBySkuAsync(request.Sku);
        var productWithSameName = await productRepository.GetByNameAsync(request.Name);
        
        if (productWithSameSku is not null)
            throw new ProductWithSkuAlreadyExistsException(request.Sku);
        if (productWithSameName is not null)
            throw new ProductWithNameAlreadyExistsException(request.Name);
        
        var category = await categoryRepository.GetByIdAsync(request.CategoryId);
        if (category is null) throw new CategoryNotFoundException(request.CategoryId);
        
        var product = new Product(
            request.Name, 
            request.Description, 
            request.Price, 
            request.Stock, 
            request.Sku,
            request.CategoryId);
        
        await productRepository.AddAsync(product);
        return Unit.Value;
    }
}