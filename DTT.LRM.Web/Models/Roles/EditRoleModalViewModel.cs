using System.Collections.Generic;
using System.Linq;
using DTT.LRM.Roles.Dto;

namespace DTT.LRM.Web.Models.Roles
{
    public class EditRoleModalViewModel
    {
        public RoleDto Role { get; set; }

        public IReadOnlyList<PermissionDto> Permissions { get; set; }

        public bool HasPermission(PermissionDto permission)
        {
            if (Role.Id == 0)
                return false;
            return Permissions != null && Role.GrantedPermissions.Any(p => p == permission.Name);
        }
    }
}