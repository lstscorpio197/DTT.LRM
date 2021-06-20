using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DTT.LRM.OrganizationUnits.Dto;
using DTT.LRM.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.OrganizationUnits
{
    public interface IOrganizationUnitAppService : IApplicationService
    {
        Task<ListResultDto<OrganizationUnitDto>> GetAll();
        Task<PagedResultExtendDto<OrganizationUnitBasicDto>> GetAllForDatatable(PagedResultRequestExtendDto input);
        Task<List<OrganizationUnitBasicDto>> GetAllForSelect();
        Task<long> CreateOrUpdateAsync(CreateOrUpdateOrganizationUnitDto input);
        Task<OrganizationUnitBasicDto> GetById(long id);
        Task DeleteById(long id);
    }
}
