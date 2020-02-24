using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace DemoMongo
{
    public static class MongoExtensions
    {
        public static ConcurrentDictionary<Type, string> CollectionNameStore { get; } = new ConcurrentDictionary<Type, string>();
        public static ConcurrentBag<Type> IndexesRegisteredCache { get; } = new ConcurrentBag<Type>();

        public static IMongoCollection<TDocument> GetCollection<TDocument>(this IMongoDatabase db) where TDocument : MongoCollectionBase
        {
            var TDocumentType = typeof(TDocument);

            if (CollectionNameStore.ContainsKey(TDocumentType))
            {
                return db.GetCollection<TDocument>(CollectionNameStore[TDocumentType]);
            }

            throw new Exception($"Please setup collection name for type {TDocumentType}");
        }

        public static BsonArray ToBsonArray<T>(this IEnumerable<T> list) where T : class
        {
            return new BsonArray(list.Select(i => i.ToBsonDocument()));
        }

        public static void RegistryMongoCollectionsAndIndexes(this IMongoDatabase db)
        {
            var objects = new List<MongoCollectionBase>();
            var resource = AppDomain.CurrentDomain.GetAssemblies();
            var mongoCollectionTypes = resource
                .SelectMany(x => x.GetTypes())
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(MongoCollectionBase)))
                .Select(x => (MongoCollectionBase)Activator.CreateInstance(x));

            foreach (var item in mongoCollectionTypes)
            {
                if (!CollectionNameStore.ContainsKey(item.GetType()))
                {
                    CollectionNameStore[item.GetType()] = item._CollectionName;
                }

                if (!IndexesRegisteredCache.Contains(item.GetType()))
                {
                    item?._RegistryIndexesAction?.Invoke(db);
                    IndexesRegisteredCache.Add(item.GetType());
                }
            }
        }
    }
}
