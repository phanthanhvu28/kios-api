using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities.Common;
public sealed record AuthenMenu
{
    public string SiteCode { get; set; }//Company, Store
    public string SiteName { get; set; }//Company, Store

    [Column(TypeName = "json")]
    public List<Feature> Feature { get; set; } = new List<Feature>();
}

public sealed record Feature
{
    public string FeatureCode { get; set; }//V,A,E,D
    public string FeatureName { get; set; }//View, Add, Edit, Delete
}
