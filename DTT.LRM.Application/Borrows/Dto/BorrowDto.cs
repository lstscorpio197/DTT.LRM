using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using DTT.LRM.Employees;
using DTT.LRM.Readers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.Borrows.Dto
{
    [AutoMapFrom(typeof(Borrow))]
    public class BorrowDto : AuditedEntityDto
    {
        public string Code { get; set; }

        public string Creator { get; set; }
        public Reader Reader { get; set; }
        public int ReaderId { get; set; }
        public Employee Employee { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime BorrowDate { get; set; }
        public string Note { get; set; }
        public int Status { get; set; }
        public string ReaderName => Reader.Name;
        public string BorrowDateText => BorrowDate.ToString("dd/MM/yyyy");
    }
}
