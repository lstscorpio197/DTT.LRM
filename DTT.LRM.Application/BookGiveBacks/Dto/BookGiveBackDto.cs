using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using DTT.LRM.Books;
using DTT.LRM.Violates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.BookGiveBacks.Dto
{
    [AutoMapFrom(typeof(BookGiveBack))]
    public class BookGiveBackDto : AuditedEntityDto
    {
        public int GiveBackId { get; set; }
        public Book Book { get; set; }
        public int BookId { get; set; }
        public DateTime? GiveBackDate { get; set; }
        public string Note { get; set; }
        public int Status { get; set; }
        public Violate Violate { get; set; }
        public int? ViolateId { get; set; }

        public string StatusText
        {
            get
            {
                if (Status == 1)
                    return "Vi phạm";
                return string.Empty;
            }
        }
    }
}
