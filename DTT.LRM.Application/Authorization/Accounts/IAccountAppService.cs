using System.Threading.Tasks;
using Abp.Application.Services;
using DTT.LRM.Authorization.Accounts.Dto;

namespace DTT.LRM.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
