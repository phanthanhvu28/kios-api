using ApplicationCore.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities;

[Table("OrderDetails")]
[Index(nameof(Code), IsUnique = true)]
public class OrderDetails : EntityBaseCode
{
    [Column(TypeName = "varchar(50)")]
    public string OrderCode { get; set; }
    [Column(TypeName = "varchar(50)")]
    public string ProductCode { get; set; }

    public int Quantity { get; set; }

    [Column(TypeName = "decimal(9, 2)")]
    public decimal UnitPrice { get; set; }
    [Column(TypeName = "decimal(9, 2)")]
    public decimal Amount { get; set; }

}
