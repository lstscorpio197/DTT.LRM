using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.Publishers.Dto
{
    [AutoMapFrom(typeof(Publisher))]
    public class PublisherDto : AuditedEntityDto
    {
        public int Code { get; set; }

        public string Name { get; set; }

        public bool Status { get; set; }
    }
}
