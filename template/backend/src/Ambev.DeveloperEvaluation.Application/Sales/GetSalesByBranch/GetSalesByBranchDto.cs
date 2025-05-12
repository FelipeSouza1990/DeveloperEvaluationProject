using System;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSalesByBranch
{
    public class GetSalesByBranchDto
    {
        public Guid Id { get; set; }
        public string SaleNumber { get; set; }
        public DateTime Date { get; set; }

        public Guid BranchId { get; set; }
        public string BranchName { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsCancelled { get; set; }
    }
}
