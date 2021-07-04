using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using DTT.LRM.Employees;
using DTT.LRM.Readers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.GiveBacks.Dto
{
    [AutoMapFrom(typeof(GiveBack))]
    public class GiveBackDto : AuditedEntityDto
    {
        public string Code { get; set; }
        public Reader Reader { get; set; }
        public int ReaderId { get; set; }
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }
        public DateTime GiveBackDate { get; set; }
        public string Note { get; set; }

        public string GiveBackDateText => GiveBackDate.ToString("dd/MM/yyyy");
        public string ReaderName
        {
            get
            {
                if (Reader != null)
                    return Reader.Name;
                return string.Empty;
            }
        }

        public string EmployeeName
        {
            get
            {
                if (Employee != null)
                    return Employee.Name;
                return string.Empty;
            }
        }
    }
}
