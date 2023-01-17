using AutoMapper;
using EmployeesAPI.Controllers;
using EmployeesAPI.Domain;
using EmployeesAPI.Domain.Entities;
using EmployeesAPI.Domain.Enums;
using EmployeesAPI.Mapping;
using EmployeesAPI.Models;
using EmployeesAPI.Services.Communication;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using static EmployeesAPITests.Utils;

namespace EmployeesAPITests.Controllers
{
    public class EmployeeControllerTests
    {
        public Mock<IEmployeeService> employeeService = new Mock<IEmployeeService>();
        public EmployeeController employeeController;

        public EmployeeControllerTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<EntityToModelProfile>();
                cfg.AddProfile<ModelToEntityProfile>();
            });
            var mapper = config.CreateMapper();

            employeeController = new EmployeeController(employeeService.Object);
        }

        [Fact]
        public async void Get_Should_ListEmployees_When_ServiceReturnsEmployees()
        {
            var employees = new List<EmployeeModel>
            {
                new EmployeeModel
                {
                    Id = 1,
                    BirthDate = DateTime.Now,
                    Cpf = "11111111111",
                    Email = "email1@email.com",
                    Gender = GenderEnum.MALE,
                    StartDate = "02/2019",
                    Team = null
                },
                new EmployeeModel
                {
                    Id = 2,
                    BirthDate = DateTime.Now,
                    Cpf = "22222222222",
                    Email = "email2@email.com",
                    Gender = GenderEnum.MALE,
                    StartDate = "02/2019",
                    Team = TeamEnum.MOBILE
                },
            };

            employeeService.Setup(s => s.ListAsync()).ReturnsAsync(employees);

            var result = await employeeController.GetAllAsync();
            var employeesResult = result.ToList();

            Assert.Equal(employees[0].Id, employeesResult[0].Id);
            Assert.Equal(employees[0].BirthDate, employeesResult[0].BirthDate);
            Assert.Equal(employees[0].Cpf, employeesResult[0].Cpf);
            Assert.Equal(employees[0].Gender, employeesResult[0].Gender);
            Assert.Equal(employees[0].StartDate, employeesResult[0].StartDate);
            Assert.Equal(employees[0].Team, employeesResult[0].Team);

            Assert.Equal(employees[1].Id, employeesResult[1].Id);
            Assert.Equal(employees[1].BirthDate, employeesResult[1].BirthDate);
            Assert.Equal(employees[1].Cpf, employeesResult[1].Cpf);
            Assert.Equal(employees[1].Gender, employeesResult[1].Gender);
            Assert.Equal(employees[1].StartDate, employeesResult[1].StartDate);
            Assert.Equal(employees[1].Team, employeesResult[1].Team);

            employeeService.Verify(s => s.ListAsync());
            employeeService.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Post_Should_InsertAndReturnEmployee_When_ValidEmployeeModel()
        {
            var birthDate = DateTime.Now;

            var employeeToSave = new SaveEmployeeModel
            {
                BirthDate = birthDate,
                Cpf = "11111111111",
                Email = "email1@email.com",
                Gender = GenderEnum.MALE,
                StartDate = "02/2019",
                Team = null
            };

            var employee = new EmployeeModel
            {
                Id = 1,
                BirthDate = birthDate,
                Cpf = "11111111111",
                Email = "email1@email.com",
                Gender = GenderEnum.MALE,
                StartDate = "02/2019",
                Team = null
            };

            var employeeResponse = new EmployeeResponse(employee, HttpStatusCode.OK);

            employeeService.Setup(s => s.SaveAsync(It.IsAny<SaveEmployeeModel>())).ReturnsAsync(employeeResponse);

            ValidateModelForTests(employeeToSave, employeeController);
            var result = await employeeController.PostAsync(employeeToSave) as ObjectResult;
            var employeeReturned = result?.Value as EmployeeModel;

            Assert.True(employeeController.ModelState.IsValid);
            Assert.Equal((int)HttpStatusCode.OK, result?.StatusCode);
            Assert.NotNull(employeeReturned);
            Assert.Equal(employeeToSave.BirthDate, employeeReturned.BirthDate);
            Assert.Equal(employeeToSave.Cpf, employeeReturned.Cpf);
            Assert.Equal(employeeToSave.Gender, employeeReturned.Gender);
            Assert.Equal(employeeToSave.StartDate, employeeReturned.StartDate);
            Assert.Equal(employeeToSave.Team, employeeReturned.Team);

            employeeService.Verify(s => s.SaveAsync(It.IsAny<SaveEmployeeModel>()));
            employeeService.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Post_Should_ReturnBadRequest_When_InvalidStartDate()
        {
            var birthDate = DateTime.Now;

            var employeeToSave = new SaveEmployeeModel
            {
                BirthDate = birthDate,
                Cpf = "11111111111",
                Email = "email1@email.com",
                Gender = GenderEnum.MALE,
                StartDate = "22/208962",
                Team = null
            };

            ValidateModelForTests(employeeToSave, employeeController);
            var result = await employeeController.PostAsync(employeeToSave) as ObjectResult;

            Assert.False(employeeController.ModelState.IsValid);
            Assert.Equal((int)HttpStatusCode.BadRequest, result?.StatusCode);
        }

        [Fact]
        public async void Post_Should_ReturnBadRequest_When_InvalidEmail()
        {
            var birthDate = DateTime.Now;

            var employeeToSave = new SaveEmployeeModel
            {
                BirthDate = birthDate,
                Cpf = "11111111111",
                Email = "emailInvalid",
                Gender = GenderEnum.MALE,
                StartDate = "08/2012",
                Team = null
            };

            ValidateModelForTests(employeeToSave, employeeController);
            var result = await employeeController.PostAsync(employeeToSave) as ObjectResult;

            Assert.False(employeeController.ModelState.IsValid);
            Assert.Equal((int)HttpStatusCode.BadRequest, result?.StatusCode);
        }

        [Fact]
        public async void Put_Should_UpdateAndReturnEmployee_When_ValidEmployeeModel()
        {
            var id = 1;
            var birthDate = DateTime.Now;
            var newEmployeeDataModel = new SaveEmployeeModel
            {
                BirthDate = birthDate,
                Cpf = "22222222222",
                Email = "email2@email.com",
                Gender = GenderEnum.MALE,
                StartDate = "02/2019",
                Team = null
            };

            var updatedEmployee = new EmployeeModel
            {
                Id = 1,
                BirthDate = birthDate,
                Cpf = "22222222222",
                Email = "email2@email.com",
                Gender = GenderEnum.MALE,
                StartDate = "02/2019",
                Team = null
            };

            var employeeResponse = new EmployeeResponse(updatedEmployee, HttpStatusCode.OK);

            employeeService.Setup(s => s.UpdateAsync(id, It.IsAny<SaveEmployeeModel>())).ReturnsAsync(employeeResponse);

            ValidateModelForTests(newEmployeeDataModel, employeeController);
            var result = await employeeController.PutAsync(id, newEmployeeDataModel) as ObjectResult;
            var employeeReturned = result?.Value as EmployeeModel;

            Assert.True(employeeController.ModelState.IsValid);
            Assert.Equal((int)HttpStatusCode.OK, result?.StatusCode);
            Assert.NotNull(employeeReturned);
            Assert.Equal(newEmployeeDataModel.BirthDate, employeeReturned.BirthDate);
            Assert.Equal(newEmployeeDataModel.Cpf, employeeReturned.Cpf);
            Assert.Equal(newEmployeeDataModel.Gender, employeeReturned.Gender);
            Assert.Equal(newEmployeeDataModel.StartDate, employeeReturned.StartDate);
            Assert.Equal(newEmployeeDataModel.Team, employeeReturned.Team);

            employeeService.Verify(s => s.UpdateAsync(id, It.IsAny<SaveEmployeeModel>()));
            employeeService.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Put_Should_ReturnBadRequest_When_InvalidStartDate()
        {
            var birthDate = DateTime.Now;
            var id = 1;

            var employeeToSave = new SaveEmployeeModel
            {
                BirthDate = birthDate,
                Cpf = "11111111111",
                Email = "email1@email.com",
                Gender = GenderEnum.MALE,
                StartDate = "22/208962",
                Team = null
            };

            ValidateModelForTests(employeeToSave, employeeController);
            var result = await employeeController.PutAsync(id, employeeToSave) as ObjectResult;

            Assert.False(employeeController.ModelState.IsValid);
            Assert.Equal((int)HttpStatusCode.BadRequest, result?.StatusCode);
        }

        [Fact]
        public async void Put_Should_ReturnBadRequest_When_InvalidEmail()
        {
            var birthDate = DateTime.Now;
            var id = 1;

            var employeeToSave = new SaveEmployeeModel
            {
                BirthDate = birthDate,
                Cpf = "11111111111",
                Email = "emailInvalid",
                Gender = GenderEnum.MALE,
                StartDate = "08/2012",
                Team = null
            };

            ValidateModelForTests(employeeToSave, employeeController);
            var result = await employeeController.PutAsync(id, employeeToSave) as ObjectResult;

            Assert.False(employeeController.ModelState.IsValid);
            Assert.Equal((int)HttpStatusCode.BadRequest, result?.StatusCode);
        }

        [Fact]
        public async void Delete_Should_DeleteAndReturnEmployee_When_ValidId()
        {
            var id = 1;
            var deletedEmployee = new EmployeeModel
            {
                Id = 1,
                BirthDate = DateTime.Now,
                Cpf = "22222222222",
                Email = "email2@email.com",
                Gender = GenderEnum.MALE,
                StartDate = "02/2019",
                Team = null
            };

            var employeeResponse = new EmployeeResponse(deletedEmployee, HttpStatusCode.OK);

            employeeService.Setup(s => s.DeleteAsync(id)).ReturnsAsync(employeeResponse);

            var result = await employeeController.DeleteAsync(id) as ObjectResult;
            var employeeReturned = result?.Value as EmployeeModel;

            Assert.True(employeeController.ModelState.IsValid);
            Assert.Equal((int)HttpStatusCode.OK, result?.StatusCode);
            Assert.NotNull(employeeReturned);
            Assert.Equal(deletedEmployee.Id, employeeReturned.Id);
            Assert.Equal(deletedEmployee.BirthDate, employeeReturned.BirthDate);
            Assert.Equal(deletedEmployee.Cpf, employeeReturned.Cpf);
            Assert.Equal(deletedEmployee.Gender, employeeReturned.Gender);
            Assert.Equal(deletedEmployee.StartDate, employeeReturned.StartDate);
            Assert.Equal(deletedEmployee.Team, employeeReturned.Team);

            employeeService.Verify(s => s.DeleteAsync(id));
            employeeService.VerifyNoOtherCalls();
        }
    }
}
