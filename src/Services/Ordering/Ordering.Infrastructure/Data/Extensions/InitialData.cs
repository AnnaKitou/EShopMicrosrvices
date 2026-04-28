namespace Ordering.Infrastructure.Data.Extensions
{
    internal class InitialData
    {
        public static IEnumerable<Customer> Customers => new List<Customer>
        {
             Customer.Create(CustomerId.Of(new Guid("11111111-1111-1111-1111-111111111111")), "John Doe", "johndoe@mail.com"),
             Customer.Create(CustomerId.Of(new Guid("22222222-2222-2222-2222-222222222222")), "Jane Smith", "janesmith@mail.com"),
             Customer.Create(CustomerId.Of(new Guid("33333333-3333-3333-3333-333333333333")), "Alice Johnson", "alicejohnson@mail.com"),
             Customer.Create(CustomerId.Of(new Guid("44444444-4444-4444-4444-444444444444")), "Bob Brown", "bobbrown@mail.com")
        };

        public static IEnumerable<Product> Products => new List<Product>
        {
             Product.Create(ProductId.Of(new Guid("33333333-3333-3333-3333-333333333333")) , "IPhone 15" , 1500),
             Product.Create(ProductId.Of(new Guid("44444444-4444-4444-4444-444444444444")), "Samsung 16 Pro", 650),
             Product.Create(ProductId.Of(new Guid("55555555-5555-5555-5555-555555555555")), "Google Pixel 8", 800),
             Product.Create(ProductId.Of(new Guid("66666666-6666-6666-6666-666666666666")), "OnePlus 12", 700),
             Product.Create(ProductId.Of(new Guid("77777777-7777-7777-7777-777777777777")), "Sony Xperia 5", 900),
             Product.Create(ProductId.Of(new Guid("88888888-8888-8888-8888-888888888888")), "Huawei P60", 750)
        };

        public static IEnumerable<Order> OrderWithItems
        {
            get
            {
                var order1 = Order.Create(
                     OrderId.Of(new Guid("77777777-7777-7777-7777-777777777777")),
                     CustomerId.Of(new Guid("11111111-1111-1111-1111-111111111111")),
                     OrderName.Of("ORD_1"),
                     Address.Of("123 Main St", "CityA", "StateA", "12345", "test", "test", "test"),
                     Address.Of("456 Elm St", "CityB", "StateB", "67890", "test", "test", "test"),
                     Payment.Of("Credit Card", "1234-5678-9012-3456", "06/30", "012", 132)
                );
                order1.Add(ProductId.Of(new Guid("88888888-8888-8888-8888-888888888888")), 750, 1);
                order1.Add(ProductId.Of(new Guid("77777777-7777-7777-7777-777777777777")), 900, 1);
                order1.Add(ProductId.Of(new Guid("66666666-6666-6666-6666-666666666666")), 700, 1);

                var order2 = Order.Create(
                     OrderId.Of(new Guid("88888888-8888-8888-8888-888888888888")),
                     CustomerId.Of(new Guid("22222222-2222-2222-2222-222222222222")),
                     OrderName.Of("ORD_2"),
                     Address.Of("789 Pine St", "CityC", "StateC", "54321", "test", "test", "test"),
                     Address.Of("101 Maple St", "CityD", "StateD", "98765", "test", "test", "test"),
                     Payment.Of("PayPal", "jane.smith@mail.com", "N/A", "N/A", 200)
                 );
                order2.Add(ProductId.Of(new Guid("33333333-3333-3333-3333-333333333333")), 1500, 1);
                order2.Add(ProductId.Of(new Guid("44444444-4444-4444-4444-444444444444")), 650, 1);
                order2.Add(ProductId.Of(new Guid("55555555-5555-5555-5555-555555555555")), 800, 1);
                return new List<Order> { order1, order2 };
            }

        }
    }   
}
