using Abp.Domain.Repositories;
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
    public class BookReaderUsingAppService : LRMAppServiceBase, IBookReaderUsingAppService
    {
        private readonly IRepository<BookReaderUsing> _bookReaderUsingRepository;
        public BookReaderUsingAppService(IRepository<BookReaderUsing> bookReaderUsingRepository)
        {
            _bookReaderUsingRepository = bookReaderUsingRepository;
        }

        public async Task<int> CreateByListBookIdAndReaderId(List<CreateOrUpdateBookBorrowDto> listBook, int readerId)
        {
            try
            {
                foreach (var book in listBook)
                {
                    var bookId = book.BookId.Value;
                    var giveBackDateExpect = book.GiveBackDate;
                    var item = new BookReaderUsing();
                    item.BookId = bookId;
                    item.BorrowDate = DateTime.Now;
                    item.ReaderId = readerId;
                    item.GiveBackDateExpect = giveBackDateExpect;
                    item.IsUse = (int)LRMEnum.BookStatus.Using;
                    await _bookReaderUsingRepository.InsertOrUpdateAsync(item);
                }
                return 1;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return -1;
            }
        }

        public async Task<int> CreateOrUpdateAsync(List<CreateOrUpdateBookReaderUsingDto> input, int readerId)
        {
            try
            {
                foreach (var item in input)
                {
                    item.ReaderId = readerId;
                    var entity = ObjectMapper.Map<BookReaderUsing>(item);
                    await _bookReaderUsingRepository.InsertOrUpdateAsync(entity);
                }
                return 1;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return 0;
            }
        }

        public async Task<List<BookReaderUsingDto>> GetBookUsingByReaderId(int readerId)
        {
            var listBookUsing = _bookReaderUsingRepository.GetAllIncluding(x => x.Book, x => x.Reader).Where(x => x.ReaderId == readerId && x.IsUse == (int)LRMEnum.BookStatus.Using).ToList();
            return ObjectMapper.Map<List<BookReaderUsingDto>>(listBookUsing);
        }

        public async Task UpdateGiveBackBook(List<int> listBookIds, int readerId)
        {
            foreach (var bookId in listBookIds)
            {
                var bookReaderUsing = await _bookReaderUsingRepository.FirstOrDefaultAsync(x => x.BookId == bookId && x.ReaderId == readerId && x.IsUse == (int)LRMEnum.BookStatus.Using);
                if(bookReaderUsing != null)
                {
                    bookReaderUsing.GiveBackDate = DateTime.Now;
                    bookReaderUsing.IsUse = (int)LRMEnum.BookStatus.UnUsed;
                    await _bookReaderUsingRepository.UpdateAsync(bookReaderUsing);
                }
            }
        }
    }
}
