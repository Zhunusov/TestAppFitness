using System;
using FitenessApp.DAL.Entities;

namespace FitenessApp.DAL.Interfaces
{
    public interface ITrainingTemplateRepository : IRepository<TrainingTemplate>
    {
        TrainingTemplate Get(Guid id);
        bool Delete(Guid id);
    }
}
