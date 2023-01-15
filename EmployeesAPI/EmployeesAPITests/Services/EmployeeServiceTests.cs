using AutoMapper;
using EmployeesAPI.Domain.Entities;
using EmployeesAPI.Domain.Repositories;
using EmployeesAPI.Mapping;
using EmployeesAPI.Services;
using Moq;

namespace EmployeesAPITests.Services
{
    public class EmployeeServiceTest
    {
        public Mock<IEmployeeRepository> employeeRepository = new Mock<IEmployeeRepository>();
        public IMapper? mapper;
        public Mock<IUnitOfWork> unitOfWork = new Mock<IUnitOfWork>();

        public EmployeeService employeeService;

        public EmployeeServiceTest()
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
        public async void ListEmployeesWithSuccess()
        {
            var employees = new List<Employee>
            {
                new Employee 
                { 
                    Id = 1, 
                    BirthDate = DateTime.Now, 
                    Cpf = "11111111111", 
                    Email = "email1@email.com", 
                    Gender = "Male", 
                    StartDate = "02/2019", 
                    Team = null 
                },
                new Employee
                {
                    Id = 2,
                    BirthDate = DateTime.Now,
                    Cpf = "22222222222",
                    Email = "email2@email.com",
                    Gender = "Male",
                    StartDate = "02/2019",
                    Team = "DEVOPS"
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
    }
}
