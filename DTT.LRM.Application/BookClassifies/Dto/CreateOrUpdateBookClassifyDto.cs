using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.BookClassifies.Dto
{
    [AutoMapTo(typeof(BookClassify))]
    public class CreateOrUpdateBookClassifyDto : AuditedEntityDto
    {
        [Required]
        public int Code { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public bool Status { get; set; }
    }
}
