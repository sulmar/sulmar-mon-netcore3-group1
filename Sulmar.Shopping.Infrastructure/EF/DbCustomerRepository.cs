using Microsoft.EntityFrameworkCore;
using Sulmar.Shopping.Domain;
using Sulmar.Shopping.Domain.SearchCriterias;
using Sulmar.Shopping.Domain.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulmar.Shopping.Infrastructure.EF
{

    public class DbCustomerRepository : ICustomerRepositoryAsync
    {
        private readonly ShoppingContext context;

        public DbCustomerRepository(ShoppingContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(Customer entity)
        {
            await context.Customers.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Customer>> GetAsync(CustomerSearchCriteria criteria)
        {
            var query = context.Customers.AsQueryable();

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

            return await query.ToListAsync();
        }

        public Task<Customer> GetAsync(string pesel)
        {
            return context.Customers.AsNoTracking().SingleOrDefaultAsync(c => c.Pesel == pesel);
        }

        public async Task<IEnumerable<Customer>> GetAsync()
        {
            return await context.Customers.AsNoTracking().ToListAsync();
        }

        public async Task<Customer> GetAsync(int id)
        {
            return await context.Customers.FindAsync(id);
        }

        public async Task RemoveAsync(int id)
        {
            //Customer customer = await GetAsync(id);
            //context.Customers.Remove(customer);
            //await context.SaveChangesAsync();

            Customer customer = new Customer { Id = id };
            // context.Customers.Attach(customer);
            context.Entry(customer).State = EntityState.Deleted;
            await context.SaveChangesAsync();
        }

        public bool TryAuthenticate(string username, string password, out Customer customer)
        {
            customer = context.Customers
                .SingleOrDefault(c => c.Login == username && c.HashPassword == password);

            return customer != null;
        }

        public async Task UpdateAsync(Customer entity)
        {
            Trace.WriteLine(context.Entry(entity).State);

            context.Entry(entity).State = EntityState.Modified;
            Trace.WriteLine(context.Entry(entity).State);

            var entities = context.ChangeTracker.Entries();


            context.ChangeTracker.TrackGraph(entity, e =>
            {
                if (e.Entry.IsKeySet)
                {
                    e.Entry.State = EntityState.Unchanged;
                }
                else
                {
                    e.Entry.State = EntityState.Added;
                }

            });

            //  context.Entry(entity).Property(p => p.City).IsModified = true;
            await context.SaveChangesAsync();

            Trace.WriteLine(context.Entry(entity).State);

        

        }
    }
}
