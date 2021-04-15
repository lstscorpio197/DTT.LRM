using System.Collections.Generic;
using DTT.LRM.Roles.Dto;

namespace DTT.LRM.Web.Models.Roles
{
    public class RoleListViewModel
    {
        public IReadOnlyList<RoleDto> Roles { get; set; }

        public IReadOnlyList<PermissionDto> Permissions { get; set; }
    }
}