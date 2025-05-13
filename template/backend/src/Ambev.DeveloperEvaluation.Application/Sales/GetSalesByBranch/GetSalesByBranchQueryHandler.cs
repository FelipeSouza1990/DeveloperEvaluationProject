using Ambev.DeveloperEvaluation.Domain.Sales.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSalesByBranch
{
    public class GetSalesByBranchQueryHandler : IRequestHandler<GetSalesByBranchQuery, List<GetSalesByBranchDto>>
    {
        private readonly ISaleRepository _repository;

        public GetSalesByBranchQueryHandler(ISaleRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetSalesByBranchDto>> Handle(GetSalesByBranchQuery request, CancellationToken cancellationToken)
        {
            var sales = await _repository.GetAllAsync();

            return sales
                .Where(s => s.BranchId == request.BranchId)
                .Select(s => new GetSalesByBranchDto
                {
                    Id = s.Id,
                    SaleNumber = s.SaleNumber,
                    Date = s.Date,
                    CustomerName = s.CustomerName,
                    BranchId = s.BranchId,
                    TotalAmount = s.TotalAmount,
                    IsCancelled = s.IsCancelled
                }).ToList();
        }
    }
}
