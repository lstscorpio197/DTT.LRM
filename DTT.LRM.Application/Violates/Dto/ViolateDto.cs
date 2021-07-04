using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using DTT.LRM.Books;
using DTT.LRM.Readers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.Violates.Dto
{
    [AutoMapFrom(typeof(Violate))]
    public class ViolateDto : AuditedEntityDto
    {
        public Reader Reader { get; set; }
        public int ReaderId { get; set; }

        public Book Book { get; set; }
        public int BookId { get; set; }

        public int ViolateError { get; set; }
        public decimal Money { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
