using System.Collections.Generic;

namespace Sulmar.Shopping.Domain.Services
{
    public interface IProductRepository : IEntityRepository<Product>
    {
        IEnumerable<Product> Get(decimal from, decimal to);
    }
}
