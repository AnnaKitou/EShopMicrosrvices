using Marten.Schema;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Catalog.API.Data
{
    public class CatalogInitialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession();
            if (await session.Query<Product>().AnyAsync())
                return;

            //Marten Upsert will cater for existing records
            session.Store<Product>(GetConfiguredProducts());
            await session.SaveChangesAsync();
        }

        private static IEnumerable<Product> GetConfiguredProducts() => new List<Product>
        {
            new Product()
            {
                Id=new Guid(),
                Name="IPhone X",
                Description="This phone is the company's biggest change to it's ",
                ImageFile="product-1.png",
                Price=950.00M,
                Category= new List<string>{"Smart Phone"}
            },
            new Product()
            {
                            Id=new Guid(),
                Name="Samsung 10",
                Description="This phone is the company's biggest change to it's ",
                ImageFile="product-2.png",
                Price=840.00M,
                Category= new List<string>{"White Appliances"}
            },
                 new Product()
            {
                 Id=new Guid(),
                Name="Huawei Plus X",
                Description="This phone is the company's biggest change to it's ",
                ImageFile="product-3.png",
                Price=650.00M,
                Category= new List<string>{ "White Appliances" }
            }


        };

    }
}
