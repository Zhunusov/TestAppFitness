using FitnessApp.DAL.Interfaces;
using FitnessApp.DAL.Entities;
using FitnessApp.DAL.EF;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;

namespace FitnessApp.DAL.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private ApplicationContext Database;
        public ImageRepository(ApplicationContext db)
        {
            Database = db;
        }
        public IEnumerable<Image> GetAll()
        {
            return Database.Images
                  .Include(i => i.ApplicationUser)                  
                  .ToList();
        }
        public Image Get(Guid id)
        {
            return Database.Images
                .Find(id);
        }
        public IEnumerable<Image> Find(Func<Image, Boolean> predicate)
        {
            return Database.Images
                .Include(i => i.ApplicationUser)
                .Where(predicate).ToList();
        }
        public void Create(Image item)
        {
            Database.Images
                .Add(item);
        }
        public void Update(Image item)
        {
            Database.Entry(item).State = EntityState.Modified;
        }
        public bool Delete(Guid id)
        {
            Image image = Database.Images.Find(id);
            if (image != null)
            {
                Database.Images.Remove(image);
                return true;
            }
            return false;
        }
    }
}
