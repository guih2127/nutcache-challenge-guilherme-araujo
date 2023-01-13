using AutoMapper;
using EmployeesAPI.Domain.Entities;
using EmployeesAPI.Models;

namespace EmployeesAPI.Mapping
{
    public class ModelToEntityProfile : Profile
    {
        public ModelToEntityProfile() 
        {
            CreateMap<EmployeeModel, Employee>();
            CreateMap<SaveEmployeeModel, Employee>();
        }
    }
}
