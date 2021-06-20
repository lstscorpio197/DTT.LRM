using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DTT.LRM.Roles.Dto;
using DTT.LRM.Share;

namespace DTT.LRM.Roles
{
    public interface IRoleAppService : IAsyncCrudAppService<RoleDto, int, PagedResultRequestDto, CreateRoleDto, RoleDto>
    {
        Task<ListResultDto<PermissionDto>> GetAllPermissions();
        Task<PagedResultExtendDto<RoleDto>> GetDataTable(PagedResultRequestExtendDto input);
        Task<bool> IsExist(string roleName, int roleId);
    }
}
