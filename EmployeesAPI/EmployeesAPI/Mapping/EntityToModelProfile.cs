using AutoMapper;
using EmployeesAPI.Domain.Entities;
using EmployeesAPI.Models;

namespace EmployeesAPI.Mapping
{
    public class EntityToModelProfile : Profile
    {
        public EntityToModelProfile()
        {
            CreateMap<Employee, EmployeeModel>();
        }
    }
}
