using EmployeesAPI.Domain.Entities;
using EmployeesAPI.Models;

namespace EmployeesAPI.Domain.Repositories
{
    public interface IEmployeeRepository
    {
        public Task<IEnumerable<Employee>> ListAsync();
        Task AddAsync(Employee employee);
        Task<Employee?> FindByIdAsync(int id);
        void Update(Employee employee);
        void Remove(Employee employee);
    }
}