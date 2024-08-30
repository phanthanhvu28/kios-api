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
                ApiCode="Kios",
                ApiName="Kios",
                Sites = new List<MenuSite>()
                {
                    new MenuSite()
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
                    new MenuSite()
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
                    new MenuSite()
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
                    },
                    new MenuSite()
                    {
                        SiteCode="Role",
                        SiteName="Role",
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
                    new MenuSite()
                    {
                        SiteCode="Area",
                        SiteName="Area",
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
                    new MenuSite()
                    {
                        SiteCode="Table",
                        SiteName="Table",
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
                    new MenuSite()
                    {
                        SiteCode="Type-Sale",
                        SiteName="Type Sale",
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
                    new MenuSite()
                    {
                        SiteCode="Type-Bida",
                        SiteName="Type Bida",
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
                    new MenuSite()
                    {
                        SiteCode="Staff",
                        SiteName="Staff",
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
                    new MenuSite()
                    {
                        SiteCode="Product",
                        SiteName="Product",
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
                }
            },
            new AuthenMenu()
            {
                ApiCode="shop",
                ApiName="shop",
                Sites = new List<MenuSite>()
                {
                    new MenuSite()
                    {
                        SiteCode="SHop1",
                        SiteName="SHop1",
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
                }

            }
        };
    }
}
