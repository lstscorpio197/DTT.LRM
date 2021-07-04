using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DTT.LRM.BookCategories.Dto;
using DTT.LRM.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.BookCategories
{
    public interface IBookCategoryAppService : IApplicationService
    {
        Task<PagedResultExtendDto<BookCategoryDto>> GetAll(PagedResultRequestExtendDto input);
        Task<int> CreateOrUpdateAsync(CreateOrUpdateBookCategoryDto input);
        Task<BookCategoryDto> GetById(int id);
        Task DeleteById(int id);
        Task<List<BookCategoryDto>> GetListForSelectByBookFieldId(int bookFieldId);
        Task<List<BookCategoryDto>> GetAllForSelect();
        Task<IdAndCodeResultDto> CreateNewBookCategoryWithName(CreateOrUpdateBookCategoryDto input);
    }
}
