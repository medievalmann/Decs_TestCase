using ConfigurationManager.Core.Repositories;

namespace ConfigurationManager.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IApplicationRepositroy Applications { get; }
        IConfigurationRepository Configurations { get; }
        Task<int> CommitAsync();
    }
}
