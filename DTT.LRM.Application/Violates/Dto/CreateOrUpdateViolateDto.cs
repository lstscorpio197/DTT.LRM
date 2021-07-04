using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.Violates.Dto
{
    [AutoMapTo(typeof(Violate))]
    public class CreateOrUpdateViolateDto : AuditedEntityDto
    {
        public int ReaderId { get; set; }

        public int BookId { get; set; }

        public int ViolateError { get; set; }
        public decimal Money { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
