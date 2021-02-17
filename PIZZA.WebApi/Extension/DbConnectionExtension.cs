using Microsoft.Extensions.DependencyInjection;
using PIZZA.DataAccess;
using PIZZA.DataAccess.ApplicationEmployeeDatabase;
using PIZZA.DataAccess.ApplicationRoleDatabase;
using PIZZA.DataAccess.ApplicationUserDatabase;
using PIZZA.DataAccess.TaskDatabase;

namespace PIZZA.WebApi.Extension
{
    public static class DbConnectionExtension
    {
        public static IServiceCollection AddDbConnection(this IServiceCollection serviceDescriptors, string connectionString)
        {
            serviceDescriptors.AddSingleton(
                (provider) =>                 
                    new DatabaseConnectionConfiguration(connectionString)
                );

            serviceDescriptors.AddTransient<ITaskRepository, TaskRepository>();
            serviceDescriptors.AddTransient<IApplicationUserRepository, ApplicationUserRepository>();
            serviceDescriptors.AddTransient<IApplicationRoleRepository, ApplicationRoleRepository>();
            serviceDescriptors.AddTransient<IApplicationEmployeeRepository, ApplicationEmployeeRepository>();
            return serviceDescriptors;
        }
    }
}
