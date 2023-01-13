using AutoMapper;
using EmployeesAPI.Domain;
using EmployeesAPI.Domain.Entities;
using EmployeesAPI.Domain.Repositories;
using EmployeesAPI.Models;
using EmployeesAPI.Services.Communication;

namespace EmployeesAPI.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<EmployeeResponse> DeleteAsync(int id)
        {
            try
            {
                var existingEmployee = await _employeeRepository.FindByIdAsync(id);
                if (existingEmployee == null)
                    return new EmployeeResponse("Employee not found.");

                _employeeRepository.Remove(existingEmployee);
                await _unitOfWork.CompleteAsync();
                return new EmployeeResponse(_mapper.Map<EmployeeModel>(existingEmployee));
            }
            catch (Exception ex)
            {
                return new EmployeeResponse($"An error occurred when deleting the employee: {ex.Message}");
            }
        }

        public async Task<EmployeeResponse> SaveAsync(SaveEmployeeModel model)
        {
            try
            {
                var entityToInsert = _mapper.Map<Employee>(model);
                await _employeeRepository.AddAsync(entityToInsert);
                await _unitOfWork.CompleteAsync();

                return new EmployeeResponse(_mapper.Map<EmployeeModel>(entityToInsert));
            }
            catch (Exception ex)
            {
                return new EmployeeResponse($"An error occurred when saving the employee: {ex.Message}");
            }
        }

        public async Task<IEnumerable<EmployeeModel>> ListAsync()
        {
            var employees = await _employeeRepository.ListAsync();
            var models = _mapper.Map<IEnumerable<EmployeeModel>>(employees);

            return models;
        }

        public async Task<EmployeeResponse> UpdateAsync(int id, SaveEmployeeModel model)
        {
            try
            {
                var existingEmployee = await _employeeRepository.FindByIdAsync(id);
                if (existingEmployee == null)
                    return new EmployeeResponse("Employee not found.");

                existingEmployee.Cpf = model.Cpf;
                existingEmployee.BirthDate = model.BirthDate;
                existingEmployee.StartDate = model.StartDate;
                existingEmployee.Email = model.Email;
                existingEmployee.Gender = model.Gender;
                existingEmployee.Team = model.Team;

                _employeeRepository.Update(existingEmployee);
                await _unitOfWork.CompleteAsync();

                return new EmployeeResponse(_mapper.Map<EmployeeModel>(existingEmployee));
            }
            catch (Exception ex)
            {
                return new EmployeeResponse($"An error occurred when updating the employee: {ex.Message}");
            }
        }
    }
}
