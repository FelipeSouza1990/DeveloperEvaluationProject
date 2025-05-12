using Ambev.DeveloperEvaluation.Domain.Sales.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleCommandHandler : IRequestHandler<UpdateSaleCommand, Unit>
    {
        private readonly ISaleRepository _repository;

        public UpdateSaleCommandHandler(ISaleRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _repository.GetByIdAsync(request.SaleId);

            if (sale == null || sale.IsCancelled)
                throw new Exception("Sale not found or is cancelled");

            sale.Update(
                request.SaleNumber,
                request.CustomerId,
                request.CustomerName,
                request.BranchId,
                request.BranchName,
                request.Items.Select(i => (i.ProductId, i.ProductName, i.Quantity, i.UnitPrice)).ToList()
            );

            await _repository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
