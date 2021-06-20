using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Organizations;
using DTT.LRM.OrganizationUnits.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using DTT.LRM.Share;

namespace DTT.LRM.OrganizationUnits
{
    public class OrganizationUnitAppService : LRMAppServiceBase, IOrganizationUnitAppService
    {
        private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;
        public OrganizationUnitAppService(IRepository<OrganizationUnit, long> organizationUnitRepository)
        {
            _organizationUnitRepository = organizationUnitRepository;
        }

        public async Task<long> CreateOrUpdateAsync(CreateOrUpdateOrganizationUnitDto input)
        {
            var check = await _organizationUnitRepository.FirstOrDefaultAsync(x => x.Code == input.Code && x.Id != input.Id);
            if (check != null)
                return 0;
            var obj = ObjectMapper.Map<OrganizationUnit>(input);
            return await _organizationUnitRepository.InsertOrUpdateAndGetIdAsync(obj);
        }

        public async Task DeleteById(long id)
        {
            var org = await _organizationUnitRepository.GetAsync(id);
            await _organizationUnitRepository.DeleteAsync(org);
        }

        public async Task<ListResultDto<OrganizationUnitDto>> GetAll()
        {
            var listOrg = await _organizationUnitRepository.GetAllListAsync();
            return ObjectMapper.Map<ListResultDto<OrganizationUnitDto>>(listOrg);
        }

        public async Task<PagedResultExtendDto<OrganizationUnitBasicDto>> GetAllForDatatable(PagedResultRequestExtendDto input)
        {
            var listPositions = _organizationUnitRepository.GetAll();
            var items = listPositions.OrderBy("id DESC").PageBy(input).ToList();
            var listItems = ObjectMapper.Map<List<OrganizationUnitBasicDto>>(items);
            return new PagedResultExtendDto<OrganizationUnitBasicDto>(totalCount: listPositions.Count(), items: listItems, countStatus: null);
        }

        public async Task<List<OrganizationUnitBasicDto>> GetAllForSelect()
        {
            var listPositions = await _organizationUnitRepository.GetAllListAsync();
            return ObjectMapper.Map<List<OrganizationUnitBasicDto>>(listPositions);
        }

        public async Task<OrganizationUnitBasicDto> GetById(long id)
        {
            var org = await _organizationUnitRepository.GetAsync(id);
            return ObjectMapper.Map<OrganizationUnitBasicDto>(org);
        }
    }
}
