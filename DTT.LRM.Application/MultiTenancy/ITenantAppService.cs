using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DTT.LRM.MultiTenancy.Dto;

namespace DTT.LRM.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}
