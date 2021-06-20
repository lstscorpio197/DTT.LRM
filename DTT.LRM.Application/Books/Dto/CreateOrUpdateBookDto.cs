using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.Books.Dto
{
    [AutoMapTo(typeof(Book))]
    public class CreateOrUpdateBookDto : AuditedEntityDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int BookCategoryId { get; set; }
        public int PublisherId { get; set; }
        public string Language { get; set; }
        public string Author { get; set; }
        public int ReleaseYear { get; set; }
        public int PageCount { get; set; }
        public decimal Price { get; set; }
        public string Note { get; set; }
        public int Status { get; set; }
        public int? Amount { get; set; }
    }
}
