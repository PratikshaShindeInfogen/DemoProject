using MongoDB.Driver;
using DemoProject.Data;
using DemoProject.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace DemoProject.Services
{
    public class BaseService<T> :IBaseService<T> where T : IEntity
    {
        protected readonly IMongoCollection<T> _dbContext;

        public BaseService(IMongoDbContext dbContext)
        {
            _dbContext = dbContext.GetCollection<T>();
        }  
        
        public virtual async Task<List<T>> GetAll()
        {
            return await _dbContext.Find(_ => true).ToListAsync();
        }

        public virtual async Task<T> GetById(string id)
        {
            //return await _dbContext.Find(id).FirstOrDefaultAsync();
            if (!ObjectId.TryParse(id, out ObjectId objectId))
            {
                throw new ArgumentException("Invalid ObjectId format", nameof(id));
            }

            var filter = Builders<T>.Filter.Eq("_id", objectId);
            return await _dbContext.Find(filter).FirstOrDefaultAsync();

        }

        //public virtual async Task<T> GetByName(string Name)
        //{

        //    return await _dbContext.Find(Name).FirstOrDefaultAsync();
        //}

        public virtual async Task<T> UpdateAsync(string id, T entity )
        {
            //var objectId = new ObjectId(id);
            if (!ObjectId.TryParse(id, out ObjectId objectId))
            {
                throw new ArgumentException("Invalid ObjectId format", nameof(id));
            }

            var filter = Builders<T>.Filter.Eq("_id", objectId);
            await _dbContext.ReplaceOneAsync(filter, entity);
            return entity;
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            await _dbContext.InsertOneAsync(entity);
            return entity;
        }

        public virtual async Task DeleteAsync(FilterDefinition<T> filter, UpdateDefinition<T> updatedfilter)
        {      
            var updated = await _dbContext.UpdateOneAsync(filter, updatedfilter); 
        
        }

    }
}
