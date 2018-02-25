using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessApp.BLL.DTO;
using FitnessApp.BLL.Infrastructure;

namespace FitnessApp.BLL.Interfaces
{
    public interface ICustomerService : IUserService<CustomerDTO>
    {
        Task<OperationDetails> CreateByAdministrator(CustomerDTO itemDto);
        ICollection<TrainingTemplateDTO> GetAllTrainingTemplates();
        Task<List<TrainingTemplateDTO>> GetFilteredTrainingTemplates(string aimParameter, string levelParameter, string forWhomParameter);
        Task<TrainingTemplateDTO> GetTrainingTemplateById(Guid? Id);
    }
}
