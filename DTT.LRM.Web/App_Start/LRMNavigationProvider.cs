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
                        PageNames.SearchInfos,
                        L("SearchInfos"),
                        url: "SearchInfos",
                        icon: "search",
                        requiresAuthentication: true,
                        permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_SearchInfos)
                    )
                )
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.MyBooks,
                        L("MyBooks"),
                        url: "MyBooks",
                        icon: "book",
                        requiresAuthentication: true,
                        permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_MyBooks)
                    )
                )
                .AddItem( //Menu items below is just for demonstration!
                    new MenuItemDefinition(
                        "BookUsing",
                        L("BookUsing"),
                        icon: "menu"
                    )
                    .AddItem(
                        new MenuItemDefinition(
                            "Borrows",
                            L("Borrows"),
                            url: "Borrows",
                            icon: "book",
                            requiresAuthentication: true,
                            permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Borrows)
                        )
                    )
                    .AddItem(
                        new MenuItemDefinition(
                            "GiveBacks",
                            L("GiveBacks"),
                            url: "GiveBacks",
                            icon: "book",
                            requiresAuthentication: true,
                            permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_GiveBacks)
                        )
                    )
                )
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Liquidations,
                        L("Liquidations"),
                        url: "Liquidations",
                        icon: "book",
                        requiresAuthentication: true,
                        permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Liquidations)
                    )
                )
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Books,
                        L("Books"),
                        url: "Books",
                        icon: "book",
                        requiresAuthentication: true,
                        permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Books)
                    )
                )
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Readers,
                        L("Readers"),
                        url: "Readers",
                        icon: "people",
                        requiresAuthentication: true,
                        permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Readers)
                    )
                )
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Employees,
                        L("Employees"),
                        url: "Employees",
                        icon: "people",
                        requiresAuthentication: true,
                        permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Employees)
                    )
                )
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Statistics,
                        L("Statistics"),
                        url: "Statistics",
                        icon: "menu",
                        requiresAuthentication: true,
                        permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Statistics)
                    )
                )
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
                                url: "BookClassifies",
                                permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_BookClassifies)
                            )
                        )
                        .AddItem(
                            new MenuItemDefinition(
                                "BookFields",
                                L("BookFields"),
                                url: "BookFields",
                                permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_BookFields)
                            )
                        )
                        .AddItem(
                            new MenuItemDefinition(
                                "BookCategories",
                                L("BookCategories"),
                                url: "BookCategories",
                                permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_BookCategories)
                            )
                        )
                        .AddItem(
                            new MenuItemDefinition(
                                "Publishers",
                                L("Publishers"),
                                url: "Publishers",
                                permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Publishers)
                            )
                        )
                        .AddItem(
                            new MenuItemDefinition(
                                "Positions",
                                L("Positions"),
                                url: "Positions",
                                permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Positions)
                            )
                        )
                        .AddItem(
                            new MenuItemDefinition(
                                "OrganizationUnits",
                                L("OrganizationUnits"),
                                url: "OrganizationUnits",
                                permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_OrganizationUnits)
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
                                "Roles",
                                L("Roles"),
                                url: "Roles",
                                permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Roles)
                            )
                        )
                        .AddItem(
                            new MenuItemDefinition(
                                "UserList",
                                L("UserList"),
                                url: "UserList",
                                permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_UserList)
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
