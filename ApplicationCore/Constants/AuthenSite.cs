using ApplicationCore.Entities.Common;

namespace ApplicationCore.Constants;
public struct AuthenSite
{
    public struct Site
    {
        public static readonly List<AuthenMenu> _menus = new()
        {
            new AuthenMenu()
            {
                SiteCode="Company",
                SiteName="Company",
                Feature = new List<Feature>()
                {
                    new Feature()
                    {
                        FeatureCode="View",
                        FeatureName="View"
                    },
                    new Feature()
                    {
                        FeatureCode="Add",
                        FeatureName="Add"
                    },
                    new Feature()
                    {
                        FeatureCode="Edit",
                        FeatureName="Edit"
                    },
                    new Feature()
                    {
                        FeatureCode="Del",
                        FeatureName="Del"
                    },
                    new Feature()
                    {
                        FeatureCode="Export",
                        FeatureName="Export"
                    }
                }
            },
            new AuthenMenu()
            {
                SiteCode="Store",
                SiteName="Store",
                Feature = new List<Feature>()
                {
                    new Feature()
                    {
                        FeatureCode="View",
                        FeatureName="View"
                    },
                    new Feature()
                    {
                        FeatureCode="Add",
                        FeatureName="Add"
                    },
                    new Feature()
                    {
                        FeatureCode="Edit",
                        FeatureName="Edit"
                    },
                    new Feature()
                    {
                        FeatureCode="Del",
                        FeatureName="Del"
                    },
                    new Feature()
                    {
                        FeatureCode="Export",
                        FeatureName="Export"
                    }
                }
            },
            new AuthenMenu()
            {
                SiteCode="User",
                SiteName="User",
                Feature = new List<Feature>()
                {
                    new Feature()
                    {
                        FeatureCode="View",
                        FeatureName="View"
                    },
                    new Feature()
                    {
                        FeatureCode="Add",
                        FeatureName="Add"
                    },
                    new Feature()
                    {
                        FeatureCode="Edit",
                        FeatureName="Edit"
                    },
                    new Feature()
                    {
                        FeatureCode="Del",
                        FeatureName="Del"
                    },
                    new Feature()
                    {
                        FeatureCode="Export",
                        FeatureName="Export"
                    }
                }
            }
        };
    }
}
