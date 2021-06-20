using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DTT.LRM.Positions.Dto;
using DTT.LRM.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.Positions
{
    public interface IPositionAppService : IApplicationService
    {
        Task<PagedResultExtendDto<PositionDto>> GetAll(PagedResultRequestExtendDto input);
        Task<List<PositionDto>> GetAllForSelect();
        Task<int> CreateOrUpdateAsync(CreateOrUpdatePositionDto input);
        Task<PositionDto> GetById(int id);
        Task DeleteById(int id);
    }
}
