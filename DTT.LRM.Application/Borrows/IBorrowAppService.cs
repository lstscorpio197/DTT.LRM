using Abp.Application.Services;
using DTT.LRM.Borrows.Dto;
using DTT.LRM.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.Borrows
{
    public interface IBorrowAppService : IApplicationService
    {
        Task<PagedResultExtendDto<BorrowDto>> GetAll(PagedResultRequestExtendDto input);
        Task<int> CreateOrUpdateAsync(CreateOrUpdateBorrowDto input);
        Task<BorrowDto> GetById(int id);
        Task DeleteById(int id);
        Task<string> AutoGetCode();
    }
}
