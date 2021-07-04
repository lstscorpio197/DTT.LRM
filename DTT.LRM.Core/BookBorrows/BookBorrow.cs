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

namespace DTT.LRM.BookBorrows
{
    [Table("BookBorrow")]
    public class BookBorrow : FullAuditedEntity
    {
        public int BorrowId { get; set; }
        [ForeignKey("BookId")]
        public Book Book { get; set; }
        public int? BookId { get; set; }

        [ForeignKey("BookCategoryId")]
        public BookCategory BookCategory { get; set; }
        public int? BookCategoryId { get; set; }
        public string Author { get; set; }
        public int? ReleaseYear { get; set; }

        [ForeignKey("PublisherId")]
        public Publisher Publisher { get; set; }
        public int? PublisherId { get; set; }
        public DateTime? GiveBackDate { get; set; }
    }
}
