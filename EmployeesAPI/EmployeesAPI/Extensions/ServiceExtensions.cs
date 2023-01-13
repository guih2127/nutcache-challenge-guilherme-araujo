using EmployeesAPI.Domain;
using EmployeesAPI.Domain.Repositories;
using EmployeesAPI.Persistence.Repositories;
using EmployeesAPI.Services;

namespace EmployeesAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void RegisterRepositories(this IServiceCollection collection)
        {
            collection.AddScoped<IEmployeeRepository, EmployeeRepository>();
        }

        public static void RegisterServices(this IServiceCollection collection)
        {
            collection.AddScoped<IUnitOfWork, UnitOfWork>();
            collection.AddScoped<IEmployeeService, EmployeeService>();
        }
    }
}
