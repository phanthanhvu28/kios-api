using ApplicationCore.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities;

[Table("Tables")]
[Index(nameof(Code), IsUnique = true)]
public class Tables : EntityBaseCode
{
    [Column(TypeName = "varchar(50)")]
    public string AreaCode { get; set; }

    [Column(TypeName = "varchar(50)")]
    public string StoreCode { get; set; }

    [Column(TypeName = "varchar(50)")]
    public string TypeSaleCode { get; set; }

    [Column(TypeName = "varchar(50)")]
    public string TypeBidaCode { get; set; }

    [Column(TypeName = "nvarchar(256)")]
    public string? Name { get; set; }

    [Column(TypeName = "nvarchar(512)")]
    public string? Address { get; set; }

    [Column(TypeName = "varchar(256)")]
    public string? Email { get; set; }

    [Column(TypeName = "varchar(50)")]
    public string? Phone { get; set; }
}
