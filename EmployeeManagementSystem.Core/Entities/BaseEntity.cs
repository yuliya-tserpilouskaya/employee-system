using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Core.Entities;

public abstract class BaseEntity
{
    protected BaseEntity(Guid id)
    {
        Id = id;
    }

    [Key]
    public virtual Guid Id { get; init; }
}