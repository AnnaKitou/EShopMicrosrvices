namespace Ordering.Domain.ValueObjects
{
	public record OrderItem
	{
		public Guid Value { get; }
        private OrderItem(Guid value) => Value = value;

        public static OrderItem Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
            {
                throw new DomainException("ProductId value cannot be an empty GUID.");
            }

            return new OrderItem(value);
        }
    }
}
