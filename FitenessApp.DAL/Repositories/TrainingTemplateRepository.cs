using FitenessApp.DAL.Interfaces;
using FitenessApp.DAL.Entities;
using FitenessApp.DAL.EF;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;

namespace FitenessApp.DAL.Repositories
{
    public class TrainingTemplateRepository : ITrainingTemplateRepository
    {
        private ApplicationContext Database;
        public TrainingTemplateRepository(ApplicationContext db)
        {
            Database = db;
        }
        public IEnumerable<TrainingTemplate> GetAll()
        {
            return Database.TrainingTemplates                  
                  .ToList();
        }
        public TrainingTemplate Get(Guid id)
        {
            return Database.TrainingTemplates
                .Find(id);
        }
        public IEnumerable<TrainingTemplate> Find(Func<TrainingTemplate, Boolean> predicate)
        {
            return Database.TrainingTemplates                
                .Where(predicate).ToList();
        }
        public void Create(TrainingTemplate item)
        {
            Database.TrainingTemplates
                .Add(item);
        }
        public void Update(TrainingTemplate item)
        {
            Database.Entry(item).State = EntityState.Modified;
        }
        public bool Delete(Guid id)
        {
            TrainingTemplate trainingTemplate = Database.TrainingTemplates.Find(id);
            if (trainingTemplate != null)
            {
                Database.TrainingTemplates.Remove(trainingTemplate);
                return true;
            }
            return false;
        }
    }
}
