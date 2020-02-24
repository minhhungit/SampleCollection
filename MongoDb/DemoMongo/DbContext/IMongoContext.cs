using MongoDB.Driver;

namespace DemoMongo
{
    public interface IMongoContext
    {
        IMongoClient Client { get; }
        IMongoDatabase DefaultDatabase { get; }
    }
}
