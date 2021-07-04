using Abp.Application.Services;
using DTT.LRM.BookBorrows.Dto;
using DTT.LRM.BookReaderUsings.Dto;
using DTT.LRM.Books.Dto;
using DTT.LRM.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.BookReaderUsings
{
    public interface IBookReaderUsingAppService : IApplicationService
    {
        Task<int> CreateOrUpdateAsync(List<CreateOrUpdateBookReaderUsingDto> input, int readerId);
        Task<int> CreateByListBookIdAndReaderId(List<CreateOrUpdateBookBorrowDto> listBook, int readerId);
        Task<List<BookReaderUsingDto>> GetBookUsingByReaderId(int readerId);
        Task UpdateGiveBackBook(List<int> listBookIds, int readerId);
    }
}
