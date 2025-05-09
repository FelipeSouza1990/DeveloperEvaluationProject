using Ambev.DeveloperEvaluation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ambev.DeveloperEvaluation.Domain.Sales.Entities
{
    public class Sale
    {
        public Guid Id { get; private set; }
        public string SaleNumber { get; private set; }
        public DateTime Date { get; private set; }
        public Guid CustomerId { get; private set; }
        public string CustomerName { get; private set; }
        public Guid BranchId { get; private set; }
        public string BranchName { get; private set; }
        public bool IsCancelled { get; private set; }
        public List<SaleItem> Items { get; private set; } = new();
        public decimal TotalAmount => Items.Sum(i => i.TotalAmount);

        protected Sale() { }

        public Sale(string saleNumber, Guid customerId, string customerName, Guid branchId, string branchName)
        {
            Id = Guid.NewGuid();
            SaleNumber = saleNumber;
            Date = DateTime.UtcNow;
            CustomerId = customerId;
            CustomerName = customerName;
            BranchId = branchId;
            BranchName = branchName;
            IsCancelled = false;
        }

        public void AddItem(Guid productId, string productName, int quantity, decimal unitPrice)
        {
            var item = new SaleItem(Id, productId, productName, quantity, unitPrice);
            Items.Add(item);
        }

        public void Cancel()
        {
            IsCancelled = true;
        }
    }
}
