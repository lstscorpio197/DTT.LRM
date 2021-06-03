using Abp.Domain.Entities.Auditing;
using DTT.LRM.BookFields;
using DTT.LRM.Positions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.PositionQuotas
{
    [Table("PositionQuota")]
    public class PositionQuota : FullAuditedEntity
    {
        [ForeignKey("PositionId")]
        public Position Position { get; set; }
        public int PositionId { get; set; }

        [ForeignKey("BookFieldId")]
        public BookField BookField { get; set; }
        public int BookFieldId { get; set; }
        public int Amount { get; set; }
    }
}
