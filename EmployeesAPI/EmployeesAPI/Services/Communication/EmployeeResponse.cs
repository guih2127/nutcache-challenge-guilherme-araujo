using EmployeesAPI.Models;

namespace EmployeesAPI.Services.Communication
{
    public class EmployeeResponse : BaseResponse
    {
        public EmployeeModel Employee { get; private set; }

        private EmployeeResponse(bool success, string message, EmployeeModel employee) : base(success, message)
        {
            Employee = employee;
        }

        public EmployeeResponse(EmployeeModel employee) : this(true, string.Empty, employee) { }

        public EmployeeResponse(string message) : this(false, message, null) { }
    }
}
