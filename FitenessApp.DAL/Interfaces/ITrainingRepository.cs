using System;
using FitnessApp.DAL.Entities;

namespace FitnessApp.DAL.Interfaces
{
    public interface ITrainingRepository : IRepository<Training>
    {
        Training Get(Guid id);
        bool Delete(Guid id);
    }
}
