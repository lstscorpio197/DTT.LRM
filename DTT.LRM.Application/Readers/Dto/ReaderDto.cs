using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using DTT.LRM.Authorization.Users;
using DTT.LRM.Positions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.Readers.Dto
{
    [AutoMapFrom(typeof(Reader))]
    public class ReaderDto : AuditedEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public OrganizationUnit OrganizationUnit { get; set; }
        public long OrganizationUnitId { get; set; }
        public Position Position { get; set; }
        public int PositionId { get; set; }
        public User User { get; set; }
        public long UserId { get; set; }
        public DateTime DayOfBirth { get; set; }
        public int Gender { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }
        public bool Status { get; set; }
        public string StatusText
        {
            get
            {
                if (Status)
                    return "Đang sử dụng";
                else
                    return "Không sử dụng";
            }
        }
        public string OrganizationUnitName { get; set; }
        public string PositionName => Position?.Name;
    }
}
