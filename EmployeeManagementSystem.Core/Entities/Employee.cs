using EmployeeManagementSystem.Core.Enumerations;
using EmployeeManagementSystem.Core.Constants;
using EmployeeManagementSystem.Core.DomainModels;

namespace EmployeeManagementSystem.Core.Entities;

public class Employee : BaseEntity
{
    public Employee() { }
    private Employee(Guid id, string firstName, string lastName, int age, Sex sex) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Age = age;
        Sex = sex;
    }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public int Age { get; private set; }
    public Sex Sex { get; private set; }
    
    public static DataResult<Employee> Create(string firstName, string lastName, int? age, Sex? sex)
    {
        DataResult<Employee> result = new DataResult<Employee>();

        if (string.IsNullOrWhiteSpace(firstName))
        {
            result.Errors.Add(ErrorsConstants.FirstNameRequired);
        }

        if (string.IsNullOrWhiteSpace(lastName))
        {
            result.Errors.Add(ErrorsConstants.LastNameRequired);
        }

        if (!sex.HasValue)
        {
            result.Errors.Add(ErrorsConstants.SexRequired);
        }

        if (!age.HasValue || age < AgeConstants.MinAge || age > AgeConstants.MaxAge)
        {
            result.Errors.Add(ErrorsConstants.InvalidAge);
        }

        if (result.Succeed)
        {
            result.Data = new Employee(Guid.NewGuid(), firstName, lastName, age.Value, sex.Value);
        }

        return result;
    }

    public static DataResult<Employee> Update(Employee existingEmployee, string firstName, string lastName, int? age, Sex? sex)
    {
        DataResult<Employee> result = new DataResult<Employee>();

        if (string.IsNullOrWhiteSpace(firstName))
        {
            result.Errors.Add(ErrorsConstants.FirstNameRequired);
        }

        if (string.IsNullOrWhiteSpace(lastName))
        {
            result.Errors.Add(ErrorsConstants.LastNameRequired);
        }

        if (!sex.HasValue)
        {
            result.Errors.Add(ErrorsConstants.SexRequired);
        }

        if (!age.HasValue || age < AgeConstants.MinAge || age > AgeConstants.MaxAge)
        {
            result.Errors.Add(ErrorsConstants.InvalidAge);
        }

        if (result.Succeed)
        {
            existingEmployee.FirstName = firstName;
            existingEmployee.LastName = lastName;
            existingEmployee.Age = age.Value;
            existingEmployee.Sex = sex.Value;
            result.Data = existingEmployee;
        }

        return result;
    }
}