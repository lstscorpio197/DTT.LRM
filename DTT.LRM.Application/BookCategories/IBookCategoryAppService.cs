using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DTT.LRM.BookCategories.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.BookCategories
{
    public interface IBookCategoryAppService : IApplicationService
    {
        Task<PagedResultDto<BookCategoryDto>> GetAll(PagedResultRequestDto input);
        Task<int> CreateOrUpdateAsync(CreateOrUpdateBookCategoryDto input);
        Task<BookCategoryDto> GetById(int id);
        Task DeleteById(int id);
    }
}
