using Abp.Domain.Entities.Auditing;
using DTT.LRM.BookCategories;
using DTT.LRM.Publishers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.Books
{
    public class Book : FullAuditedEntity
    {
        [Required]
        public string Code { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [ForeignKey("BookCategoryId")]
        public BookCategory BookCategory { get; set; }
        public int BookCategoryId { get; set; }

        [ForeignKey("PublisherId")]
        public Publisher Publisher { get; set; }
        public int PublisherId { get; set; }

        [MaxLength(100)]
        public string Language { get; set; }

        public string Author { get; set; }
        public int ReleaseYear { get; set; }
        public int PageCount { get; set; }
        public decimal Price { get; set; }
        public string Note { get; set; }
        public int Status { get; set; }
    }
}
