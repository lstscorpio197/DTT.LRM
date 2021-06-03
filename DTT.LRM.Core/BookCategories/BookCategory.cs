using Abp.Domain.Entities.Auditing;
using DTT.LRM.BookFields;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.BookCategories
{
    [Table("BookCategory")]
    public class BookCategory : FullAuditedEntity
    {
        [Required]
        public int Code { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [ForeignKey("BookFieldId")]
        public BookField BookField { get; set; }
        public int BookFieldId { get; set; }

        public bool Status { get; set; }
    }
}
