using Ambev.DeveloperEvaluation.Domain.Sales.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.ORM.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, Guid>
    {
        private readonly ISaleRepository _repository;

        public CreateSaleHandler(ISaleRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = new Sale(
                request.SaleNumber,
                request.CustomerId,
                request.CustomerName,
                request.BranchId,
                request.BranchName
            );

            foreach (var item in request.Items)
            {
                sale.AddItem(item.ProductId, item.ProductName, item.Quantity, item.UnitPrice);
            }

            await _repository.AddAsync(sale);
            return sale.Id;
        }
    }
}
