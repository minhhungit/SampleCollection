using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;

namespace DemoMongo
{
    public abstract class MongoCollectionBase
    {
        public MongoCollectionBase(string collectionName)
        {
            this._CollectionName = collectionName;
        }

        public MongoCollectionBase(string collectionName, Action<IMongoDatabase> registryIndexesAction)
        {
            this._CollectionName = collectionName;
            this._RegistryIndexesAction = registryIndexesAction;
        }

        [BsonIgnore]
        public string _CollectionName { get; private set; }

        [BsonIgnore]
        public Action<IMongoDatabase> _RegistryIndexesAction { get; private set; }
    }
}
