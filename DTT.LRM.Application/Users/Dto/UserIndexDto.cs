using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using DTT.LRM.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.Users.Dto
{
    [AutoMapFrom(typeof(User))]
    public class UserIndexDto : AuditedEntityDto
    {
        public string UserName { get; set; }
        public string Name { get; set; }

        public string Surname { get; set; }
        public string EmailAddress { get; set; }

        public bool IsActive { get; set; }

        public string FullName { get; set; }

        public string Password { get; set; }

        public DateTime? LastLoginTime { get; set; }

        public DateTime CreationTime { get; set; }

        public string[] Roles { get; set; }

        public string IsActiveIcon
        {
            get
            {
                if (IsActive)
                    return "fa fa-check";
                else
                    return "fa fa-ban";
            }
        }

    }
}
