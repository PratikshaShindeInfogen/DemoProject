using DemoProject.Models;
using MongoDB.Driver;

namespace DemoProject.Services
{
    public interface IBaseService<T> where T :IEntity
    {
        Task<List<T>> GetAll();
        Task<T> GetById(string id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(string id, T entity);
        Task DeleteAsync(FilterDefinition<T> filter, UpdateDefinition<T> update);
    }
    
}
