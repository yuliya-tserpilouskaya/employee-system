namespace EmployeeManagementSystem.Core.Constants;

public class ErrorsConstants
{
    public const string FirstNameRequired = "First name is required.";
    public const string LastNameRequired = "Last name is required.";
    public const string SexRequired = "Sex is required.";
    public static readonly string InvalidAge = $"Age must be between {AgeConstants.MinAge} and {AgeConstants.MaxAge}.";
}