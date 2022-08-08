using ConfigurationManager.Core;
using ConfigurationManager.Core.Models;
using ConfigurationManager.Core.Services;

namespace ConfigurationManager.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ConfigurationService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<Configuration> Create(Configuration configuration)
        {
            await _unitOfWork.Configurations.AddAsync(configuration);
            await _unitOfWork.CommitAsync();


            return configuration;
        }

        public async Task Delete(Configuration configuration)
        {
            _unitOfWork.Configurations.Remove(configuration);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Configuration>> GetAll()
        {
            return _unitOfWork.Configurations.Find(x => x.IsActive);
        }

        public async Task<Configuration> GetById(int id)
        {
            return await _unitOfWork.Configurations.GetByIdAsync(id);
        }
        public async Task<Configuration> GetByApplicationIdAndNameAsync(string applicationId, string name)
        {
            return await _unitOfWork.Configurations.SingleOrDefaultAsync(x => x.ApplicationId == applicationId && x.Name == name && x.IsActive);
        }
        public Configuration GetByApplicationIdAndName(string applicationId, string name)
        {
            return _unitOfWork.Configurations.Find(x => x.ApplicationId == applicationId && x.Name == name && x.IsActive).FirstOrDefault();
        }

        public async Task Update(Configuration configurationToBeUpdated, Configuration configuration)
        {
            configuration.Name = configurationToBeUpdated.Name;
            configuration.Type = configurationToBeUpdated.Type;
            configuration.ApplicationId = configurationToBeUpdated.ApplicationId;
            configuration.IsActive = configurationToBeUpdated.IsActive;

            await _unitOfWork.CommitAsync();
        }
    }
}
