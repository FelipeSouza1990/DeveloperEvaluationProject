using Ambev.DeveloperEvaluation.Domain.Sales.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    public class GetSaleByIdQueryHandler : IRequestHandler<GetSaleByIdQuery, GetSaleByIdResponse>
    {
        private readonly ISaleRepository _repository;

        public GetSaleByIdQueryHandler(ISaleRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetSaleByIdResponse> Handle(GetSaleByIdQuery request, CancellationToken cancellationToken)
        {
            var sale = await _repository.GetByIdAsync(request.Id);

            if (sale == null) return null;

            return new GetSaleByIdResponse
            {
                Id = sale.Id,
                SaleNumber = sale.SaleNumber,
                CustomerId = sale.CustomerId,
                CustomerName = sale.CustomerName,
                BranchId = sale.BranchId,
                BranchName = sale.BranchName,
                Date = sale.Date,
                IsCancelled = sale.IsCancelled,
                TotalAmount = sale.TotalAmount,
                Items = sale.Items.Select(i => new GetSaleByIdItemResponse
                {
                    Id = i.Id,
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    TotalAmount = i.TotalAmount
                }).ToList()
            };
        }
    }
}
