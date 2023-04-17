using System.ComponentModel.DataAnnotations.Schema;
using Ecommerce.Domain.Common;

namespace Ecommerce.Domain;

public class ShoppingCartItem : BaseDomainModel
{
    public Guid? ShoppingCartMasterId { get;  set; }
    public int ShoppingCartItemId { get;  set; }
    public int ProductId { get;  set; }
    public int Quantity { get;  set; }
    public string? ProductName { get;  set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get;  set; }
    public string? PictureUrl { get;  set; }
    public string? CategoryName { get;  set; }
    public int Stock { get;  set; }

    public int ShoppingCartId {get; set;}
    public virtual ShoppingCart? ShoppingCart {get; set;}
}