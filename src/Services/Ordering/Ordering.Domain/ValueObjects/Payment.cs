namespace Ordering.Domain.ValueObjects
{
    public record Payment
    {
        public string? CardName { get; } = default!;
        public string CardNumber { get; } = default!;
        public string Expiration { get; } = default!;
        public string CVV { get; } = default!;
        public int PaymentMethod { get; } = default!;

        protected Payment()
        {
        }

        private Payment(string? cardName, string cardNumber, string expration, string cVV, int paymentMethod)
        {
            CardName = cardName;
            CardNumber = cardNumber;
            Expiration = expration;
            CVV = cVV;
            PaymentMethod = paymentMethod;
        }

        public static Payment Of(string cardName, string cardNumber, string expration, string cVV, int paymentMethod)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(cardNumber);
            ArgumentException.ThrowIfNullOrWhiteSpace(expration);
            ArgumentException.ThrowIfNullOrWhiteSpace(cVV);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(cVV.Length, 3);
            return new Payment(cardName, cardNumber, expration, cVV, paymentMethod);
        }
    }
}
