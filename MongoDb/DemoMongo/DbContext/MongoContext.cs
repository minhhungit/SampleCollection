using MongoDB.Driver;

namespace DemoMongo
{
    public class MongoContext : IMongoContext
    {
        public MongoContext(string connectionString)
        {
            var url = new MongoUrl(connectionString);
            this.Client = new MongoClient(connectionString);
            this.DefaultDatabase = Client.GetDatabase(url.DatabaseName);
            MongoExtensions.RegistryMongoCollectionsAndIndexes(this.DefaultDatabase);
        }
        public IMongoClient Client { get; private set; }

        public IMongoDatabase DefaultDatabase { get; private set; }
    }
}
