using Abp.Application.Services;
using DTT.LRM.BookLiquidations.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.BookLiquidations
{
    public interface IBookLiquidationAppService : IApplicationService
    {
        Task<int> CreateOrUpdateAsync(List<CreateBookLiquidationDto> input, int liquidationId);
        Task<List<BookLiquidationDto>> GetAllByLiquidationId(int liquidationId);
    }
}
