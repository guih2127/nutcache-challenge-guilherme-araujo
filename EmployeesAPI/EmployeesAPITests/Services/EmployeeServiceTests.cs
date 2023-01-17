using AutoMapper;
using EmployeesAPI.Domain.Entities;
using EmployeesAPI.Domain.Enums;
using EmployeesAPI.Domain.Repositories;
using EmployeesAPI.Mapping;
using EmployeesAPI.Models;
using EmployeesAPI.Services;
using Moq;

namespace EmployeesAPITests.Services
{
    public class EmployeeServiceTests
    {
        public Mock<IEmployeeRepository> employeeRepository = new Mock<IEmployeeRepository>();
        public IMapper? mapper;
        public Mock<IUnitOfWork> unitOfWork = new Mock<IUnitOfWork>();

        public EmployeeService employeeService;

        public EmployeeServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ModelToEntityProfile>();
                cfg.AddProfile<EntityToModelProfile>();
            });
            var mapper = config.CreateMapper();

            employeeService = new EmployeeService(employeeRepository.Object, mapper, unitOfWork.Object);
        }

        [Fact]
        public async void List_Should_ListEmployees_When_EmployeesExistOnDatabase()
        {
            var birthDate = DateTime.Now;
            var employees = new List<Employee>
            {
                new Employee 
                { 
                    Id = 1, 
                    BirthDate = birthDate,
                    Cpf = "11111111111", 
                    Email = "email1@email.com", 
                    Gender = GenderEnum.MALE, 
                    StartDate = "02/2019", 
                    Team = null 
                },
                new Employee
                {
                    Id = 2,
                    BirthDate = birthDate,
                    Cpf = "22222222222",
                    Email = "email2@email.com",
                    Gender = GenderEnum.MALE,
                    StartDate = "02/2019",
                    Team = TeamEnum.MOBILE
                },
            };


            employeeRepository.Setup(s => s.ListAsync()).ReturnsAsync(employees);
            var result = await employeeService.ListAsync();
            var employeesModels = result.ToList();

            Assert.Equal(employees.Count, employeesModels.Count);
            
            Assert.Equal(employees[0].Id, employeesModels[0].Id);
            Assert.Equal(employees[0].BirthDate, employeesModels[0].BirthDate);
            Assert.Equal(employees[0].Cpf, employeesModels[0].Cpf);
            Assert.Equal(employees[0].Gender, employeesModels[0].Gender);
            Assert.Equal(employees[0].StartDate, employeesModels[0].StartDate);
            Assert.Equal(employees[0].Team, employeesModels[0].Team);

            Assert.Equal(employees[1].Id, employeesModels[1].Id);
            Assert.Equal(employees[1].BirthDate, employeesModels[1].BirthDate);
            Assert.Equal(employees[1].Cpf, employeesModels[1].Cpf);
            Assert.Equal(employees[1].Gender, employeesModels[1].Gender);
            Assert.Equal(employees[1].StartDate, employeesModels[1].StartDate);
            Assert.Equal(employees[1].Team, employeesModels[1].Team);

            employeeRepository.Verify(s => s.ListAsync());
            employeeRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Insert_Should_InsertAndReturnEmployee_When_ValidEmployeeModel()
        {
            var employeeModel = new SaveEmployeeModel
            {
                BirthDate = DateTime.Now,
                Cpf = "22222222222",
                Email = "email2@email.com",
                Gender = GenderEnum.MALE,
                StartDate = "02/2019",
                Team = TeamEnum.MOBILE
            };

            var result = await employeeService.SaveAsync(employeeModel);

            employeeRepository.Verify(s => s.AddAsync(It.IsAny<Employee>()));
            employeeRepository.VerifyNoOtherCalls();
            unitOfWork.Verify(s => s.CompleteAsync());
            unitOfWork.VerifyNoOtherCalls();

            Assert.Equal(result.Employee.BirthDate, employeeModel.BirthDate);
            Assert.Equal(result.Employee.Cpf, employeeModel.Cpf);
            Assert.Equal(result.Employee.Email, employeeModel.Email);
            Assert.Equal(result.Employee.Gender, employeeModel.Gender);
            Assert.Equal(result.Employee.StartDate, employeeModel.StartDate);
            Assert.Equal(result.Employee.Team, employeeModel.Team);

            Assert.True(result.Success);
        }

        [Fact]
        public async void Update_Should_UpdateAndReturnEmployee_When_ValidEmployeeModelAndValidId()
        {
            var id = 1;
            var birthDate = DateTime.Now;
            var existentEmployee = new Employee 
            {
                Id = 1,
                BirthDate = birthDate,
                Cpf = "11111111111",
                Email = "email1@email.com",
                Gender = GenderEnum.MALE,
                StartDate = "02/2019",
                Team = TeamEnum.MOBILE
            };

            var employeeModel = new SaveEmployeeModel
            {
                BirthDate = birthDate,
                Cpf = "22222222222",
                Email = "email2@email.com",
                Gender = GenderEnum.MALE,
                StartDate = "02/2019",
                Team = TeamEnum.MOBILE
            };

            employeeRepository.Setup(s => s.FindByIdAsync(id)).ReturnsAsync(existentEmployee);
            var result = await employeeService.UpdateAsync(id, employeeModel);

            employeeRepository.Verify(s => s.FindByIdAsync(id));
            employeeRepository.Verify(s => s.Update(It.IsAny<Employee>()));
            employeeRepository.VerifyNoOtherCalls();
            unitOfWork.Verify(s => s.CompleteAsync());
            unitOfWork.VerifyNoOtherCalls();

            Assert.Equal(result.Employee.BirthDate, employeeModel.BirthDate);
            Assert.Equal(result.Employee.Cpf, employeeModel.Cpf);
            Assert.Equal(result.Employee.Email, employeeModel.Email);
            Assert.Equal(result.Employee.Gender, employeeModel.Gender);
            Assert.Equal(result.Employee.StartDate, employeeModel.StartDate);
            Assert.Equal(result.Employee.Team, employeeModel.Team);

            Assert.True(result.Success);
        }


        [Fact]
        public async void Update_Should_NotReturnSuccess_When_NotExistentId()
        {
            var id = 1;
            var employeeModel = new SaveEmployeeModel
            {
                BirthDate = DateTime.Now,
                Cpf = "22222222222",
                Email = "email2@email.com",
                Gender = GenderEnum.MALE,
                StartDate = "02/2019",
                Team = TeamEnum.MOBILE
            };

            employeeRepository.Setup(s => s.FindByIdAsync(id)).ReturnsAsync(() => null);
            var result = await employeeService.UpdateAsync(id, employeeModel);

            employeeRepository.Verify(s => s.FindByIdAsync(id));
            employeeRepository.VerifyNoOtherCalls();

            Assert.False(result.Success);
        }

        [Fact]
        public async void Delete_Should_DeleteAndReturnEmployee_When_ValidId()
        {
            var id = 1;
            var existentEmployee = new Employee
            {
                Id = 1,
                BirthDate = DateTime.Now,
                Cpf = "11111111111",
                Email = "email1@email.com",
                Gender = GenderEnum.MALE,
                StartDate = "02/2019",
                Team = TeamEnum.MOBILE
            };

            employeeRepository.Setup(s => s.FindByIdAsync(id)).ReturnsAsync(existentEmployee);
            var result = await employeeService.DeleteAsync(id);

            employeeRepository.Verify(s => s.FindByIdAsync(id));
            employeeRepository.Verify(s => s.Remove(It.IsAny<Employee>()));
            employeeRepository.VerifyNoOtherCalls();
            unitOfWork.Verify(s => s.CompleteAsync());
            unitOfWork.VerifyNoOtherCalls();

            Assert.Equal(result.Employee.BirthDate, existentEmployee.BirthDate);
            Assert.Equal(result.Employee.Cpf, existentEmployee.Cpf);
            Assert.Equal(result.Employee.Email, existentEmployee.Email);
            Assert.Equal(result.Employee.Gender, existentEmployee.Gender);
            Assert.Equal(result.Employee.StartDate, existentEmployee.StartDate);
            Assert.Equal(result.Employee.Team, existentEmployee.Team);

            Assert.True(result.Success);
        }

        [Fact]
        public async void Delete_Should_NotReturnSuccess_When_NotExistentId()
        {
            var id = 1;

            employeeRepository.Setup(s => s.FindByIdAsync(id)).ReturnsAsync(() => null);
            var result = await employeeService.DeleteAsync(id);

            employeeRepository.Verify(s => s.FindByIdAsync(id));
            employeeRepository.VerifyNoOtherCalls();

            Assert.False(result.Success);
        }
    }
}
