using EmployeesAPI.Models;
using System.Net;

namespace EmployeesAPI.Services.Communication
{
    public class EmployeeResponse : BaseResponse
    {
        public EmployeeModel Employee { get; private set; }

        private EmployeeResponse(bool success, string message, HttpStatusCode statusCode, EmployeeModel employee) : base(success, message, statusCode)
        {
            Employee = employee;
        }

        public EmployeeResponse(EmployeeModel employee, HttpStatusCode statusCode) : this(true, string.Empty, statusCode, employee) { }

        public EmployeeResponse(string message, HttpStatusCode statusCode) : this(false, message, statusCode, null) { }
    }
}
