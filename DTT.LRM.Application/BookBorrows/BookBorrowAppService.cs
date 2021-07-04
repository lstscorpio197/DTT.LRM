using Abp.Domain.Repositories;
using DTT.LRM.BookBorrows.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.BookBorrows
{
    public class BookBorrowAppService : LRMAppServiceBase, IBookBorrowAppService
    {
        private readonly IRepository<BookBorrow> _bookBorrowRepository;
        public BookBorrowAppService(IRepository<BookBorrow> bookBorrowRepository)
        {
            _bookBorrowRepository = bookBorrowRepository;
        }

        public async Task<int> CreateOrUpdateAsync(List<CreateOrUpdateBookBorrowDto> input, int borrowId)
        {
            try
            {
                var listIds = input.Select(x => x.Id);
                await _bookBorrowRepository.DeleteAsync(x => x.BorrowId == borrowId && listIds.Contains(x.Id) == false);
                foreach (var item in input)
                {
                    item.BorrowId = borrowId;
                    var obj = ObjectMapper.Map<BookBorrow>(item);
                    await _bookBorrowRepository.InsertOrUpdateAndGetIdAsync(obj);
                }
                return 1;
            }
            catch (Exception)
            {
                return -1;
                throw;
            }
        }
        public async Task<List<BookBorrowDto>> GetAllByBorrowId(int borrowId)
        {
            var listEntities = _bookBorrowRepository.GetAllIncluding(x=>x.Book).Where(x => x.BorrowId == borrowId).ToList();
            return ObjectMapper.Map<List<BookBorrowDto>>(listEntities);
        }
    }
}
