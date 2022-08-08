using ConfigurationManager.Core.Models;

namespace ConfigurationManager.Core.Services
{
    public interface IConfigurationService
    {
        Task<IEnumerable<Configuration>> GetAll();
        Task<Configuration> GetById(int id);
        Task<Configuration> GetByApplicationIdAndNameAsync(string applicationId, string name);
        Task<Configuration> Create(Configuration configuration);
        Task Update(Configuration configurationToBeUpdated, Configuration configuration);
        Task Delete(Configuration configuration);
    }
}
