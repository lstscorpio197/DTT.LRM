using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Authorization.Roles;
using Abp.AutoMapper;
using DTT.LRM.Authorization.Roles;

namespace DTT.LRM.Roles.Dto
{
    [AutoMapFrom(typeof(Role))]
    public class RoleDto : EntityDto<int>
    {
        [Required]
        [StringLength(AbpRoleBase.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(AbpRoleBase.MaxDisplayNameLength)]
        public string DisplayName { get; set; }

        [StringLength(Role.MaxDescriptionLength)]
        public string Description { get; set; }

        public bool? IsDefault { get; set; }
        public string IsDefaultText
        {
            get
            {
                if (IsDefault.Value)
                    return "fa fa-check";
                else
                    return "fa fa-ban";
            }
        }
        public bool IsStatic { get; set; }

        public List<string> GrantedPermissions { get; set; }
    }
}
