using Abp.Application.Services;
using DTT.LRM.SearchInfos.Dto;
using DTT.LRM.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.SearchInfos
{
    public interface ISearchInfoAppService : IApplicationService
    {
        Task<PagedResultExtendDto<SearchInfoDto>> GetAll(PagedResultRequestExtendDto input);
    }
}
