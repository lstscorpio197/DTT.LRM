using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DTT.LRM.Roles.Dto;
using DTT.LRM.Users.Dto;

namespace DTT.LRM.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedResultRequestDto, CreateUserDto, UpdateUserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();
    }
}