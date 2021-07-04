using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using DTT.LRM.Books;
using DTT.LRM.Readers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.BookReaderUsings.Dto
{
    [AutoMapFrom(typeof(BookReaderUsing))]
    public class BookReaderUsingDto : AuditedEntityDto
    {
        public Reader Reader { get; set; }
        public int ReaderId { get; set; }
        public Book Book { get; set; }
        public int? BookId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? GiveBackDate { get; set; }
        public DateTime? GiveBackDateExpect { get; set; }
        public int IsUse { get; set; }

        public int OutOfDate
        {
            get
            {
                if (GiveBackDateExpect == null)
                    return 0;
                return DateTime.Now.Subtract(GiveBackDateExpect.Value).Days;
            }
        }

        public string BorrowDateText => BorrowDate.ToString("yyyy-MM-dd");
        public string GiveBackDateExpectText
        {
            get
            {
                if (GiveBackDateExpect != null)
                {
                    return GiveBackDateExpect.Value.ToString("yyyy-MM-dd");
                }
                else
                    return BorrowDate.AddDays(7).ToString("yyyy-MM-dd");
            }
        }
    }
}
