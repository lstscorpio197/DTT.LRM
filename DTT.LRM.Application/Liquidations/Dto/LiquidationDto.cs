using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.Liquidations.Dto
{
    [AutoMapFrom(typeof(Liquidation))]
    public class LiquidationDto : AuditedEntityDto
    {
        public string Code { get; set; }
        public string Creator { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateDateText => CreationTime.ToString("dd/MM/yyyy");
    }
}
