using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.BookGiveBacks.Dto
{
    [AutoMapTo(typeof(BookGiveBack))]
    public class CreateOrUpdateBookGiveBackDto : AuditedEntityDto
    {
        public int GiveBackId { get; set; }
        public int BookId { get; set; }
        public DateTime? GiveBackDate { get; set; }
        public string Note { get; set; }
        public int Status { get; set; }
        public int? ViolateId { get; set; }
    }
}
