using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using DTT.LRM.BookClassifies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.BookFields.Dto
{
    [AutoMapFrom(typeof(BookField))]
    public class BookFieldDto : AuditedEntityDto
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public BookClassify BookClassify { get; set; }
        public int BookClassifyId { get; set; }

        public bool Status { get; set; }
    }
}
