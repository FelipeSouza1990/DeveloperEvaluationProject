using Ambev.DeveloperEvaluation.Application.Sales.GetCancelledSales;
using Ambev.DeveloperEvaluation.Domain.Sales.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetCancelledSales
{
    public class GetCancelledSalesQueryHandler : IRequestHandler<GetCancelledSalesQuery, List<GetCancelledSalesDto>>
    {
        private readonly ISaleRepository _repository;

        public GetCancelledSalesQueryHandler(ISaleRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetCancelledSalesDto>> Handle(GetCancelledSalesQuery request, CancellationToken cancellationToken)
        {
            var sales = await _repository.GetAllAsync();
            return sales
                .Where(s => s.IsCancelled)
                .Select(s => new GetCancelledSalesDto
                {
                    Id = s.Id,
                    SaleNumber = s.SaleNumber,
                    Date = s.Date,
                    CustomerName = s.CustomerName,
                    BranchName = s.BranchName,
                    TotalAmount = s.TotalAmount
                })
                .ToList();
        }
    }
}
