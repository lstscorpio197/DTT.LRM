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
                        L("Quản lý sách"),
                        url: "Books",
                        icon: "book",
                        requiresAuthentication: true
                    )
                )
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Readers,
                        L("Quản lý độc giả"),
                        url: "Readers",
                        icon: "people",
                        requiresAuthentication: true
                    )
                )
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Employees,
                        L("Quản lý nhân viên"),
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
                        L("Hệ thống"),
                        icon: "menu"
                    )
                    .AddItem(
                        new MenuItemDefinition(
                            "Category",
                            new FixedLocalizableString("Danh mục")
                        )
                        .AddItem(
                            new MenuItemDefinition(
                                "BookClassifies",
                                new FixedLocalizableString("Phân loại sách"),
                                url: "BookClassifies"
                            )
                        )
                        .AddItem(
                            new MenuItemDefinition(
                                "BookFields",
                                new FixedLocalizableString("Lĩnh vực sách"),
                                url: "BookFields"
                            )
                        )
                        .AddItem(
                            new MenuItemDefinition(
                                "BookCategories",
                                new FixedLocalizableString("Loại sách"),
                                url: "BookCategories"
                            )
                        )
                        .AddItem(
                            new MenuItemDefinition(
                                "Positions",
                                new FixedLocalizableString("Chức vụ"),
                                url: "Positions"
                            )
                        )
                    )
                    .AddItem(
                        new MenuItemDefinition(
                            "Administration",
                            new FixedLocalizableString("Quản trị")
                        )
                        .AddItem(
                            new MenuItemDefinition(
                                "Decentralization",
                                new FixedLocalizableString("Phân quyền"),
                                url: "Decentralization"
                            )
                        )
                        .AddItem(
                            new MenuItemDefinition(
                                "UserList",
                                new FixedLocalizableString("Danh sách người dùng"),
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
