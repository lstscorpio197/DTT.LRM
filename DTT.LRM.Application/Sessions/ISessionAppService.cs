using System.Threading.Tasks;
using Abp.Application.Services;
using DTT.LRM.Sessions.Dto;

namespace DTT.LRM.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
