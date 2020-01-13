using System;
using System.Collections.Generic;
using System.Text;

namespace Sulmar.Shopping.Domain.Services
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> Get();
        Customer Get(int id);
        void Add(Customer customer);
        void Update(Customer customer);
        void Remove(int id);
    }

    public interface IProductRepository
    {
        IEnumerable<Product> Get();
        Product Get(int id);
        void Add(Customer customer);
        void Update(Product product);
        void Remove(int id);
    }
}
