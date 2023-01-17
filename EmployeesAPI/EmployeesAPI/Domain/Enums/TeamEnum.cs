using System.ComponentModel;

namespace EmployeesAPI.Domain.Enums
{
    public enum TeamEnum
    {
        [Description("Backend")]
        BACKEND = 1,

        [Description("Frontend")]
        FRONTEND = 2,

        [Description("Mobile")]
        MOBILE = 3,
    }
}
