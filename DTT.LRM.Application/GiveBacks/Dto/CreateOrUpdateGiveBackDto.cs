using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.GiveBacks.Dto
{
    [AutoMapTo(typeof(GiveBack))]
    public class CreateOrUpdateGiveBackDto : AuditedEntityDto
    {
        public string Code { get; set; }
        public int ReaderId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime GiveBackDate { get; set; }
        public string Note { get; set; }
    }
}
