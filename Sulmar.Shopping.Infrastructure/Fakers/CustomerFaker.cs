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
            RuleFor(p => p.Id, f => f.IndexFaker);
            RuleFor(p => p.FirstName, f => f.Person.FirstName);
            RuleFor(p => p.LastName, f => f.Person.LastName);
            RuleFor(p => p.IsRemoved, f => f.Random.Bool(0.2f));
        }
    }
}
