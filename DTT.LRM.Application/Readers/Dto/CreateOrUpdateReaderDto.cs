using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.Readers.Dto
{
    [AutoMapFrom(typeof(Reader))]
    public class CreateOrUpdateReaderDto : AuditedEntityDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public long OrganizationUnitId { get; set; }
        public int PositionId { get; set; }
        public long UserId { get; set; }
        public DateTime DayOfBirth { get; set; }
        public int Gender { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }
        public bool Status { get; set; }
    }
}
