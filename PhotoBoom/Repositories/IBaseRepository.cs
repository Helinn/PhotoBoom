
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace PhotoBoom.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        bool Add(TEntity obj);
        void Update(TEntity obj);
        void Delete(string id);
        TEntity Get(string id);
        ICollection<TEntity> Get();

        Task<ICollection<TEntity>> GetByFilter(FilterDefinition<TEntity> filter);
    }
}