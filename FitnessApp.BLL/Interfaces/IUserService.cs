using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessApp.BLL.Infrastructure;

namespace FitnessApp.BLL.Interfaces
{
    public interface IUserService<T> : IDisposable
    {
        Task<OperationDetails> Create(T itemDto);
        Task<OperationDetails> Update(T itemDto);
        OperationDetails GetPersonalInfo(string username, out T itemDto);
        Task<string> GetRole(string username);
        Task<ICollection<T>> GetAll();
        Task<OperationDetails> RemoveUser(string username);
    }
}
