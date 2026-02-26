namespace Ordering.Domain.Models
{
	public class OrderItem : Entity<OrderItemId>
	{
		public OrderId OrderId { get; private set; } = default!;
		public ValueObjects.OrderItem ProductId { get; private set; } = default!;
		public decimal Price { get; private set; } = default!;
		public int Quantity { get; private set; } = default!;

		internal OrderItem(OrderId orderId, ValueObjects.OrderItem productId, decimal price, int quantity)
		{
			Id = OrderItemId.Of(Guid.NewGuid());
			OrderId = orderId;
			ProductId = productId;
			Price = price;
			Quantity = quantity;
		}
	}
}
