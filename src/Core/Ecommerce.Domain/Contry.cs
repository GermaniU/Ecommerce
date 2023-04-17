using System.ComponentModel.DataAnnotations.Schema;
using Ecommerce.Domain.Common;

namespace Ecommerce.Domain
{
    public class Country : BaseDomainModel
    {
        [Column(TypeName = "NVARCHAR(100)")]
        public string? Name { get;  set; }

        [Column(TypeName = "NVARCHAR(2)")]
        public string? Iso2 { get;  set; }

        [Column(TypeName = "NVARCHAR(3)")]
        public string? Iso3 { get;  set; }
    }
}