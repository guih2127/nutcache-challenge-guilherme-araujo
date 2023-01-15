using System.ComponentModel.DataAnnotations;

namespace EmployeesAPI.Models
{
    public class SaveEmployeeModel
    {
        [Required(ErrorMessage = "The BirthDate is required")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "The Gender is required")]
        public string? Gender { get; set; }

        [Required(ErrorMessage = "The email is required")]
        [EmailAddress(ErrorMessage = "You need to inform a valid email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "The Cpf is required")]
        public string? Cpf { get; set; }

        [Required(ErrorMessage = "The Start date is required")]
        [RegularExpression("^((0[1-9])|(1[0-2]))\\/((2009)|(20[1-2][0-9]))$", ErrorMessage = "The start date must be in MM/YYYY format")]
        public string? StartDate { get; set; }

        public string? Team { get; set; }
    }
}
