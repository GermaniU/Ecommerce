namespace Ecommerce.Domain.Common;

public  abstract class BaseDomainModel
{
    public  int Id { get; protected set; }
    public  DateTime CreatedAt { get; protected set; }
    public  DateTime UpdatedAt { get; protected set; }
    public string? CreatedBy { get;  set; }
    public DateTime? LastModifiedBy { get;  set; }
}