using ApplicationCore.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities;

[Table("Companies")]
[Index(nameof(Code), IsUnique = true)]
public class Companies : EntityBaseCode
{
    [Column(TypeName = "nvarchar(256)")]
    public string? Name { get; set; }

    [Column(TypeName = "nvarchar(512)")]
    public string? Address { get; set; }

    [Column(TypeName = "varchar(256)")]
    public string? Email { get; set; }

    [Column(TypeName = "varchar(50)")]
    public string? Phone { get; set; }
}
