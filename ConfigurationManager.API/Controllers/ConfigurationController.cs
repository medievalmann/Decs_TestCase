using ConfigurationManager.API.DTOs;
using ConfigurationManager.Core;
using ConfigurationManager.Core.Models;
using ConfigurationManager.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConfigurationManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        IConfigurationService _configurationService;
        public ConfigurationController(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        [HttpGet]
        public async Task<IEnumerable<Configuration>> Get()
        {
            return await _configurationService.GetAll();
        }

        [HttpGet("{id}")]
        public Task<Configuration> Get(int id)
        {
            return _configurationService.GetById(id);
        }

        [HttpPost]
        public Task<Configuration> Post([FromBody] ConfigurationSaveDTO value)
        {
            var configuration = new Configuration()
            {
                Name = value.Name,
                Type = value.Type,
                IsActive = value.IsActive,
                Value = value.Value,
                ApplicationId = value.ApplicationId,
            };

            return _configurationService.Create(configuration);

        }

        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] ConfigurationSaveDTO value)
        {
            var configurationToBeUpdate = new Configuration()
            {
                Name = value.Name,
                Type = value.Type,
                IsActive = value.IsActive,
                Value = value.Value,
                ApplicationId = value.ApplicationId,
            };

            var configuration = await _configurationService.GetById(id);

            await _configurationService.Update(configurationToBeUpdate, configuration);

        }

       

    }
}
