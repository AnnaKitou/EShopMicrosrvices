namespace Ordering.Domain.Models
{
	public class Order : Aggregate<OrderId>
	{
		// One to N relationship
		public readonly List<OrderItem> _orderItems = new();
		public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();
		public CustomerId CustomerId { get; private set;  } = default!;
		public OrderName OrderName { get; private set; } = default!;
		public Address ShippingAddress { get; private set; } = default!;
		public Address BillingAddress { get; private set; } = default!;
		public Payment Payment { get; private set; } = default!;
		public OrderStatus Status { get; private set; } = OrderStatus.Pending;

		public decimal TotalPrice
		{
			get => _orderItems.Sum(item => item.Price * item.Quantity);
			private set { }
		}
	}
}
