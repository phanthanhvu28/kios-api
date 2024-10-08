﻿using ApplicationCore.Contracts.SpecificationBase;

namespace ApplicationCore.Specifications.Order;
public class OrderByCodeStatusSpec : SpecificationBase<Entities.Orders>
{
    public OrderByCodeStatusSpec(string code, string[] status)
    {
        ApplyFilter(entity => entity.Code == code);
        ApplyFilter(entity => status.Any(d => d == entity.Status));//New, Partial, Finish
        ApplyFilter(entity => entity.IsDelete == false);
    }
}
