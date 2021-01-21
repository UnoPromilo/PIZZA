using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PIZZA.Enums;
using PIZZA.Models.Instalation;
using PIZZA.Models.Results;
using PIZZA.Models.User;
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
        private  CustomSettings _customSettings;
        private readonly UserManager<ApplicationUser> _userManager;
        public ConfigureServerController(CustomSettings customSettings, UserManager<ApplicationUser> userManager)
        {
            _customSettings = customSettings;
            _userManager = userManager;
        }

        /// <summary>
        /// Configure server with given parameters.
        /// </summary>
        /// <param name="instalationInfo">All data needed to perfom instalation.</param>
        /// <response code="200">Success.</response>
        /// <response code="403">Server is already configured.</response>
        /// <response code="400">Cannot connect to database.</response>
        /// <response code="409">Cannot create tables or cannot add administrator account.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult> Post([FromBody] InstalationInfo instalationInfo)
        {
            if (_customSettings.Configured)
                return Forbid();

            if(!TryConnectoToDatabase(instalationInfo.SQLServerConfiguration))
                return ValidationProblem(detail:"Cannot connect to database.");

            if (!GenerateDatabase(instalationInfo.SQLServerConfiguration))
                return Conflict("Cannot create tables.");

            if (!await CreateAdministratorAsync(instalationInfo.AdministratorUserCreationModel))
                return Conflict("Cannot create administrator account.");
            _customSettings.Configured = true;
            _customSettings.JwtSecurityKey = RandomJwtKey(128);
            _customSettings.SaveConfiguration();
            return Ok();
        }

        private bool TryConnectoToDatabase(SQLServerConfiguration sqlServerConfiguration)
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

            if (!DataAccess.Helper.TestConnection(sqlServerConfiguration.ConnectionString)) return false;
            _customSettings.ConnectionString = sqlServerConfiguration.ConnectionString;
            return true;
        }
    
        private bool GenerateDatabase(SQLServerConfiguration sqlServerConfiguration)
        {
            return true;
        }

        private async Task<bool> CreateAdministratorAsync(AdministratorUserCreationModel administratorUserCreationModel)
        {
            var newUser = new ApplicationUser { UserName = administratorUserCreationModel.Username, Email = administratorUserCreationModel.Email };
            var result = await _userManager.CreateAsync(newUser, administratorUserCreationModel.NewPassowrd);
            if (!result.Succeeded)
            {
                return false;
            }
            return true;
        }

        public static string RandomJwtKey(int length)
        {
            string allowed = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            return new string(allowed
                .OrderBy(o => Guid.NewGuid())
                .Take(length)
                .ToArray());
        }
    }
}
