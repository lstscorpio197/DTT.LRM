using Abp.Application.Services.Dto;
using DTT.LRM.Violates.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.BookGiveBacks.Dto
{
    public class BookGiveBackAndViolateDto : AuditedEntityDto
    {
        public int GiveBackId { get; set; }
        public int BookId { get; set; }
        public DateTime? GiveBackDate { get; set; }
        public string Note { get; set; }
        public int Status { get; set; }
        public CreateOrUpdateViolateDto Violate { get; set; }
    }
}
