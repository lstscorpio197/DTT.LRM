using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DTT.LRM.Books.Dto;
using DTT.LRM.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.Books
{
    public interface IBookAppService : IApplicationService
    {
        Task<PagedResultExtendDto<BookDto>> GetAll(PagedResultRequestExtendDto input);
        Task<int> CreateOrUpdateAsync(CreateOrUpdateBookDto input);
        Task<BookDto> GetById(int id);
        Task DeleteById(int id);
    }
}
