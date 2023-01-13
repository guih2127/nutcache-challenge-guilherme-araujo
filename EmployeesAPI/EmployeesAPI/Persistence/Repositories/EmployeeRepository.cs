using EmployeesAPI.Domain.Entities;
using EmployeesAPI.Domain.Repositories;
using EmployeesAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EmployeesAPI.Persistence.Repositories
{
    public class EmployeeRepository : BaseRepository, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
        }

        public async Task<Employee?> FindByIdAsync(int id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task<IEnumerable<Employee>> ListAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public void Remove(Employee employee)
        {
            _context.Employees.Remove(employee);
        }

        public void Update(Employee employee)
        {
            _context.Employees.Update(employee);
        }
    }
}
