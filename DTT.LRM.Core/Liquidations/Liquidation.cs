using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.Liquidations
{
    [Table("Liquidation")]
    public class Liquidation : FullAuditedEntity
    {
        public string Code { get; set; }
        public string Creator { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
