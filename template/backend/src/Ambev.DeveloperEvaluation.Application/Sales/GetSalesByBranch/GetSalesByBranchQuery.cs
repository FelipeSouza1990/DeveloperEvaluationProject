using MediatR;
using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSalesByBranch
{
    public class GetSalesByBranchQuery : IRequest<List<GetSalesByBranchDto>>
    {
        public Guid BranchId { get; set; }

        public GetSalesByBranchQuery(Guid branchId)
        {
            BranchId = branchId;
        }
    }
}