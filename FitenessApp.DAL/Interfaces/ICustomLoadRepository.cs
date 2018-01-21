using System;
using FitnessApp.DAL.Entities;

namespace FitnessApp.DAL.Interfaces
{
    public interface ICustomLoadRepository : IRepository<CustomLoad>
    {
        CustomLoad Get(Guid id);
        bool Delete(Guid id);
    }
}
