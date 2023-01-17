using EmployeesAPI.Domain.Enums;

namespace EmployeesAPI.Domain.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public DateTime BirthDate { get; set; }
        public GenderEnum Gender { get; set; }
        public string? Email { get; set; }
        public string? Cpf { get; set; }
        public string? StartDate { get; set; }
        public TeamEnum? Team { get; set; }
    }
}