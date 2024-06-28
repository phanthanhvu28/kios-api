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

    [Column(TypeName = "nvarchar(256)")]
    public string Fullname { get; set; }

    [Column(TypeName = "varchar(256)")]
    public string Email { get; set; }

    [Column(TypeName = "nvarchar(256)")]
    public string Address { get; set; }

    [Column(TypeName = "varchar(256)")]
    public string Phone { get; set; }

    [Column(TypeName = "varchar(512)")]
    public string Password { get; set; }
}
