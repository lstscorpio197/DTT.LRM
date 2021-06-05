using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using DTT.LRM.BookFields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.BookCategories.Dto
{
    [AutoMapFrom(typeof(BookCategory))]
    public class BookCategoryDto : AuditedEntityDto
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public BookField BookField { get; set; }
        public int BookFieldId { get; set; }

        public bool Status { get; set; }
    }
}
