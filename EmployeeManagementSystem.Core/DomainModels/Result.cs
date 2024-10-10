using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EmployeeManagementSystem.Core.DomainModels;

public class Result
{
    public bool Succeed => !Errors.Any();
    public List<string> Errors { get; set; }

    public Result()
    {
        Errors = new List<string>();
    }

    public Result(string error)
    {
        Errors = new List<string> { error };
    }

    public Result(IEnumerable<string> errors)
    {
        Errors = errors.ToList();
    }
}