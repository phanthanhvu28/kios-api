using ApplicationCore.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities;

[Table("AuthenUser")]
[Index(nameof(Username), IsUnique = true)]
public class AuthenUser : EntityBase
{
    [Column(TypeName = "varchar(256)")]
    public string Username { get; set; }

    [Column(TypeName = "varchar(512)")]
    public string Password { get; set; }
}
