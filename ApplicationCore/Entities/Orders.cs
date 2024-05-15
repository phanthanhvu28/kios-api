using ApplicationCore.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities;

[Table("Orders")]
[Index(nameof(Code), IsUnique = true)]
public class Orders : EntityBaseCode
{
    [Column(TypeName = "varchar(50)")]
    public string AreaCode { get; set; }

    [Column(TypeName = "varchar(50)")]
    public string StoreCode { get; set; }

    [Column(TypeName = "varchar(50)")]
    public string TableCode { get; set; }

    public DateTime OrderDate { get; set; }

}
