﻿using ApplicationCore.Contracts.Domains;
using ApplicationCore.Entities.Common;
using Microsoft.EntityFrameworkCore;
using OneOf;
using System.ComponentModel.DataAnnotations.Schema;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.Entities;

[Table("Tables")]
[Index(nameof(Code), IsUnique = true)]
public class Tables : EntityBaseCode, IKiosProcess
{
    [Column(TypeName = "varchar(50)")]
    public string AreaCode { get; set; }

    [Column(TypeName = "varchar(50)")]
    public string StoreCode { get; set; }

    [Column(TypeName = "varchar(50)")]
    public string TypeSaleCode { get; set; }

    [Column(TypeName = "varchar(50)")]
    public string TypeBidaCode { get; set; }

    [Column(TypeName = "varchar(50)")]
    public string StaffCode { get; set; }

    [Column(TypeName = "nvarchar(256)")]
    public string? Name { get; set; }

    [Column(TypeName = "nvarchar(512)")]
    public string? Address { get; set; }

    [Column(TypeName = "varchar(256)")]
    public string? Email { get; set; }

    [Column(TypeName = "varchar(50)")]
    public string? Phone { get; set; }

    public OneOf<bool, CommonExceptionBase> ProcessStep(IWorkflowProcess workflowProcess)
    {
        return workflowProcess.Execute(this);
    }
    public override string PrefixCode => Constants.Prefix.Table;
}
