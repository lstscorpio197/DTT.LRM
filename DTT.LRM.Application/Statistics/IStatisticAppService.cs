using Abp.Application.Services;
using DTT.LRM.Share;
using DTT.LRM.Statistics.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.Statistics
{
    public interface IStatisticAppService : IApplicationService
    {
        Task<PagedResultExtendDto<StatisticDto>> GetAll(PagedResultRequestExtendDto input);
        Task<List<StatisticDto>> GetAllForExport(PagedResultRequestExtendDto input);
    }
}
