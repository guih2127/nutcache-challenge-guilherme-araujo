using EmployeesAPI.Models;
using EmployeesAPI.Services.Communication;

namespace EmployeesAPI.Domain
{
    public interface IEmployeeService
    {
        public Task<IEnumerable<EmployeeModel>> ListAsync();
        public Task<EmployeeResponse> SaveAsync(SaveEmployeeModel model);
        public Task<EmployeeResponse> UpdateAsync(int id, SaveEmployeeModel model);
        public Task<EmployeeResponse> DeleteAsync(int id);
    }
}
