namespace Ambev.DeveloperEvaluation.Application.Sales.GetSalesByCustomer
{
    public class GetSalesByCustomerDto
    {
        public Guid Id { get; set; }
        public string SaleNumber { get; set; }
        public Guid CustomerId { get; set; } // Adicione esta linha
        public string CustomerName { get; set; }
        public string BranchName { get; set; }
        public Guid BranchId { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsCancelled { get; set; }
    }
}

