using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DTT.LRM.BookFields.Dto;
using DTT.LRM.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.BookFields
{
    public interface IBookFieldAppService : IApplicationService
    {
        Task<PagedResultExtendDto<BookFieldDto>> GetAll(PagedResultRequestExtendDto input);
        Task<int> CreateOrUpdateAsync(CreateOrUpdateBookFieldDto input);
        Task<BookFieldDto> GetById(int id);
        Task DeleteById(int id);
        Task<List<BookFieldDto>> GetAllForSelect();
        Task<List<BookFieldDto>> GetListForSelectByBookClassifyId(int bookClassifyId);
    }
}
