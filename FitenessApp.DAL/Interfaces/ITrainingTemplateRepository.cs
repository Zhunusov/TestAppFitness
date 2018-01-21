using System;
using FitnessApp.DAL.Entities;

namespace FitnessApp.DAL.Interfaces
{
    public interface ITrainingTemplateRepository : IRepository<TrainingTemplate>
    {
        TrainingTemplate Get(Guid id);
        bool Delete(Guid id);
    }
}
