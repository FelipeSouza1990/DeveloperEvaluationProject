﻿public class SaleDto
{
    public Guid Id { get; set; }
    public string SaleNumber { get; set; }
    public DateTime Date { get; set; }
    public Guid CustomerId { get; set; }
    public string CustomerName { get; set; }
    public Guid BranchId { get; set; }
    public string BranchName { get; set; }
    public bool IsCancelled { get; set; }
    public decimal TotalAmount { get; set; }
    public List<SaleItemDto> Items { get; set; } = new();
}