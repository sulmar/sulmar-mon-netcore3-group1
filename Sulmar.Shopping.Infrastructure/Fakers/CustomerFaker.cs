using Bogus;
using Sulmar.Shopping.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sulmar.Shopping.Infrastructure.Fakers
{
    public class CustomerFaker : Faker<Customer>
    {
        public CustomerFaker()
        {
            StrictMode(true);
            UseSeed(1);
            RuleFor(p => p.Id, f => f.IndexFaker);
            RuleFor(p => p.FirstName, f => f.Person.FirstName);
            RuleFor(p => p.LastName, f => f.Person.LastName);
            //Ignore(p > => p.Pesel);
            RuleFor(p => p.Pesel, f => f.Random.String2(11, "0123456789"));
            RuleFor(p => p.City, f => f.Person.Address.City);
            RuleFor(p => p.PostCode, f => f.Person.Address.ZipCode);
            RuleFor(p => p.Email, (f, customer) => $"{customer.FirstName}.{customer.LastName}@mon.gov.pl");
            RuleFor(p => p.IsRemoved, f => f.Random.Bool(0.2f));
        }
    }
}
