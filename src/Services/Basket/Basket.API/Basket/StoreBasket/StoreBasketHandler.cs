
namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
    public record StoreBasketResult(string UserName);
    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("UserName is required");
            RuleFor(x => x.Cart).NotNull().WithMessage("Cart cannot be null");
        }
    }

    public class StoreBasketHandler(IBasketRepository basketRepository) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
			//TODO : communicate with Dicount.Grpc and calculate lastet prices for products in the basket
			//TODO: strore basket in database (use Marten upsert - if exist = updates, if not exist = insert)
			ShoppingCart cart = command.Cart;

            await basketRepository.StoreBasket(cart, cancellationToken);

            return new StoreBasketResult(command.Cart.UserName);
        }
    }
}
