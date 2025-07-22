using DemoProject.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;

namespace DemoProject.Data
{
    //public interface IMongoDbContext
    //{
    //    IMongoCollection<Student> Students { get; }
    //}
    public class MongoDbContext : IMongoDbContext
    {
        public readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration) 
        { 
         var connectionString = configuration.GetConnectionString("MongoDB");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString), "MongoDB connection string is missing.");
            }
            var client = new MongoClient(connectionString);
         _database = client.GetDatabase("studentDB");
        }

        public IMongoCollection<T> GetCollection<T>() 
        {
            return _database.GetCollection<T>(typeof(T).Name.ToLower() + "s");
        }

    }
}
