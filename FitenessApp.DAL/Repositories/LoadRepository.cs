using FitnessApp.DAL.Interfaces;
using FitnessApp.DAL.Entities;
using FitnessApp.DAL.EF;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;

namespace FitnessApp.DAL.Repositories
{
    public class LoadRepository : ILoadRepository
    {
        private ApplicationContext Database;
        public LoadRepository(ApplicationContext db)
        {
            Database = db;
        }
        public IEnumerable<Load> GetAll()
        {
            return Database.Loads                  
                  .ToList();
        }
        public Load Get(string title)
        {
            return Database.Loads
                .FirstOrDefault(l => l.Title == title);
        }
        public IEnumerable<Load> Find(Func<Load, Boolean> predicate)
        {
            return Database.Loads                
                .Where(predicate).ToList();
        }
        public void Create(Load item)
        {
            Database.Loads
                .Add(item);
        }
        public void Update(Load item)
        {
            Database.Entry(item).State = EntityState.Modified;
        }
        public bool Delete(string title)
        {
            Load load = Database.Loads
                .FirstOrDefault(l => l.Title == title);
            if (load != null)
            {
                Database.Loads.Remove(load);
                return true;
            }
            return false;
        }
    }
}
