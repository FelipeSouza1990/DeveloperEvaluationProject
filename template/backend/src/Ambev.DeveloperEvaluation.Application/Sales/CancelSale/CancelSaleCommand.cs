using MediatR;
using System;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    public class CancelSaleCommand : IRequest<bool>
    {
        public Guid SaleId { get; set; }

        public CancelSaleCommand() { }

        public CancelSaleCommand(Guid saleId)
        {
            SaleId = saleId;
        }
    }
}
