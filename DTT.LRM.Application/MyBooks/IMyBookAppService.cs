using Abp.Application.Services;
using DTT.LRM.MyBooks.Dto;
using DTT.LRM.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.MyBooks
{
    public interface IMyBookAppService : IApplicationService
    {
        Task<PagedResultExtendDto<MyBookDto>> GetAll(PagedResultRequestExtendDto input);
    }
}
