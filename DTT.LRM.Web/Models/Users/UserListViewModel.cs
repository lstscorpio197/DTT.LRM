using System.Collections.Generic;
using DTT.LRM.Roles.Dto;
using DTT.LRM.Users.Dto;

namespace DTT.LRM.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<UserDto> Users { get; set; }

        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}