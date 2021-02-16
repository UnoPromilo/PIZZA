﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PIZZA.Enums;
using PIZZA.Models.Database;
using PIZZA.Models.Instalation;
using System;
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
        private readonly RoleManager<ApplicationRole> _roleManager;
        public ConfigureServerController(CustomSettings customSettings, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _customSettings = customSettings;
            _userManager = userManager;
            _roleManager = roleManager;
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

            if (instalationInfo.FillDatabse.FillDatabaseWithObjects)
            {
                if (!await GenerateDatabase(instalationInfo.SQLServerConfiguration))
                    return Conflict("Cannot create tables.");
            }
            else if (instalationInfo.FillDatabse.ClearDatabaseData)
                if (!await ClearDatabase(instalationInfo.SQLServerConfiguration))
                    return Conflict("Cannot clear database.");

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
            DataAccess.Helper.ChangeConnectionString(sqlServerConfiguration.ConnectionString);
            return true;
        }
    
        private async Task<bool> GenerateDatabase(SQLServerConfiguration sqlServerConfiguration)
        {
            return await DataAccess.Helper.CreateDatabaseModelAsync(sqlServerConfiguration.ConnectionString);
        }
        private async Task<bool> ClearDatabase(SQLServerConfiguration sqlServerConfiguration)
        {
            return await DataAccess.Helper.ClearDatabaseDataAsync(sqlServerConfiguration.ConnectionString);
        }

        private async Task<bool> CreateAdministratorAsync(AdministratorUserCreationModel administratorUserCreationModel)
        {
            ApplicationRole roleAdmin = new ();
            roleAdmin.Name = "Admin";
            await _roleManager.CreateAsync(roleAdmin);

            ApplicationRole roleManager = new();
            roleManager.Name = "Manager";
            await _roleManager.CreateAsync(roleManager);

            var newUser = new ApplicationUser { UserName = administratorUserCreationModel.Username, Email = administratorUserCreationModel.Email };
            var result = await _userManager.CreateAsync(newUser, administratorUserCreationModel.NewPassowrd);
            if (!result.Succeeded)
            {
                return false;
            }
            result = await _userManager.AddToRoleAsync(newUser, "Admin");
            result = await _userManager.AddToRoleAsync(newUser, "Manager");

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
