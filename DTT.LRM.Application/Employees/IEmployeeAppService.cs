using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DTT.LRM.Employees.Dto;
using DTT.LRM.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.Employees
{
    public interface IEmployeeAppService : IApplicationService
    {
        Task<PagedResultExtendDto<EmployeeDto>> GetAll(PagedResultRequestExtendDto input);
        Task<int> CreateOrUpdateAsync(CreateOrUpdateEmployeeDto input);
        Task<EmployeeDto> GetById(int id);
        Task DeleteById(int id);
        Task<string> CodeIsExist(string code, int id);
        Task<string> EmailIsExist(string email, int id);
        Task<EmployeeDto> GetByUserId(long userId);
    }
}
