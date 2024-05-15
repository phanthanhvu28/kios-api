using ApplicationCore.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities;

[Table("Products")]
[Index(nameof(Code), IsUnique = true)]
public class Products : EntityBaseCode
{
    [Column(TypeName = "nvarchar(256)")]
    public string? Name { get; set; }

    [Column(TypeName = "varchar(50)")]
    public string StoreCode { get; set; }

    [Column(TypeName = "varchar(50)")]
    public string TypeSaleCode { get; set; }

    [Column(TypeName = "varchar(50)")]
    public string TypeBidaCode { get; set; }


}
