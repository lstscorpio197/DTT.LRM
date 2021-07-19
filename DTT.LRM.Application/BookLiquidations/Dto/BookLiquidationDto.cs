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

namespace DTT.LRM.BookLiquidations.Dto
{
    [AutoMapFrom(typeof(BookLiquidation))]
    public class BookLiquidationDto : AuditedEntityDto
    {
        public int LiquidationId { get; set; }
        public BookCategory BookCategory { get; set; }
        public int BookCategorieId { get; set; }

        public string Author { get; set; }
        public int? ReleaseYear { get; set; }
        public Publisher Publisher { get; set; }
        public int? PublisherId { get; set; }
        public decimal LiquidationPrice { get; set; }
        public string PublisherName => Publisher != null ? Publisher.Name : string.Empty;
    }
}
