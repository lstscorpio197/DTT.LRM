using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace DTT.LRM.Authorization
{
    public class LRMAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);

            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));

            context.CreatePermission(PermissionNames.Pages_UserList, L("View"));
            context.CreatePermission(PermissionNames.Pages_UserList_Update, L("Edit"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("View"));
            context.CreatePermission(PermissionNames.Pages_Roles_Create, L("Add"));
            context.CreatePermission(PermissionNames.Pages_Roles_Update, L("Edit"));
            context.CreatePermission(PermissionNames.Pages_Roles_Delete, L("Delete"));

            context.CreatePermission(PermissionNames.Pages_BookCategories, L("View"));
            context.CreatePermission(PermissionNames.Pages_BookCategories_Create, L("Add"));
            context.CreatePermission(PermissionNames.Pages_BookCategories_Update, L("Edit"));
            context.CreatePermission(PermissionNames.Pages_BookCategories_Delete, L("Delete"));

            context.CreatePermission(PermissionNames.Pages_Books, L("View"));
            context.CreatePermission(PermissionNames.Pages_Books_Create, L("Add"));
            context.CreatePermission(PermissionNames.Pages_Books_Update, L("Edit"));
            context.CreatePermission(PermissionNames.Pages_Books_Delete, L("Delete"));

            context.CreatePermission(PermissionNames.Pages_BookClassifies, L("View"));
            context.CreatePermission(PermissionNames.Pages_BookClassifies_Create, L("Add"));
            context.CreatePermission(PermissionNames.Pages_BookClassifies_Update, L("Edit"));
            context.CreatePermission(PermissionNames.Pages_BookClassifies_Delete, L("Delete"));

            context.CreatePermission(PermissionNames.Pages_BookFields, L("View"));
            context.CreatePermission(PermissionNames.Pages_BookFields_Create, L("Add"));
            context.CreatePermission(PermissionNames.Pages_BookFields_Update, L("Edit"));
            context.CreatePermission(PermissionNames.Pages_BookFields_Delete, L("Delete"));

            context.CreatePermission(PermissionNames.Pages_OrganizationUnits, L("View"));
            context.CreatePermission(PermissionNames.Pages_OrganizationUnits_Create, L("Add"));
            context.CreatePermission(PermissionNames.Pages_OrganizationUnits_Update, L("Edit"));
            context.CreatePermission(PermissionNames.Pages_OrganizationUnits_Delete, L("Delete"));

            context.CreatePermission(PermissionNames.Pages_Positions, L("View"));
            context.CreatePermission(PermissionNames.Pages_Positions_Create, L("Add"));
            context.CreatePermission(PermissionNames.Pages_Positions_Update, L("Edit"));
            context.CreatePermission(PermissionNames.Pages_Positions_Delete, L("Delete"));

            context.CreatePermission(PermissionNames.Pages_Publishers, L("View"));
            context.CreatePermission(PermissionNames.Pages_Publishers_Create, L("Add"));
            context.CreatePermission(PermissionNames.Pages_Publishers_Update, L("Edit"));
            context.CreatePermission(PermissionNames.Pages_Publishers_Delete, L("Delete"));

            context.CreatePermission(PermissionNames.Pages_Readers, L("View"));
            context.CreatePermission(PermissionNames.Pages_Readers_Create, L("Add"));
            context.CreatePermission(PermissionNames.Pages_Readers_Update, L("Edit"));
            context.CreatePermission(PermissionNames.Pages_Readers_Delete, L("Delete"));

            context.CreatePermission(PermissionNames.Pages_Employees, L("View"));
            context.CreatePermission(PermissionNames.Pages_Employees_Create, L("Add"));
            context.CreatePermission(PermissionNames.Pages_Employees_Update, L("Edit"));
            context.CreatePermission(PermissionNames.Pages_Employees_Delete, L("Delete"));

            context.CreatePermission(PermissionNames.Pages_Systems, L("View"));
            context.CreatePermission(PermissionNames.Pages_Administration, L("View"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, LRMConsts.LocalizationSourceName);
        }
    }
}
