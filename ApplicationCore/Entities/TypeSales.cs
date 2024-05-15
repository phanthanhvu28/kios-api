using ApplicationCore.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities;

[Table("TypeSales")]
[Index(nameof(Code), IsUnique = true)]
public class TypeSales : EntityBaseCode
{
    /// <summary>
    /// Bida, Cafe, Food
    /// </summary>
    [Column(TypeName = "nvarchar(256)")]
    public string? Name { get; set; }

    [Column(TypeName = "varchar(50)")]
    public string StoreCode { get; set; }

}
