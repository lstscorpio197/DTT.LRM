using Abp.Domain.Entities.Auditing;
using DTT.LRM.Books;
using DTT.LRM.Readers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.Violates
{
    [Table("Violate")]
    public class Violate : FullAuditedEntity
    {
        [ForeignKey("ReaderId")]
        public Reader Reader { get; set; }
        public int ReaderId { get; set; }

        [ForeignKey("BookId")]
        public Book Book { get; set; }
        public int BookId { get; set; }

        public int ViolateError { get; set; }
        public decimal Money { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
