using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;

namespace DemoMongo.DbCollections
{
    public class CategoryCollection : MongoCollectionBase
    {
        public CategoryCollection() : base("Categories")
        {

        }

        [BsonId(IdGenerator = typeof(CombGuidGenerator))]
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
