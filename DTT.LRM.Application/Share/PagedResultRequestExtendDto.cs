using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.Share
{
    public class PagedResultRequestExtendDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}
