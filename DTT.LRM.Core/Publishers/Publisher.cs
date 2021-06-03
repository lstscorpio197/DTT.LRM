using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.Publishers
{
    [Table("Publisher")]
    public class Publisher : FullAuditedEntity
    {
        [Required]
        public int Code { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public bool Status { get; set; }
    }
}
