using Sulmar.Shopping.Domain.SearchCriterias;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Sulmar.Shopping.Domain.Services
{
    public interface ICustomerRepository : IEntityRepository<Customer>
    {
        Customer Get(string pesel);

        // zla praktyka
        // IEnumerable<Customer> Get(string country, string city, bool isRemoved);

        IEnumerable<Customer> Get(CustomerSearchCriteria criteria);
    }

    public interface ICustomerRepositoryAsync : IEntityRepositoryAsync<Customer>
    {
        Task<Customer> GetAsync(string pesel);
        Task<IEnumerable<Customer>> GetAsync(CustomerSearchCriteria criteria);

        bool TryAuthenticate(string username, string password, out Customer customer);
    }
}
