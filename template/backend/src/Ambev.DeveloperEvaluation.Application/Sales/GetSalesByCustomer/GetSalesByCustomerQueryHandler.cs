using Ambev.DeveloperEvaluation.Domain.Sales.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSalesByCustomer
{
    public class GetSalesByCustomerQueryHandler : IRequestHandler<GetSalesByCustomerQuery, List<GetSalesByCustomerDto>>
    {
        private readonly ISaleRepository _repository;

        public GetSalesByCustomerQueryHandler(ISaleRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetSalesByCustomerDto>> Handle(GetSalesByCustomerQuery request, CancellationToken cancellationToken)
        {
            var sales = await _repository.GetByCustomerIdAsync(request.CustomerId);

            return sales.Select(s => new GetSalesByCustomerDto
            {
                Id = s.Id,
                SaleNumber = s.SaleNumber,
                CustomerId = s.CustomerId,
                CustomerName = s.CustomerName,
                BranchId = s.BranchId,
                BranchName = s.BranchName,
                TotalAmount = s.TotalAmount,
                IsCancelled = s.IsCancelled,
                Date = s.Date
            }).ToList();
        }
    }
}
