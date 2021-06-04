using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DTT.LRM.BookClassifies.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.BookClassifies
{
    public interface IBookClassifyAppService : IApplicationService
    {
        Task<PagedResultDto<BookClassifyDto>> GetAll(PagedResultRequestDto input);
        Task<int> CreateOrUpdateAsync(CreateOrUpdateBookClassifyDto input);
        Task<BookClassifyDto> GetById(int id);
    }
}
