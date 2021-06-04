using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using DTT.LRM.BookFields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.BookClassifies.Dto
{
    [AutoMapFrom(typeof(BookClassify))]
    public class BookClassifyDto : AuditedEntityDto
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public virtual ICollection<BookField> Children { get; set; }
    }
}
