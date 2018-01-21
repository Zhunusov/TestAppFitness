using FitnessApp.DAL.Interfaces;
using FitnessApp.DAL.Entities;
using FitnessApp.DAL.EF;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;

namespace FitnessApp.DAL.Repositories
{
    public class TrainingRepository : ITrainingRepository
    {
        private ApplicationContext Database;
        public TrainingRepository(ApplicationContext db)
        {
            Database = db;
        }
        public IEnumerable<Training> GetAll()
        {
            return Database.Trainings
                  .Include(t => t.Coach)
                  .Include(t => t.Customer)
                  .Include(t => t.Manager)                  
                  .ToList();
        }
        public Training Get(Guid id)
        {
            return Database.Trainings
                .Find(id);
        }
        public IEnumerable<Training> Find(Func<Training, Boolean> predicate)
        {
            return Database.Trainings
                .Include(t => t.Coach)
                .Include(t => t.Customer)
                .Include(t => t.Manager)
                .Where(predicate).ToList();
        }
        public void Create(Training item)
        {
            Database.Trainings
                .Add(item);
        }
        public void Update(Training item)
        {
            Database.Entry(item).State = EntityState.Modified;
        }
        public bool Delete(Guid id)
        {
            Training training = Database.Trainings.Find(id);
            if (training != null)
            {
                Database.Trainings.Remove(training);
                return true;
            }
            return false;
        }
    }
}
