using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DTT.LRM.Readers.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.Readers
{
    public interface IReaderAppService : IApplicationService
    {
        Task<PagedResultDto<ReaderDto>> GetAll(PagedResultRequestDto input);
        Task<int> CreateOrUpdateAsync(CreateOrUpdateReaderDto input);
        Task<ReaderDto> GetById(int id);
        Task DeleteById(int id);
    }
}
