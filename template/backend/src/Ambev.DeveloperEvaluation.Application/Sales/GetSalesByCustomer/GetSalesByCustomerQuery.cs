using MediatR;
using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSalesByCustomer
{
    public class GetSalesByCustomerQuery : IRequest<List<GetSalesByCustomerDto>>
    {
        public Guid CustomerId { get; set; }

        public GetSalesByCustomerQuery() { }

        public GetSalesByCustomerQuery(Guid customerId)
        {
            CustomerId = customerId;
        }
    }
}