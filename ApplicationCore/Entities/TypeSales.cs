using ApplicationCore.Contracts.Domains;
using ApplicationCore.Entities.Common;
using Microsoft.EntityFrameworkCore;
using OneOf;
using System.ComponentModel.DataAnnotations.Schema;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.Entities;

[Table("TypeSales")]
[Index(nameof(Code), IsUnique = true)]
public class TypeSales : EntityBaseCode, IKiosProcess
{
    /// <summary>
    /// Bida, Cafe, Food
    /// </summary>
    [Column(TypeName = "nvarchar(256)")]
    public string? Name { get; set; }

    [Column(TypeName = "varchar(50)")]
    public string StoreCode { get; set; }

    [Column(TypeName = "varchar(50)")]
    public string StaffCode { get; set; }

    public OneOf<bool, CommonExceptionBase> ProcessStep(IWorkflowProcess workflowProcess)
    {
        return workflowProcess.Execute(this);
    }
    public override string PrefixCode => Constants.Prefix.TypeSale;
}
