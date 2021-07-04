using DTT.LRM.Employees.Dto;
using DTT.LRM.OrganizationUnits.Dto;
using DTT.LRM.Positions.Dto;
using DTT.LRM.Roles.Dto;
using DTT.LRM.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTT.LRM.Web.Models.Employees
{
    public class CreateOrUpdateModel
    {
        public EmployeeDto Employee { get; set; }
        public UserDto User { get; set; }
        public List<OrganizationUnitBasicDto> ListOrganizationUnits { get; set; }
        public List<PositionDto> ListPositions { get; set; }
        public IReadOnlyList<RoleDto> Roles { get; set; }

        public bool UserIsInRole(RoleDto role)
        {
            if (Employee.Id == 0)
                return false;
            return User.Roles != null && User.Roles.Any(r => r == role.Name);
        }
    }
}