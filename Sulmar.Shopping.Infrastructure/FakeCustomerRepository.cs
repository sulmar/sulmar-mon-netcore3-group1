using Microsoft.Extensions.Options;
using Sulmar.Shopping.Domain;
using Sulmar.Shopping.Domain.SearchCriterias;
using Sulmar.Shopping.Domain.Services;
using Sulmar.Shopping.Infrastructure.Fakers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Sulmar.Shopping.Infrastructure
{
    public class FakeCustomerRepositoryOptions
    {
        public int Quantity { get; set; }
    }

    public class FakeCustomerRepository : ICustomerRepository
    {
        private readonly ICollection<Customer> customers;

        private readonly FakeCustomerRepositoryOptions options;

        // dotnet add package Microsoft.Extensions.Option
        public FakeCustomerRepository(
            CustomerFaker customerFaker,
            IOptionsSnapshot<FakeCustomerRepositoryOptions> options)
        {

            this.options = options.Value;

            customers = customerFaker.Generate(this.options.Quantity);
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

        public Customer Get(string pesel)
        {
            return customers.SingleOrDefault(c => c.Pesel == pesel);
        }

        public IEnumerable<Customer> Get(CustomerSearchCriteria criteria)
        {
            IQueryable<Customer> query = customers.AsQueryable();

            if (!string.IsNullOrEmpty(criteria.FirstName))
            {
                query = query.Where(c => c.FirstName == criteria.FirstName);
            }

            if (!string.IsNullOrEmpty(criteria.LastName))
            {
                query = query.Where(c => c.LastName == criteria.LastName);
            }

            if (criteria.IsRemoved.HasValue)
            {
                query = query.Where(c => c.IsRemoved == criteria.IsRemoved);
            }

         //   query = query.OrderBy(c=>c.Id).Skip(20).Take()

           return query.ToList();
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
