using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DTT.LRM.Positions.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.Positions
{
    public interface IPositionAppService : IApplicationService
    {
        Task<PagedResultDto<PositionDto>> GetAll(PagedResultRequestDto input);
        Task<int> CreateOrUpdateAsync(CreateOrUpdatePositionDto input);
        Task<PositionDto> GetById(int id);
        Task DeleteById(int id);
    }
}
