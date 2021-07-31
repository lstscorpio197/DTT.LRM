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
        Task<List<BookDto>> GetListBookForBorrow(AdvanceSearchBookDto input);
        Task<int> UpdateBookStatus(List<int> listBookIds, int status);
        Task<int> UpdateBookStatusLiquidation(int bookCategotyId, string author, int releaseYear, int publisherId);
        Task<int> CountBookBorrowByCategoryId(int bookCategotyId);
    }
}
