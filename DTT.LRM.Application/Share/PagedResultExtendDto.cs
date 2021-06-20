using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.Share
{
    public class PagedResultExtendDto<T> : PagedResultDto<T>
    {
        public PagedResultExtendDto()
        {

        }
        public PagedResultExtendDto(int totalCount, IReadOnlyList<T> items, List<CountStatus> countStatus = null)
        {
            TotalCount = totalCount;
            Items = items;
            CountStatus = countStatus;
        }
        public List<CountStatus> CountStatus { get; set; }

    }
}
