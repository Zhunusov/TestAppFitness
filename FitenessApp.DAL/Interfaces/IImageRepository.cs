using System;
using FitenessApp.DAL.Entities;

namespace FitenessApp.DAL.Interfaces
{
    public interface IImageRepository : IRepository<Image>
    {
        Image Get(Guid id);
        bool Delete(Guid id);
    }
}
