using ConfigurationManager.Core;
using ConfigurationManager.Core.Repositories;
using ConfigurationManager.Data.Repositories;

namespace ConfigurationManager.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ConfigurationManagerDbContext _context;
        private ApplicationRepository _applicationRepository;
        private ConfigurationRepository _configurationRepository;

        public UnitOfWork(ConfigurationManagerDbContext context)
        {
            this._context = context;
        }

        public IApplicationRepositroy Applications => _applicationRepository = _applicationRepository ?? new ApplicationRepository(_context);

        public IConfigurationRepository Configurations => _configurationRepository = _configurationRepository ?? new ConfigurationRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
