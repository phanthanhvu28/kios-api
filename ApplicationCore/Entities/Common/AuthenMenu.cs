using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities.Common;
public sealed record AuthenMenu
{
    //[Column(TypeName = "json")]
    //public List<MenuApi> Apis { get; set; } = new List<MenuApi>();

    public string ApiCode { get; set; }//KiosApi
    public string ApiName { get; set; }//Kios

    [Column(TypeName = "json")]
    public List<MenuSite> Sites { get; set; } = new List<MenuSite>();

    //public string SiteCode { get; set; }//Company, Store
    //public string SiteName { get; set; }//Company, Store

    //[Column(TypeName = "json")]
    //public List<Feature> Feature { get; set; } = new List<Feature>();
}

public sealed record Feature
{
    public string FeatureCode { get; set; }//V,A,E,D
    public string FeatureName { get; set; }//View, Add, Edit, Delete
}

public sealed record MenuApi
{
    public string ApiCode { get; set; }//KiosApi
    public string ApiName { get; set; }//Kios

    [Column(TypeName = "json")]
    public List<MenuSite> Sites { get; set; } = new List<MenuSite>();
}

public sealed record MenuSite
{
    public string SiteCode { get; set; }//Company, Store
    public string SiteName { get; set; }//Company, Store

    [Column(TypeName = "json")]
    public List<Feature> Feature { get; set; } = new List<Feature>();
}
