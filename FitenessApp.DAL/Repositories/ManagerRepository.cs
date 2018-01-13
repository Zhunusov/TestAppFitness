using FitenessApp.DAL.Interfaces;
using FitenessApp.DAL.Entities;
using FitenessApp.DAL.EF;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;

namespace FitenessApp.DAL.Repositories
{
    public class ManagerRepository : IUserManager<Manager>
    {
        private ApplicationContext db;

        public ManagerRepository(ApplicationContext context)
        {
            db = context;
        }

        public void Create(Manager item)
        {
            db.Managers.Add(item);
        }

        public Manager Get(string username)
        {
            return db.Managers
                .Include(m => m.ApplicationUser)
                .Include(m => m.Image)               
                .FirstOrDefault(a => a.ApplicationUser.UserName == username);
        }

        public IEnumerable<Manager> GetAll()
        {
            return db.Managers
                .Include(m => m.ApplicationUser)
                .Include(m => m.Image)
                .ToList();
        }

        public IEnumerable<Manager> Find(Func<Manager, Boolean> predicate)
        {
            return db.Managers
                .Include(m => m.ApplicationUser)
                .Include(m => m.Image)
                .Where(predicate)
                .ToList();
        }

        public void Update(Manager item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public bool Delete(string username)
        {
            Manager manager = db.Managers
                .FirstOrDefault(a => a.ApplicationUser.UserName == username);

            if (manager != null)
            {
                db.Managers.Remove(manager);
                return true;
            }
            return false;
        }
    }
}
