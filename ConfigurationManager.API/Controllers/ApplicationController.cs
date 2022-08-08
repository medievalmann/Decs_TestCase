using ConfigurationManager.API.DTOs;
using ConfigurationManager.Core.Models;
using ConfigurationManager.Core.Services;
using Microsoft.AspNetCore.Mvc;


namespace ConfigurationManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        IApplicationService _applicationService;
        public ApplicationController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        public async Task<IEnumerable<Application>> Get()
        {
            return await _applicationService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<Application> Get(string id)
        {
            return await _applicationService.GetById(id);
        }

        [HttpPost]
        public async Task<Application> Post([FromBody] ApplicationSaveDTO value)
        {

            var application = new Application()
            {
                Name = value.Name
            };

            return await _applicationService.Create(application);

        }

        [HttpPut("{id}")]
        public async Task Put(string id, [FromBody] ApplicationSaveDTO value)
        {
            var applicationToBeUpdate = new Application()
            {
                Name = value.Name
            };

            var application = await _applicationService.GetById(id);

            await _applicationService.Update(applicationToBeUpdate, application);

        }
    }
}
