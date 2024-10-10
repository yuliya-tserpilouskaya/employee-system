namespace EmployeeManagementSystem.Core.DomainModels;

public class Result
{
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

    public bool Succeed => !Errors.Any();
    public List<string> Errors { get; set; }
}