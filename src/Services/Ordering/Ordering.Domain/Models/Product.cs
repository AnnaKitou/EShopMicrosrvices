namespace Ordering.Domain.Models
{
	public class Product : Entity<ValueObjects.OrderItem>
	{
		public string Name { get; private set; } = default!;
		public decimal Price { get; private set; } = default!;

		public static Product Create(ValueObjects.OrderItem id, string name, decimal price)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(name);
			ArgumentOutOfRangeException.ThrowIfNegative(price);

			return new Product
			{
				Id = id,
				Name = name,
				Price = price
			};
		}
	}
}
