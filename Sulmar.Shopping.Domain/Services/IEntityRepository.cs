using System.Collections.Generic;

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
}
