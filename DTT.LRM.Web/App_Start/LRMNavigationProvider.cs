using Abp.Application.Navigation;
using Abp.Authorization;
using Abp.Localization;
using DTT.LRM.Authorization;

namespace DTT.LRM.Web
{
    /// <summary>
    /// This class defines menus for the application.
    /// It uses ABP's menu system.
    /// When you add menu items here, they are automatically appear in angular application.
    /// See Views/Layout/_TopMenu.cshtml file to know how to render menu.
    /// </summary>
    public class LRMNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Home,
                        L("HomePage"),
                        url: "",
                        icon: "home",
                        requiresAuthentication: true
                    )
                )
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Books,
                        L("Books"),
                        url: "Books",
                        icon: "book",
                        requiresAuthentication: true
                    )
                )
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Readers,
                        L("Readers"),
                        url: "Readers",
                        icon: "people",
                        requiresAuthentication: true
                    )
                )
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Employees,
                        L("Employees"),
                        url: "Employees",
                        icon: "people",
                        requiresAuthentication: true
                    )
                )
                //.AddItem(
                //    new MenuItemDefinition(
                //        PageNames.Tenants,
                //        L("Tenants"),
                //        url: "Tenants",
                //        icon: "business",
                //        permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Tenants)
                //    )
                //).AddItem(
                //    new MenuItemDefinition(
                //        PageNames.Users,
                //        L("Users"),
                //        url: "Users",
                //        icon: "people",
                //        permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Users)
                //    )
                //).AddItem(
                //    new MenuItemDefinition(
                //        PageNames.Roles,
                //        L("Roles"),
                //        url: "Roles",
                //        icon: "local_offer",
                //        permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Roles)
                //    )
                //).AddItem(
                //    new MenuItemDefinition(
                //        PageNames.About,
                //        L("About"),
                //        url: "About",
                //        icon: "info"
                //    )
                //)
                .AddItem( //Menu items below is just for demonstration!
                    new MenuItemDefinition(
                        "Systems",
                        L("Systems"),
                        icon: "menu"
                    )
                    .AddItem(
                        new MenuItemDefinition(
                            "Categories",
                            L("Categories")
                        )
                        .AddItem(
                            new MenuItemDefinition(
                                "BookClassifies",
                                L("BookClassifies"),
                                url: "BookClassifies"
                            )
                        )
                        .AddItem(
                            new MenuItemDefinition(
                                "BookFields",
                                L("BookFields"),
                                url: "BookFields"
                            )
                        )
                        .AddItem(
                            new MenuItemDefinition(
                                "BookCategories",
                                L("BookCategories"),
                                url: "BookCategories"
                            )
                        )
                        .AddItem(
                            new MenuItemDefinition(
                                "Positions",
                                L("Positions"),
                                url: "Positions"
                            )
                        )
                        .AddItem(
                            new MenuItemDefinition(
                                "Publishers",
                                L("Publishers"),
                                url: "Publishers"
                            )
                        )
                    )
                    .AddItem(
                        new MenuItemDefinition(
                            "Administration",
                            L("Administration")
                        )
                        .AddItem(
                            new MenuItemDefinition(
                                "Decentralization",
                                L("Decentralization"),
                                url: "Decentralization"
                            )
                        )
                        .AddItem(
                            new MenuItemDefinition(
                                "UserList",
                                L("UserList"),
                                url: "UserList"
                            )
                        )
                    )
                );
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, LRMConsts.LocalizationSourceName);
        }
    }
}
