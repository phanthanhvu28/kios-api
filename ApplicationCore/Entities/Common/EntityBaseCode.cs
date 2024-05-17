using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using VELA.WebCoreBase.Libraries.Extensions;

namespace ApplicationCore.Entities.Common;

[Index(nameof(Code))]
public abstract class EntityBaseCode : EntityBase
{
    protected static Random RandomInstance = new();

    [Column(TypeName = "varchar(50)")]
    public virtual string? Code { get; set; }

    public abstract string PrefixCode { get; }
    public virtual void GenerateCode()
    {
        string formattedTime = DateTimeExtension.CreateDisplayFormat("hhmmssffffff");
        string digits = $"{PrefixCode}{formattedTime}{StringExtension.RandomDigitsLength(3)}";
        GenerateCode(digits);

    }
    public void GenerateCode(string format)
    {
        Code ??= format;
    }
}
