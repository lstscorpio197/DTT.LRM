using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using DTT.LRM.Authorization.Users;
using DTT.LRM.Users;

namespace DTT.LRM.Sessions.Dto
{
    [AutoMapFrom(typeof(User))]
    public class UserLoginInfoDto : EntityDto<long>
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string UserName { get; set; }

        public string EmailAddress { get; set; }
    }
}
