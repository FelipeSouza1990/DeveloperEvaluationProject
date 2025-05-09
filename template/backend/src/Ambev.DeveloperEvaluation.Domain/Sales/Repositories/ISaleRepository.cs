using Ambev.DeveloperEvaluation.Domain.Sales.Entities;
using System;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Sales.Repositories
{
    public interface ISaleRepository
    {
        Task AddAsync(Sale sale);
        //Implementar posteriormente os métodos de GetById, Update, Delete entre outros
    }
}
