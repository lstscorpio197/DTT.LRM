using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.BookCategories.Dto
{
    [AutoMapTo(typeof(BookCategory))]
    public class CreateOrUpdateBookCategoryDto : AuditedEntityDto
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public int BookFieldId { get; set; }
        public int TotalBorrowTime { get; set; }
        public bool Status { get; set; }
    }
}
