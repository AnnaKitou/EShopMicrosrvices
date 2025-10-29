
using FluentValidation;

namespace Basket.API.Basket.StoreBasket
{
	public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
	public record StoreBasketResult(bool Success);
	public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
	{
		public StoreBasketCommandValidator()
		{
			RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("UserName is required");
			RuleFor(x => x.Cart).NotNull().WithMessage("Cart cannot be null");
		}
	}

	public class StoreBasketHandler : ICommandHandler<StoreBasketCommand, StoreBasketResult>
	{
		public Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
