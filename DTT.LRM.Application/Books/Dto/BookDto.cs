using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using DTT.LRM.BookCategories;
using DTT.LRM.Publishers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.Books.Dto
{
    [AutoMapFrom(typeof(Book))]
    public class BookDto : AuditedEntityDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public BookCategory BookCategory { get; set; }
        public int BookCategoryId { get; set; }
        public Publisher Publisher { get; set; }
        public int PublisherId { get; set; }
        public string Language { get; set; }
        public string Author { get; set; }
        public int ReleaseYear { get; set; }
        public int PageCount { get; set; }
        public decimal Price { get; set; }
        public string Note { get; set; }
        public int Status { get; set; }
        public string PublisherName => Publisher.Name.ToString();
        public string StatusText
        {
            get
            {
                if (Status == (int)LRMEnum.BookStatus.UnUsed)
                    return "Chưa sử dụng";
                if (Status == (int)LRMEnum.BookStatus.Using)
                    return "Đang được mượn";
                if (Status == (int)LRMEnum.BookStatus.Lost)
                    return "Đã bị mất";
                if (Status == (int)LRMEnum.BookStatus.Liquidated)
                    return "Đã thanh lý";
                return string.Empty;
            }
        }
    }
}
