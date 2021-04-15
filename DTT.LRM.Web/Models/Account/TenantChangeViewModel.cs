using Abp.AutoMapper;
using DTT.LRM.Sessions.Dto;

namespace DTT.LRM.Web.Models.Account
{
    [AutoMapFrom(typeof(GetCurrentLoginInformationsOutput))]
    public class TenantChangeViewModel
    {
        public TenantLoginInfoDto Tenant { get; set; }
    }
}