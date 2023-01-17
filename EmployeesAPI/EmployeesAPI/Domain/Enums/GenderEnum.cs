using System.ComponentModel;

namespace EmployeesAPI.Domain.Enums
{
    public enum GenderEnum
    {
        [Description("Male")]
        MALE = 1,

        [Description("Female")]
        FEMALE = 2,

        [Description("Others")]
        OTHERS = 3,
    }
}
