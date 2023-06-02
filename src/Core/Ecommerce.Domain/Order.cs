using System.ComponentModel.DataAnnotations.Schema;
using  Ecommerce.Domain;
using Ecommerce.Domain.Common;

namespace Ecommerce.Domain;

public class Order:BaseDomainModel{
    
    public Order(){} // This is required for EF Core

    public Order(string buyerEmail, string buyerName, string buyerPhoneNumber, 
     OrderAddress shippingAddress, decimal subtotalPrice, decimal totalPrice,
     decimal taxes, string shippingPrice)
    {
        BuyerEmail = buyerEmail;
        BuyerName = buyerName;
        BuyerPhoneNumber = buyerPhoneNumber;
        ShippingAddress = shippingAddress;
        SubtotalPrice = subtotalPrice;
        TotalPrice = totalPrice;
        Taxes = taxes;
        ShippingPrice = shippingPrice;
    }

    public string? BuyerName { get;  set; }

    public string? BuyerUsername { get;  set; }

    public string? BuyerEmail { get;  set; }

    public string? BuyerPhoneNumber { get;  set; }

    public OrderAddress? ShippingAddress { get;  set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal SubtotalPrice { get;  set; }

    public OrderStatus Status { get;  set; } = OrderStatus.Pending;

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalPrice { get;  set; }
  
    [Column(TypeName = "decimal(18,2)")]
    public decimal Taxes { get;  set; }  

    public string? ShippingPrice { get;  set; }

    public string? PaymentIntentId { get;  set; }

    public string? PaymentMethod { get;  set; }

    public string? ClientSecret { get;  set; }

    public string? StripeApiKey { get;  set; }

    public virtual ICollection<OrderItem>? OrderItems { get;  set; }
}