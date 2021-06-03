using Abp.Domain.Entities.Auditing;
using DTT.LRM.BookCategories;
using DTT.LRM.BookClassifies;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.BookFields
{
    [Table("BookField")]
    public class BookField : FullAuditedEntity
    {
        [Required]
        public int Code { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [ForeignKey("BookClassifyId")]
        public BookClassify BookClassify { get; set; }
        public int BookClassifyId { get; set; }

        public bool Status { get; set; }
        public virtual ICollection<BookCategory> Children { get; set; }
    }
}
