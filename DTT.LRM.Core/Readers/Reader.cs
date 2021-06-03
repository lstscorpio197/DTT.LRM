using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using DTT.LRM.Authorization.Users;
using DTT.LRM.Positions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.Readers
{
    [Table("Reader")]
    public class Reader : FullAuditedEntity
    {
        [Required]
        [MaxLength(20)]
        public string Code { get; set; }

        [Required]
        [MaxLength(32)]
        public string Name { get; set; }

        [ForeignKey("OrganizationUnitId")]
        public OrganizationUnit OrganizationUnit { get; set; }
        public long OrganizationUnitId { get; set; }

        [ForeignKey("PositionId")]
        public Position Position { get; set; }
        public int PositionId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
        public long UserId { get; set; }
        public DateTime DayOfBirth { get; set; }
        public int Gender { get; set; }
        public string Address { get; set; }

        [MaxLength(15)]
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }
        public bool Status { get; set; }
    }
}
