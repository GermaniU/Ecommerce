using Ecommerce.Domain.Common;

namespace Ecommerce.Domain;

public class Address : BaseDomainModel
{
    public string? Street { get;  set; }
    public string? City { get;  set; }
    public string? State { get;  set; }
    public string? Country { get;  set; }
    public string? ZipCode { get;  set; }
    public string? Department { get; set; }
    public string? UserName { get;  set; }
    public string? PhoneNumber { get;  set; }
}