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
        public string CodeText => Code.ToString("D3");
        public string Name { get; set; }
        public BookField BookField { get; set; }
        public int BookFieldId { get; set; }

        public bool Status { get; set; }
        public string StatusText
        {
            get
            {
                if (Status)
                    return "Đang sử dụng";
                else
                    return "Không sử dụng";
            }
        }
    }
}
