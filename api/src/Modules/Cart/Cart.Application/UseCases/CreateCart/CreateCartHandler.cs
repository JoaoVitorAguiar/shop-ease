using Cart.Domain.Exceptions;
using Cart.Domain.Repositories;
using MediatR;

namespace Cart.Application.UseCases.CreateCart;

public class CreateCartHandler(ICartRepository cartRepository): IRequestHandler<CreateCartCommand, Unit>
{
    public async Task<Unit> Handle(CreateCartCommand request, CancellationToken cancellationToken)
    {
        var existingCart = await cartRepository.GetByUserIdAsync(request.UserId);
        if(existingCart is not null) throw new CartAlreadyExistsException(request.UserId);
        
        var cart = new Domain.Entities.Cart(request.UserId); 
        await cartRepository.AddAsync(cart);
        return Unit.Value;
    }
}