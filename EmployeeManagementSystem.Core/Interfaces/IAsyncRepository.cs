using EmployeeManagementSystem.Core.Entities;
using System.Linq.Expressions;

namespace EmployeeManagementSystem.Core.Interfaces;

public interface IAsyncRepository<T> where T : BaseEntity
{
    Task<IReadOnlyList<T>> ListAllAsync();
    Task<IReadOnlyList<T>> ListByCriteriaAsync(Expression<Func<T, bool>> criteria);
    Task<T> GetByIdAsync(Guid id);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(IEnumerable<T> entities);
}