﻿using ApplicationCore.Contracts.Domains;
using ApplicationCore.Entities.Common;
using Microsoft.EntityFrameworkCore;
using OneOf;
using System.ComponentModel.DataAnnotations.Schema;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.Entities;

[Table("Orders")]
[Index(nameof(Code), IsUnique = true)]
public class Orders : EntityBaseCode, IKiosProcess
{

    [Column(TypeName = "varchar(50)")]
    public string StoreCode { get; set; }

    [Column(TypeName = "varchar(50)")]
    public string TableCode { get; set; }

    [Column(TypeName = "varchar(50)")]
    public string StaffCode { get; set; }

    public DateTime OrderDate { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    [Column(TypeName = "decimal(9,2)")]
    public decimal TotalCost { get; set; }

    public OneOf<bool, CommonExceptionBase> ProcessStep(IWorkflowProcess workflowProcess)
    {
        return workflowProcess.Execute(this);
    }
    public override string PrefixCode => Constants.Prefix.Order;
}
