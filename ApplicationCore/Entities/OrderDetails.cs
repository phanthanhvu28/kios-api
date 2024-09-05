using ApplicationCore.Contracts.Domains;
using ApplicationCore.Entities.Common;
using Microsoft.EntityFrameworkCore;
using OneOf;
using System.ComponentModel.DataAnnotations.Schema;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.Entities;

[Table("OrderDetails")]
[Index(nameof(Code), IsUnique = true)]
public class OrderDetails : EntityBaseCode, IKiosProcess
{
    [Column(TypeName = "varchar(50)")]
    public string OrderCode { get; set; }

    [Column(TypeName = "varchar(50)")]
    public string StaffCode { get; set; }

    [Column(TypeName = "varchar(50)")]
    public string ProductCode { get; set; }

    public int Quantity { get; set; }

    [Column(TypeName = "decimal(9, 2)")]
    public decimal UnitPrice { get; set; }

    [Column(TypeName = "decimal(9, 2)")]
    public decimal Amount { get; set; }

    public OneOf<bool, CommonExceptionBase> ProcessStep(IWorkflowProcess workflowProcess)
    {
        return workflowProcess.Execute(this);
    }
    public override string PrefixCode => Constants.Prefix.OrderDetail;
}
