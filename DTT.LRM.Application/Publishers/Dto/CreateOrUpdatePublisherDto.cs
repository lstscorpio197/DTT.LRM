using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.Publishers.Dto
{
    [AutoMapTo(typeof(Publisher))]
    public class CreateOrUpdatePublisherDto : AuditedEntityDto
    {
        public int Code { get; set; }

        public string Name { get; set; }

        public bool Status { get; set; }
    }
}
