namespace Ordering.Infrastructure.Data.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<Domain.Models.OrderItem>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.OrderItem> builder)
        {
            builder.HasKey(oi => oi.Id);
            builder.Property(oi => oi.Id)
                .HasConversion(
                    orderItemId => orderItemId.Value,
                    dbId => OrderItemId.Of(dbId));

         

            builder.Property(oi => oi.Price).IsRequired();
            builder.Property(oi => oi.Quantity).IsRequired();
        }
    }
}
