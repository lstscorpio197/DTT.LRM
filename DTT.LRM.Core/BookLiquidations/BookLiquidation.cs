using Abp.Domain.Entities.Auditing;
using DTT.LRM.BookCategories;
using DTT.LRM.Books;
using DTT.LRM.Publishers;
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
        [ForeignKey("BookCategorieId")]
        public BookCategory BookCategorie { get; set; }
        public int BookCategorieId { get; set; }

        public string Author { get; set; }
        public int? ReleaseYear { get; set; }

        [ForeignKey("PublisherId")]
        public Publisher Publisher { get; set; }
        public int? PublisherId { get; set; }
        public decimal LiquidationPrice { get; set; }
    }
}
