using System;
using FitenessApp.DAL.Entities;

namespace FitenessApp.DAL.Interfaces
{
    public interface ILoadRepository : IRepository<Load>
    {
        Load Get(string title);
        bool Delete(string title);
    }
}
