using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.Borrows.Dto
{
    [AutoMapTo(typeof(Borrow))]
    public class CreateOrUpdateBorrowDto : AuditedEntityDto
    {
        public string Code { get; set; }
        public string Creator { get; set; }
        public int ReaderId { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime BorrowDate { get; set; }
        public string Note { get; set; }
        public int Status { get; set; }
    }
}
