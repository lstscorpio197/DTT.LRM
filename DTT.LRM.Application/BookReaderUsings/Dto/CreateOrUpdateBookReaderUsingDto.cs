using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.BookReaderUsings.Dto
{
    [AutoMapTo(typeof(BookReaderUsing))]
    public class CreateOrUpdateBookReaderUsingDto : AuditedEntityDto
    {
        public int ReaderId { get; set; }
        public int? BookId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? GiveBackDate { get; set; }
        public DateTime? GiveBackDateExpect { get; set; }
        public int IsUse { get; set; }
    }
}
