using FitnessApp.DAL.Interfaces;
using FitnessApp.DAL.Entities;
using FitnessApp.DAL.EF;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;

namespace FitnessApp.DAL.Repositories
{
    public class CustomerRepository : IUserManager<Customer>
    {
        private ApplicationContext db;

        public CustomerRepository(ApplicationContext context)
        {
            db = context;
        }

        public void Create(Customer item)
        {
            db.Customers.Add(item);
        }

        public Customer Get(string username)
        {
            return db.Customers
                .Include(c => c.ApplicationUser)
                .Include(c => c.Image)
                .Include(c => c.Manager)
                .Include(c => c.Coach)
                .FirstOrDefault(a => a.ApplicationUser.UserName == username);
        }

        public IEnumerable<Customer> GetAll()
        {
            return db.Customers
                .Include(c => c.ApplicationUser)
                .Include(c => c.Image)
                .Include(c => c.Manager)
                .Include(c => c.Coach)
                .ToList();
        }

        public IEnumerable<Customer> Find(Func<Customer, Boolean> predicate)
        {
            return db.Customers
                .Include(c => c.ApplicationUser)
                .Include(c => c.Image)
                .Include(c => c.Manager)
                .Include(c => c.Coach)
                .Where(predicate)
                .ToList();
        }

        public void Update(Customer item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public bool Delete(string username)
        {
            Customer customer = db.Customers
                .FirstOrDefault(a => a.ApplicationUser.UserName == username);

            if (customer != null)
            {
                db.Customers.Remove(customer);
                return true;
            }
            return false;
        }
    }
}
