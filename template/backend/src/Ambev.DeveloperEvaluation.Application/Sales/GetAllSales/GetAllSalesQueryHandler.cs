using Ambev.DeveloperEvaluation.Application.Sales.GetAllSales;
using Ambev.DeveloperEvaluation.Domain.Sales.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetAllSales
{
    public class GetAllSalesQueryHandler : IRequestHandler<GetAllSalesQuery, List<GetAllSalesResult>>
    {
        private readonly ISaleRepository _repository;

        public GetAllSalesQueryHandler(ISaleRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetAllSalesResult>> Handle(GetAllSalesQuery request, CancellationToken cancellationToken)
        {
            var sales = await _repository.GetAllAsync();

            return sales.Select(s => new GetAllSalesResult
            {
                Id = s.Id,
                SaleNumber = s.SaleNumber,
                Date = s.Date,
                CustomerName = s.CustomerName,
                BranchName = s.BranchName,
                TotalAmount = s.TotalAmount
            }).ToList();
        }
    }
}
