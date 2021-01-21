using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PIZZA.Enums;
using PIZZA.Models.Instalation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PIZZA.WebApi.Controllers.Instalation
{


    [Route("api/instalation/[controller]")]
    [ApiController]
    public class ConfigureServerController : ControllerBase
    {
        CustomSettings _customSettings;
        public ConfigureServerController(CustomSettings customSettings)
        {
            _customSettings = customSettings;
        }

        /// <summary>
        /// Configure server with given parameters.
        /// </summary>
        /// <param name="instalationInfo">All data needed to perfom instalation.</param>
        /// <response code="200">Success.</response>
        /// <response code="403">Server is already configured.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult Post([FromBody] InstalationInfo instalationInfo)
        {
            if (_customSettings.Configured)
                return Forbid();

            return Ok();
        }

        public bool TryCreateDatabase(SQLServerConfiguration sqlServerConfiguration)
        {

            if (sqlServerConfiguration.DatabaseConnectionType == DatabaseConnectionType.Advanced)
            {
                if (String.IsNullOrWhiteSpace(sqlServerConfiguration.ConnectionString)) return false;

            }
            else if(sqlServerConfiguration.DatabaseConnectionType == DatabaseConnectionType.SQLServerAuthentication)
            {
                if (String.IsNullOrWhiteSpace(sqlServerConfiguration.DatabaseAddress)) return false;
                if (String.IsNullOrWhiteSpace(sqlServerConfiguration.DatabaseName)) return false;
                if (String.IsNullOrWhiteSpace(sqlServerConfiguration.DatabaseUsername)) return false;
                if (String.IsNullOrWhiteSpace(sqlServerConfiguration.DatabasePassword)) return false;
                sqlServerConfiguration.ConnectionString = $"Server={sqlServerConfiguration.DatabaseAddress};" +
                                                          $"Database={sqlServerConfiguration.DatabaseName};" +
                                                          $"User Id={sqlServerConfiguration.DatabaseUsername};" +
                                                          $"Password={sqlServerConfiguration.DatabasePassword};";
            }
            else if (sqlServerConfiguration.DatabaseConnectionType == DatabaseConnectionType.WindowsAuthentication)
            {
                if (String.IsNullOrWhiteSpace(sqlServerConfiguration.DatabaseAddress)) return false;
                if (String.IsNullOrWhiteSpace(sqlServerConfiguration.DatabaseName)) return false;
                sqlServerConfiguration.ConnectionString = $"Server={sqlServerConfiguration.DatabaseAddress};" +
                                                          $"Database={sqlServerConfiguration.DatabaseName};" +
                                                          $"Trusted_Connection=True;";
            }


            return true;
        }
    }
}
