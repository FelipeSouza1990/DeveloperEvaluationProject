using Ambev.DeveloperEvaluation.Domain.Sales.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    public class CancelSaleCommandHandler : IRequestHandler<CancelSaleCommand, bool>
    {
        private readonly ISaleRepository _repository;

        public CancelSaleCommandHandler(ISaleRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(CancelSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _repository.GetByIdAsync(request.SaleId);
            if (sale == null)
                throw new InvalidOperationException("Sale not found");

            sale.Cancel();

            await _repository.SaveChangesAsync();

            return true;
        }
    }
}
