using System;
using FitenessApp.DAL.Entities;

namespace FitenessApp.DAL.Interfaces
{
    public interface ICustomLoadRepository : IRepository<CustomLoad>
    {
        CustomLoad Get(Guid id);
        bool Delete(Guid id);
    }
}
