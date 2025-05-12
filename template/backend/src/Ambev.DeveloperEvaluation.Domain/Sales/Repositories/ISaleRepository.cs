using Ambev.DeveloperEvaluation.Domain.Sales.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Sales.Repositories;

public interface ISaleRepository
{
    Task AddAsync(Sale sale);
    Task<Sale?> GetByIdAsync(Guid id);
    Task<IEnumerable<Sale>> GetAllAsync();
    Task SaveChangesAsync();
    Task<IEnumerable<Sale>> GetByCustomerIdAsync(Guid customerId);
    Task<IEnumerable<Sale>> GetByBranchIdAsync(Guid branchId);
    Task UpdateAsync(Sale sale);

}
