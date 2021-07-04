using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using DTT.LRM.BookCategories;
using DTT.LRM.Books;
using DTT.LRM.Publishers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.BookBorrows.Dto
{
    [AutoMapFrom(typeof(BookBorrow))]
    public class BookBorrowDto : AuditedEntityDto
    {
        public int BorrowId { get; set; }
        public Book Book { get; set; }
        public int? BookId { get; set; }

        public BookCategory BookCategory { get; set; }
        public int? BookCategoryId { get; set; }
        public string Author { get; set; }
        public int? ReleaseYear { get; set; }

        public Publisher Publisher { get; set; }
        public int? PublisherId { get; set; }
        public DateTime? GiveBackDate { get; set; }
    }
}
