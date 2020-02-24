using DemoMongo.DbCollections;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace DemoMongo
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json", true, true)
              .AddUserSecrets("12e8e82a-b14e-4039-b005-92bb9e1b9727") // C:\Users\admin\AppData\Roaming\Microsoft\UserSecrets\12e8e82a-b14e-4039-b005-92bb9e1b9727
              .Build();

            var testDbconnectionString = config.GetSection("ConnectionStrings:TestDb").Value;

            Console.WriteLine("Hello World!");

            IMongoContext ctx = new MongoContext(testDbconnectionString);

            var firstCategory = new CategoryCollection
            {
                Name = "Demo category 01"
            };

            ctx.DefaultDatabase.GetCollection<CategoryCollection>()
                .InsertOne(firstCategory);

            ctx.DefaultDatabase.GetCollection<ProductCollection>()
                .InsertMany(new List<ProductCollection>
                {
                    new ProductCollection
                    {
                        CategoryId = firstCategory.Id,
                        Name = "Product 01"
                    },
                    new ProductCollection
                    {
                        CategoryId = firstCategory.Id,
                        Name = "Product 02"
                    }
                });

            var countCat = ctx.DefaultDatabase.GetCollection<CategoryCollection>().EstimatedDocumentCountAsync().GetAwaiter().GetResult();
            var countProduct = ctx.DefaultDatabase.GetCollection<ProductCollection>().EstimatedDocumentCountAsync().GetAwaiter().GetResult();
            Console.WriteLine($"{countCat } / {countProduct}");

            Console.ReadKey();
        }
    }
}
