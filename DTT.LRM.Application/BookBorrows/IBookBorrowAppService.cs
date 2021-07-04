using Abp.Application.Services;
using DTT.LRM.BookBorrows.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.BookBorrows
{
    public interface IBookBorrowAppService : IApplicationService
    {
        Task<int> CreateOrUpdateAsync(List<CreateOrUpdateBookBorrowDto> input, int borrowId);
        Task<List<BookBorrowDto>> GetAllByBorrowId(int borrowId);
    }
}
