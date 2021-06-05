using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Organizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.OrganizationUnits.Dto
{
    [AutoMapTo(typeof(OrganizationUnit))]
    public class CreateOrUpdateOrganizationUnitDto : AuditedEntityDto<long>
    {
        public string Code { get; set; }
        public long? ParentId { get; set; }
        public int? TenantId { get; set; }
        public string DisplayName { get; set; }
    }
}
