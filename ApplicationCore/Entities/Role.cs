using ApplicationCore.Contracts.Domains;
using ApplicationCore.Entities.Common;
using Microsoft.EntityFrameworkCore;
using OneOf;
using System.ComponentModel.DataAnnotations.Schema;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.Entities;

[Table("Roles")]
[Index(nameof(Code), IsUnique = true)]
public class Role : EntityBaseCode, IKiosProcess
{
    /// <summary>
    /// Administrator, Read, Write,...
    /// </summary>
    [Column(TypeName = "nvarchar(256)")]
    public string? Name { get; set; }

    [Column(TypeName = "json")]
    public List<AuthenMenu> Menus { get; set; } = new List<AuthenMenu>();

    public OneOf<bool, CommonExceptionBase> ProcessStep(IWorkflowProcess workflowProcess)
    {
        return workflowProcess.Execute(this);
    }
    public override string PrefixCode => Constants.Prefix.Role;
}
