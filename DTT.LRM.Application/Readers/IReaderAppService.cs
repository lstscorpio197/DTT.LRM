using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DTT.LRM.Readers.Dto;
using DTT.LRM.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.Readers
{
    public interface IReaderAppService : IApplicationService
    {
        Task<PagedResultExtendDto<ReaderDto>> GetAll(PagedResultRequestExtendDto input);
        Task<int> CreateOrUpdateAsync(CreateOrUpdateReaderDto input);
        Task<ReaderDto> GetById(int id);
        Task DeleteById(int id);
        Task<string> CodeIsExist(string code, int id);
        Task<string> EmailIsExist(string email, int id);
        Task<List<ReaderDto>> GetAllForSelect();
    }
}
