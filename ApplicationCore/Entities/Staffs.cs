using ApplicationCore.Contracts.Domains;
using ApplicationCore.Entities.Common;
using Microsoft.EntityFrameworkCore;
using OneOf;
using System.ComponentModel.DataAnnotations.Schema;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.Entities;

[Table("Staffs")]
[Index(nameof(Code), IsUnique = true)]
public class Staffs : EntityBaseCode, IKiosProcess
{

    [Column(TypeName = "varchar(50)")]
    public string StoreCode { get; set; }

    [Column(TypeName = "nvarchar(250)")]
    public string? FullName { get; set; }

    [Column(TypeName = "nvarchar(250)")]
    public string? Email { get; set; }

    [Column(TypeName = "nvarchar(250)")]
    public string? Address { get; set; }

    public OneOf<bool, CommonExceptionBase> ProcessStep(IWorkflowProcess workflowProcess)
    {
        return workflowProcess.Execute(this);
    }
    public override string PrefixCode => Constants.Prefix.Staff;
}
