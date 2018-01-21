using System;
using FitnessApp.DAL.Entities;

namespace FitnessApp.DAL.Interfaces
{
    public interface IImageRepository : IRepository<Image>
    {
        Image Get(Guid id);
        bool Delete(Guid id);
    }
}
