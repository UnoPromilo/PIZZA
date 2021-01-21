using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PIZZA.Models.Instalation;

namespace PIZZA.WebApi.Controllers.Instalation
{
    [Route("api/instalation/[controller]")]
    [ApiController]
    public class GetConfigurationStatusController : ControllerBase
    {
        CustomSettings _customSettings;
        public GetConfigurationStatusController(CustomSettings customSettings)
        {
            _customSettings = customSettings;
        }

        /// <summary>
        /// Get configuration status
        /// </summary>
        /// <returns>
        /// Return true if application is configured 
        /// </returns>
        /// <response code="200">Success</response>

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ConfigurationStatus> Get()
        {
            return Ok(new ConfigurationStatus { Configured = _customSettings.Configured});
        }
    }
}
