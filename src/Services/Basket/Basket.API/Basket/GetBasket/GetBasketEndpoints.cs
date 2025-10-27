
using MediatR;

namespace Basket.API.Basket.GetBasket
{
	public record GetBasketRequest(string userName);
	public record GetBasketResponse(ShoppingCart Cart);
	public class GetBasketEndpoints : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapGet("/basket/{username}", async (string username, ISender sender) =>
			{
				var result = await sender.Send(query);
				var response = result.Adapt<GetBasketResponse>();
				return Results.Ok(response);
			})
			.WithName("GetProducts")
			.Produces<GetBasketResponse>(StatusCodes.Status200OK)
			.ProducesProblem(StatusCodes.Status400BadRequest)
			.WithSummary("Get Products")
			.WithDescription("Get Products");
		}
	}
}
