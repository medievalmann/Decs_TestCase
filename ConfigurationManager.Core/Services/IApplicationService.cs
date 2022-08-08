using ConfigurationManager.Core.Models;

namespace ConfigurationManager.Core.Services
{
    public interface IApplicationService
    {
        Task<IEnumerable<Application>> GetAll();
        Task<Application> GetById(string id);
        Task<Application> Create(Application application);
        Task Update(Application applicationToBeUpdated,Application application);
        Task Delete(Application application);
    }
}
