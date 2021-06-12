using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.PositionQuotas.Dto
{
    [AutoMapTo(typeof(PositionQuota))]
    public class CreateOrUpdatePositionQuotaDto : AuditedEntityDto
    {
        public int PositionId { get; set; }
        public int BookClassifyId { get; set; }
        public int Amount { get; set; }
    }
}
