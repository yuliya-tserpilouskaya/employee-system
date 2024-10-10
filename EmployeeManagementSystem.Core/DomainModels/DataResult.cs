namespace EmployeeManagementSystem.Core.DomainModels;

public class DataResult<T> : Result
{
    public T Data { get; set; }
}