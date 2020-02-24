using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;
using System;

namespace DemoMongo.DbCollections
{
    public class ProductCollection : MongoCollectionBase
    {
        public ProductCollection() : base("Products", db => InitIndexes(db))
        {

        }

        static void InitIndexes(IMongoDatabase db)
        {
            var indexKeys = new BsonDocument() {
                { nameof(CategoryId), 1}
            };
            var indexOptions = new CreateIndexOptions() { Name = "CategoryId" };
            var indexModel = new CreateIndexModel<ProductCollection>(indexKeys, indexOptions);
            db.GetCollection<ProductCollection>().Indexes.CreateOne(indexModel);
        }

        [BsonId(IdGenerator = typeof(CombGuidGenerator))]
        public Guid Id { get; set; }

        public Guid CategoryId { get; set; }

        public string Name { get; set; }
    }
}
