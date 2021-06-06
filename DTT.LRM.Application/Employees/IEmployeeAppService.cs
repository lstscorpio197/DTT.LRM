using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DTT.LRM.Employees.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.Employees
{
    public interface IEmployeeAppService : IApplicationService
    {
        Task<PagedResultDto<EmployeeDto>> GetAll(PagedResultRequestDto input);
        Task<int> CreateOrUpdateAsync(CreateOrUpdateEmployeeDto input);
        Task<EmployeeDto> GetById(int id);
        Task DeleteById(int id);
    }
}
