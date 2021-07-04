using Abp.Domain.Entities.Auditing;
using DTT.LRM.Books;
using DTT.LRM.Violates;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.BookGiveBacks
{
    [Table("BookGiveBack")]
    public class BookGiveBack : FullAuditedEntity
    {
        public int GiveBackId { get; set; }
        [ForeignKey("BookId")]
        public Book Book { get; set; }
        public int BookId { get; set; }
        public DateTime? GiveBackDate { get; set; }
        public string Note { get; set; }
        public int Status { get; set; }

        [ForeignKey("ViolateId")]
        public Violate Violate { get; set; }
        public int? ViolateId { get; set; }
    }
}
