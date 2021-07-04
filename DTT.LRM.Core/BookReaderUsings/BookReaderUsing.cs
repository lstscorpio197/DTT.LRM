using Abp.Domain.Entities.Auditing;
using DTT.LRM.Books;
using DTT.LRM.Readers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.BookReaderUsings
{
    [Table("BookReaderUsing")]
    public class BookReaderUsing : FullAuditedEntity
    {
        [ForeignKey("ReaderId")]
        public Reader Reader { get; set; }
        public int ReaderId { get; set; }

        [ForeignKey("BookId")]
        public Book Book { get; set; }
        public int? BookId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? GiveBackDate { get; set; }
        public DateTime? GiveBackDateExpect { get; set; }
        public int IsUse { get; set; }
    }
}
