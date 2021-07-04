using Abp.Domain.Entities.Auditing;
using DTT.LRM.Employees;
using DTT.LRM.Readers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.Borrows
{
    [Table("Borrow")]
    public class Borrow : FullAuditedEntity
    {
        [Required]
        public string Code { get; set; }

        public string Creator { get; set; }

        [ForeignKey("ReaderId")]
        public Reader Reader { get; set; }
        public int ReaderId { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime BorrowDate { get; set; }
        public string Note { get; set; }
        public int Status { get; set; }
    }
}
