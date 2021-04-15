using Abp.Authorization;
using DTT.LRM.Authorization.Roles;
using DTT.LRM.Authorization.Users;

namespace DTT.LRM.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
