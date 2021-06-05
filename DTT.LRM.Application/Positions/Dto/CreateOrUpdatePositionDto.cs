using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.Positions.Dto
{
    [AutoMapTo(typeof(Position))]
    public class CreateOrUpdatePositionDto : AuditedEntityDto
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public bool Status { get; set; }
    }
}
