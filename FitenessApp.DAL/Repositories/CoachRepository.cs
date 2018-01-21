using FitnessApp.DAL.Interfaces;
using FitnessApp.DAL.Entities;
using FitnessApp.DAL.EF;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;

namespace FitnessApp.DAL.Repositories
{
    public class CoachRepository : IUserManager<Coach>
    {
        private ApplicationContext db;

        public CoachRepository(ApplicationContext context)
        {
            db = context;
        }

        public void Create(Coach item)
        {
            db.Coaches.Add(item);
        }

        public Coach Get(string username)
        {
            return db.Coaches
                .Include(c => c.ApplicationUser)
                .Include(c => c.Image)
                .Include(c => c.Manager)
                .FirstOrDefault(a => a.ApplicationUser.UserName == username);
        }

        public IEnumerable<Coach> GetAll()
        {
            return db.Coaches
                .Include(c => c.ApplicationUser)
                .Include(c => c.Image)
                .Include(c => c.Manager)
                .ToList();
        }

        public IEnumerable<Coach> Find(Func<Coach, Boolean> predicate)
        {
            return db.Coaches
                .Include(c => c.ApplicationUser)
                .Include(c => c.Image)
                .Include(c => c.Manager)
                .Where(predicate)
                .ToList();
        }

        public void Update(Coach item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public bool Delete(string username)
        {
            Coach coach = db.Coaches
                .FirstOrDefault(a => a.ApplicationUser.UserName == username);

            if (coach != null)
            {
                db.Coaches.Remove(coach);
                return true;
            }
            return false;
        }
    }
}
