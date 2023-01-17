using EmployeesAPI.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace EmployeesAPI.Models
{
    public class SaveEmployeeModel
    {
        [Required(ErrorMessage = "The BirthDate is required")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "The Gender is required")]
        public GenderEnum Gender { get; set; }

        [Required(ErrorMessage = "The email is required")]
        [EmailAddress(ErrorMessage = "You need to inform a valid email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "The Cpf is required")]
        [RegularExpression("([0-9]{2}[\\.]?[0-9]{3}[\\.]?[0-9]{3}[\\/]?[0-9]{4}[-]?[0-9]{2})|([0-9]{3}[\\.]?[0-9]{3}[\\.]?[0-9]{3}[-]?[0-9]{2})", ErrorMessage = "You need to inform a valid CPF.")]
        public string? Cpf { get; set; }

        [Required(ErrorMessage = "The Start date is required")]
        [RegularExpression("(0?[1-9]|1[0-2])\\/(\\d{4})", ErrorMessage = "The start date must be in MM/YYYY format")]
        public string? StartDate { get; set; }

        public TeamEnum? Team { get; set; }
    }
}
