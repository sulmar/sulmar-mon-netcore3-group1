using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sulmar.Shopping.Domain.Services
{
    public interface IEntityRepository<TEntity>
    {
        IEnumerable<TEntity> Get();
        TEntity Get(int id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(int id);
    }

    public interface IEntityRepositoryAsync<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAsync();
        Task<TEntity> GetAsync(int id);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task RemoveAsync(int id);
    }
}
