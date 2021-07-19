using Abp.AutoMapper;
using DTT.LRM.BookReaderUsings;
using DTT.LRM.Books;
using DTT.LRM.Readers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.SearchInfos.Dto
{
    [AutoMapFrom(typeof(BookReaderUsing))]
    public class SearchInfoDto
    {
        public Reader Reader { get; set; }
        public int ReaderId { get; set; }
        public Book Book { get; set; }
        public int? BookId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? GiveBackDate { get; set; }
        public DateTime? GiveBackDateExpect { get; set; }
        public int IsUse { get; set; }

        public string BookCode { get; set; }
        public string BookName { get; set; }
        public string ReaderCode { get; set; }
        public string ReaderName { get; set; }
        public string BorrowDateText => BorrowDate.ToString("dd/MM/yyyy");
        public string GiveBackDateText
        {
            get
            {
                return GiveBackDate != null ? GiveBackDate.Value.ToString("dd/MM/yyyy"):string.Empty;
            }
        }
        public string GiveBackDateExpectText => GiveBackDateExpect.HasValue ? GiveBackDateExpect.Value.ToString("dd/MM/yyyy") : string.Empty;
        public string StatusText
        {
            get
            {
                if (IsUse == (int)LRMEnum.BookStatus.Using)
                    return "Đang sử dụng";
                return "Đã trả";
            }
        }
    }
}
