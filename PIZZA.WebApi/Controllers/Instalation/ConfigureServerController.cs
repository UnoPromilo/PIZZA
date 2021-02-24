using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PIZZA.DataAccess;
using PIZZA.Enums;
using PIZZA.Models.Database;
using PIZZA.Models.Instalation;
using PIZZA.Models.Task;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
        private string sqlConnectionString;
        public ConfigureServerController(CustomSettings customSettings, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, DatabaseConnectionConfiguration databaseConnectionConfiguration)
        {
            sqlConnectionString = databaseConnectionConfiguration.SqlConnectionString;

            Helper.OnConnectionStringAcctualization += (sender, connectionString) =>
            {
                sqlConnectionString = connectionString;
            };
            
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

            if (instalationInfo.FillDatabse.FillDatabaseWithSampleData)
            {
                await FillDatabase();
            }

            _customSettings.Configured = true;
            _customSettings.JwtSecurityKey = RandomJwtKey(128);
            _customSettings.SaveConfiguration();
            return Ok();
        }

        protected IDbConnection DbConnection
        {
            get
            {
                return new SqlConnection(sqlConnectionString);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            await FillDatabase();
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

        private async Task FillDatabase()
        {
            DataGenerator dataGenerator = new DataGenerator();

            int errors = 0;

            //Adding users
            IEnumerable<ApplicationUser> users = dataGenerator.GenerateRandomApplicationUsers(1000);
            List<Task<IdentityResult>> addingUsers = new();
            foreach(var user in users)
                addingUsers.Add(_userManager.CreateAsync(user));

            await Task.WhenAll(addingUsers);

            foreach (var task in addingUsers)
                errors += (await task).Succeeded?0:1;


            //Adding admin roles
            IEnumerable<ApplicationUser> admins = dataGenerator.GetRandomUsers(10, users);
            List<Task<IdentityResult>> addingAdminRoleTasks = new();
            foreach (var user in admins)
                addingAdminRoleTasks.Add(_userManager.AddToRoleAsync(user, "Admin"));

            await Task.WhenAll(addingAdminRoleTasks);

            foreach (var task in addingAdminRoleTasks)
                errors += (await task).Succeeded ? 0 : 1;


            //Adding manager roles
            IEnumerable<ApplicationUser> managers = dataGenerator.GetRandomUsers(50, users); ;
            List<Task<IdentityResult>> addingManagerRoleTasks = new();
            foreach (var user in managers)
                addingManagerRoleTasks.Add(_userManager.AddToRoleAsync(user, "Manager"));

            await Task.WhenAll(addingManagerRoleTasks);

            foreach (var task in addingManagerRoleTasks)
                errors += (await task).Succeeded ? 0 : 1;

            //Adding Tasks
            IEnumerable<TaskModelWithActualStateAndCreator> taskModels = dataGenerator.GenerateRandomTasks(1200, managers);
            using (var cnn = DbConnection)
            {
                foreach (var task in taskModels)
                {
                    try
                    {
                        var procedure = "[CreateTask]";
                        var values = new
                        {
                            Creator = task.TaskCreator,
                            task.Deadline,
                            task.Priority,
                            task.Name,
                            task.Description,
                            Note = ""
                        };
                        var taskToDo = cnn.QueryFirstOrDefaultAsync<int>(procedure, values, commandType: CommandType.StoredProcedure);
                        _ = taskToDo.ContinueWith(async (Task<int> baseTask) => { task.ID = await baseTask; });
                        await taskToDo;
                    }
                    catch { }
                }

            }

            //Adding employees to task
            IEnumerable<EmployeeTask> employeeTaskModels = dataGenerator.GenerateEmployeeTask(taskModels, users, 0, 5);
            using (var cnn = DbConnection)
            {
                foreach (var task in employeeTaskModels)
                {                  
                    try
                    {
                        var procedure = "[AddUserToTask]";
                        var values = new
                        {
                            Employee = task.Employee.ID,
                            Task = task.Task.ID,
                            Role = (int)task.TaskRole
                        };
                        var taskToDo = cnn.ExecuteAsync(procedure, values, commandType: CommandType.StoredProcedure);
                        await taskToDo;
                    }
                    catch { }
                }
            }

            //Adding history to task
            IEnumerable<TaskStateModel> tashHistory = dataGenerator.GenerateTaskStateHistory(taskModels, employeeTaskModels);
            using (var cnn = DbConnection)
            {
                foreach (var task in tashHistory)
                {
                    try
                    {
                        var procedure = "[AddTaskState]";
                        var values = new
                        {
                            task.Task,
                            task.NewTaskState,
                            task.DateTime,
                            task.Editor,
                            Note = ""
                        };
                        var taskToDo = cnn.ExecuteAsync(procedure, values, commandType: CommandType.StoredProcedure);
                        await taskToDo;
                    }
                    catch { }
                }             
            }

            //
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
