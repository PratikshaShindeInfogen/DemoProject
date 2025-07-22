using MongoDB.Driver;

namespace DemoProject.Data
{
    public interface IMongoDbContext
    {
        IMongoCollection<T> GetCollection<T>();
    }
}
