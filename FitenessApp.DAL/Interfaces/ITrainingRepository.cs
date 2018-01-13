using System;
using FitenessApp.DAL.Entities;

namespace FitenessApp.DAL.Interfaces
{
    public interface ITrainingRepository : IRepository<Training>
    {
        Training Get(Guid id);
        bool Delete(Guid id);
    }
}
