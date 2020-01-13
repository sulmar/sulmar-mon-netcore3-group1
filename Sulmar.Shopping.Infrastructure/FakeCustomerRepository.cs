using Sulmar.Shopping.Domain;
using Sulmar.Shopping.Domain.Services;
using Sulmar.Shopping.Infrastructure.Fakers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sulmar.Shopping.Infrastructure
{
    public class FakeCustomerRepository : ICustomerRepository
    {
        private readonly ICollection<Customer> customers;

        public FakeCustomerRepository(CustomerFaker customerFaker)
        {
            customers = customerFaker.Generate(100);
        }

        public void Add(Customer entity)
        {
            customers.Add(entity);
        }

        public IEnumerable<Customer> Get()
        {
            return customers;
        }

        public Customer Get(int id)
        {
            return customers.SingleOrDefault(c => c.Id == id);
        }

        public void Remove(int id)
        {
            customers.Remove(Get(id));
        }

        public void Update(Customer entity)
        {
            throw new NotImplementedException();
        }
    }
}
