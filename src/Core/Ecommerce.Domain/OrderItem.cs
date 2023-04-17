namespace Ecommerce.Domain;

using System.ComponentModel.DataAnnotations.Schema;
using Ecommerce.Domain.Common;

public class OrderItem:BaseDomainModel{
    public int Quantity { get;  set; }
    public int? ProductId { get;  set; }
    public  string? ProductName { get;  set; }
    public  string? ImageUrl { get;  set; }
    public Guid? OrderId { get;  set; }
    public virtual Order? Order { get;  set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get;  set; }
}