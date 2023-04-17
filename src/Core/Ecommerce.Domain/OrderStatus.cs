using System.Runtime.Serialization;

namespace Ecommerce.Domain;

public enum OrderStatus
{
    [EnumMember(Value = "Pending")]
    Pending,
    [EnumMember(Value = "Completed")]
    Completed,
    [EnumMember(Value = "Productshipped")]
    Productshipped,
    [EnumMember(Value = "Error")]
    Error
}