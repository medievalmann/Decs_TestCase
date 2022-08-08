using ConfigurationManager.Reader;
using Microsoft.AspNetCore.Mvc;

namespace Test.API.Controllers
{
    public class TestController : Controller
    {
        private IConfigurationReader _configurationReader;
        public TestController(IConfigurationReader configurationReader)
        {
            _configurationReader = configurationReader;
        }
        [HttpGet("{value}")]
        public object Get(TestValues value)
        {
            switch (value)
            {
                case TestValues.SiteName:
                    return Ok(_configurationReader.GetValue<string>("SiteName"));
                case TestValues.IsBasketEnabled:
                    return Ok(_configurationReader.GetValue<bool>("IsBasketEnabled"));
                case TestValues.MaxItemCount:
                    return Ok(_configurationReader.GetValue<int>("MaxItemCount"));
                default:
                    return BadRequest();
            }
        }
    }

    public enum TestValues
    {
        SiteName,
        IsBasketEnabled,
        MaxItemCount
    }
}
