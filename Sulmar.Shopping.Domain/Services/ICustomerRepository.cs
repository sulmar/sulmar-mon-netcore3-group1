using Sulmar.Shopping.Domain.SearchCriterias;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Sulmar.Shopping.Domain.Services
{
    public interface ICustomerRepository : IEntityRepository<Customer>
    {
        Customer Get(string pesel);

        // zla praktyka
        // IEnumerable<Customer> Get(string country, string city, bool isRemoved);

        IEnumerable<Customer> Get(CustomerSearchCriteria criteria);
    }
}
