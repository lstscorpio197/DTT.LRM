using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Organizations;
using DTT.LRM.OrganizationUnits.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<OrganizationUnitDto> GetById(long id)
        {
            var org = await _organizationUnitRepository.GetAsync(id);
            return ObjectMapper.Map<OrganizationUnitDto>(org);
        }
    }
}
