using Abp.Application.Services;
using DTT.LRM.PositionQuotas.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.PositionQuotas
{
    public interface IPositionQuotaAppService : IApplicationService
    {
        Task<List<PositionQuotaDto>> GetAllByPositionId(int positionId);
        Task<int> CreateOrUpdateAsync(List<CreateOrUpdatePositionQuotaDto> input, int positionId);
    }
}
