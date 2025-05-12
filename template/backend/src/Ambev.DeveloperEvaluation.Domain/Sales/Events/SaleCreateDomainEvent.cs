using Ambev.DeveloperEvaluation.Domain.Sales.Entities;
using MediatR;
using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Domain.Sales.Events
{
    public class SaleCreatedDomainEvent : INotification
    {
        public Guid SaleId { get; }
        public string SaleNumber { get; }
        public Guid CustomerId { get; }
        public string CustomerName { get; }
        public Guid BranchId { get; }
        public string BranchName { get; }
        public List<SaleItem> Items { get; }

        public SaleCreatedDomainEvent(Guid saleId, string saleNumber, Guid customerId, string customerName, Guid branchId, string branchName, List<SaleItem> items)
        {
            SaleId = saleId;
            SaleNumber = saleNumber;
            CustomerId = customerId;
            CustomerName = customerName;
            BranchId = branchId;
            BranchName = branchName;
            Items = items;
        }
    }

    public class SaleModifiedDomainEvent : INotification
    {
        public Guid SaleId { get; }
        public string NewSaleNumber { get; }
        public Guid NewCustomerId { get; }
        public string NewCustomerName { get; }
        public Guid NewBranchId { get; }
        public string NewBranchName { get; }

        public SaleModifiedDomainEvent(Guid saleId, string newSaleNumber, Guid newCustomerId, string newCustomerName, Guid newBranchId, string newBranchName)
        {
            SaleId = saleId;
            NewSaleNumber = newSaleNumber;
            NewCustomerId = newCustomerId;
            NewCustomerName = newCustomerName;
            NewBranchId = newBranchId;
            NewBranchName = newBranchName;
        }
    }

    public class SaleCancelledDomainEvent : INotification
    {
        public Guid SaleId { get; }

        public SaleCancelledDomainEvent(Guid saleId)
        {
            SaleId = saleId;
        }
    }

    public class ItemCancelledDomainEvent : INotification
    {
        public Guid SaleId { get; }
        public Guid ProductId { get; }

        public ItemCancelledDomainEvent(Guid saleId, Guid productId)
        {
            SaleId = saleId;
            ProductId = productId;
        }
    }
}
