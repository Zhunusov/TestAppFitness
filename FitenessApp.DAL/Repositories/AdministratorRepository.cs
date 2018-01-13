using FitenessApp.DAL.Interfaces;
using FitenessApp.DAL.Entities;
using FitenessApp.DAL.EF;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;

namespace FitenessApp.DAL.Repositories
{
    public class AdministratorRepository : IUserManager<Administrator>
    {
        private ApplicationContext db;

        public AdministratorRepository (ApplicationContext context)
        {
            db = context;
        }

        public void Create(Administrator item)
        {
            db.Administrators.Add(item);
        }

        public Administrator Get(string username)
        {
            return db.Administrators
                .Include(a => a.ApplicationUser)
                .Include(a => a.Image)                
                .FirstOrDefault(a => a.ApplicationUser.UserName == username);
        }

        public IEnumerable<Administrator> GetAll()
        {
            return db.Administrators
                .Include(a => a.ApplicationUser)
                .Include(a => a.Image)
                .ToList();
        }

        public IEnumerable<Administrator> Find(Func<Administrator, Boolean> predicate)
        {
            return db.Administrators
                .Include(a => a.ApplicationUser)
                .Include(a => a.Image)
                .Where(predicate)
                .ToList();
        }

        public void Update(Administrator item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public bool Delete(string username)
        {
            Administrator admin = db.Administrators
                .FirstOrDefault(a => a.ApplicationUser.UserName == username);

            if (admin != null)
            {
                db.Administrators.Remove(admin);
                return true;
            }
            return false;
        }
    }
}
