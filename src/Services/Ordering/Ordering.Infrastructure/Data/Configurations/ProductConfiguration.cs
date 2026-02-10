namespace Ordering.Infrastructure.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p=>p.Id)
                .HasConversion(
                    id => id.Value,
                    dbId => ProductId.Of(dbId));

               builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
        }
    }
}
