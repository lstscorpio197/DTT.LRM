using Abp.Application.Services;
using DTT.LRM.GiveBacks.Dto;
using DTT.LRM.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.GiveBacks
{
    public interface IGiveBackAppService : IApplicationService
    {
        Task<PagedResultExtendDto<GiveBackDto>> GetAll(PagedResultRequestExtendDto input);
        Task<int> CreateOrUpdateAsync(CreateOrUpdateGiveBackDto input);
        Task<GiveBackDto> GetById(int id);
        Task DeleteById(int id);
        Task<string> AutoGetCode();
    }
}
