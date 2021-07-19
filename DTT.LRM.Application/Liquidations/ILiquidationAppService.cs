using Abp.Application.Services;
using DTT.LRM.Liquidations.Dto;
using DTT.LRM.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.Liquidations
{
    public interface ILiquidationAppService : IApplicationService
    {
        Task<PagedResultExtendDto<LiquidationDto>> GetAll(PagedResultRequestExtendDto input);
        Task<int> CreateOrUpdateAsync(CreateLiquidationDto input);
        Task<LiquidationDto> GetById(int id);
        Task<string> AutoGetCode();
    }
}
