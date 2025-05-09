using System;

namespace Ambev.DeveloperEvaluation.Domain.Sales.Entities
{
    public class SaleItem
    {
        public Guid Id { get; private set; }
        public Guid SaleId { get; private set; }
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal Discount { get; private set; }
        public decimal TotalAmount => Quantity * UnitPrice * (1 - Discount);

        protected SaleItem() { }

        public SaleItem(Guid saleId, Guid productId, string productName, int quantity, decimal unitPrice)
        {
            if (quantity > 20)
                throw new ArgumentException("Maximum of 20 items per product allowed.");

            SaleId = saleId;
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Discount = CalculateDiscount(quantity);
            Id = Guid.NewGuid();
        }

        private decimal CalculateDiscount(int quantity)
        {
            if (quantity >= 10) return 0.2m;
            if (quantity >= 4) return 0.1m;
            return 0;
        }
    }
}
