using Abp.Domain.Entities.Auditing;
using DTT.LRM.Books;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.BookLiquidations
{
    [Table("BookLiquidation")]
    public class BookLiquidation : FullAuditedEntity
    {
        public int LiquidationId { get; set; }
        [ForeignKey("BookId")]
        public Book Book { get; set; }
        public int BookId { get; set; }
        public decimal LiquidationPrice { get; set; }
    }
}
