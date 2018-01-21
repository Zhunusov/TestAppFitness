using FitnessApp.DAL.Interfaces;
using FitnessApp.DAL.Entities;
using FitnessApp.DAL.EF;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;

namespace FitnessApp.DAL.Repositories
{
    public class CustomLoadRepository : ICustomLoadRepository
    {
        private ApplicationContext Database;
        public CustomLoadRepository(ApplicationContext db)
        {
            Database = db;
        }
        public IEnumerable<CustomLoad> GetAll()
        {
            return Database.CustomLoads
                  .Include(c => c.Load)
                  .Include(c => c.Training)
                  .ToList();
        }
        public CustomLoad Get(Guid id)
        {
            return Database.CustomLoads
                .Find(id);
        }
        public IEnumerable<CustomLoad> Find(Func<CustomLoad, Boolean> predicate)
        {
            return Database.CustomLoads
                .Include(c => c.Load)
                .Include(c => c.Training)
                .Where(predicate).ToList();
        }
        public void Create(CustomLoad item)
        {
            Database.CustomLoads
                .Add(item);
        }
        public void Update(CustomLoad item)
        {
            Database.Entry(item).State = EntityState.Modified;
        }
        public bool Delete(Guid id)
        {
            CustomLoad customLoad = Database.CustomLoads.Find(id);
            if (customLoad != null)
            {
                Database.CustomLoads.Remove(customLoad);
                return true;
            }
            return false;
        }
    }
}
