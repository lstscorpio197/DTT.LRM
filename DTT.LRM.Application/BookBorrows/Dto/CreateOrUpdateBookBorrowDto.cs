using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.BookBorrows.Dto
{
    [AutoMapTo(typeof(BookBorrow))]
    public class CreateOrUpdateBookBorrowDto : AuditedEntityDto
    {
        public int BorrowId { get; set; }
        public int? BookId { get; set; }
        public int? BookCategoryId { get; set; }
        public string Author { get; set; }
        public int? ReleaseYear { get; set; }
        public int? PublisherId { get; set; }
        public DateTime? GiveBackDate { get; set; }
    }
}
