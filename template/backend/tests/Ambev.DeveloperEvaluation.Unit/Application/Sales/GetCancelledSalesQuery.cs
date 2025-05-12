using MediatR;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetCancelledSales
{
    public class GetCancelledSalesQuery : IRequest<List<GetCancelledSalesDto>>
    {
    }
}
