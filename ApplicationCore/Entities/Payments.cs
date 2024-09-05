using ApplicationCore.Contracts.Domains;
using ApplicationCore.Entities.Common;
using Microsoft.EntityFrameworkCore;
using OneOf;
using System.ComponentModel.DataAnnotations.Schema;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.Entities;

[Table("Payments")]
[Index(nameof(Code), IsUnique = true)]
public class Payments : EntityBaseCode, IKiosProcess
{
    [Column(TypeName = "varchar(50)")]
    public string TableCode { get; set; }
    public DateTime PaymentDate { get; set; }

    [Column(TypeName = "decimal(9,2)")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Card, Cash, Wallet
    /// </summary>

    [Column(TypeName = "varchar(50)")]
    public string PaymentMethod { get; set; }
    public OneOf<bool, CommonExceptionBase> ProcessStep(IWorkflowProcess workflowProcess)
    {
        return workflowProcess.Execute(this);
    }
    public override string PrefixCode => Constants.Prefix.Payment;
}
