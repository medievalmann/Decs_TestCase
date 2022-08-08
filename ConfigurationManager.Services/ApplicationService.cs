using ConfigurationManager.Core;
using ConfigurationManager.Core.Models;
using ConfigurationManager.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationManager.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ApplicationService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<Application> Create(Application application)
        {
            application.Id = Guid.NewGuid().ToString("N");
            await _unitOfWork.Applications.AddAsync(application);
            await _unitOfWork.CommitAsync();
            return application;
        }

        public async Task Delete(Application application)
        {
            _unitOfWork.Applications.Remove(application);

            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Application>> GetAll()
        {
            return await _unitOfWork.Applications.GetAllAsync();
        }

        public async Task<Application> GetById(string id)
        {
            return await _unitOfWork.Applications.GetByIdAsync(id);
        }

        public async Task Update(Application applicationToBeUpdated, Application application)
        {
            application.Name = applicationToBeUpdated.Name;

            await _unitOfWork.CommitAsync();
        }
    }
}
