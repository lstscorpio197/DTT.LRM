using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using DTT.LRM.BookClassifies;
using DTT.LRM.Positions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.PositionQuotas.Dto
{
    [AutoMapFrom(typeof(PositionQuota))]
    public class PositionQuotaDto : AuditedEntityDto
    {
        public Position Position { get; set; }
        public int PositionId { get; set; }
        public BookClassify BookClassify { get; set; }
        public int BookClassifyId { get; set; }
        public int Amount { get; set; }
    }
}
