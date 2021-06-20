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
    [AutoMapFrom(typeof(OrganizationUnit))]
    public class OrganizationUnitBasicDto : AuditedEntityDto<long>
    {
        public string Code { get; set; }
        public string DisplayName { get; set; }
        public long? ParentId { get; set; }
    }
}
