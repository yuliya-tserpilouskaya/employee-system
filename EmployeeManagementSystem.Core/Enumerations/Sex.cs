using System.ComponentModel;

namespace EmployeeManagementSystem.Core.Enumerations;

public enum Sex
{
    [Description("Male")]
    Male = 1,
    [Description("Female")]
    Female = 2,
    [Description("Other")]
    Other = 3,
    [Description("Prefer not to say")]
    PreferNotToSay = 4
}