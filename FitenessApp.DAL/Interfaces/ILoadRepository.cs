using System;
using FitnessApp.DAL.Entities;

namespace FitnessApp.DAL.Interfaces
{
    public interface ILoadRepository : IRepository<Load>
    {
        Load Get(string title);
        bool Delete(string title);
    }
}
