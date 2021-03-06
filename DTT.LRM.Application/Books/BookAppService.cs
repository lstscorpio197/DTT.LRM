using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using DTT.LRM.Books.Dto;
using DTT.LRM.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;

namespace DTT.LRM.Books
{
    public class BookAppService : LRMAppServiceBase, IBookAppService
    {
        private readonly IRepository<Book> _bookRepository;
        public BookAppService(IRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<int> CountBookBorrowByCategoryId(int bookCategotyId)
        {
            if (bookCategotyId > 0)
            {
                return _bookRepository.GetAll().Where(x => x.BookCategoryId == bookCategotyId && x.Status == (int)LRMEnum.BookStatus.UnUsed).Count();
            }
            return 0;
        }

        public async Task<int> CreateOrUpdateAsync(CreateOrUpdateBookDto input)
        {
            try
            {
                var check = await _bookRepository.FirstOrDefaultAsync(x => x.Code == input.Code && x.Id != input.Id);
                if (check != null)
                    return 0;

                if (input.Amount.HasValue && input.Amount.Value > 0 && !(input.Id > 0))
                {
                    var codeToInt = 0;
                    var lastBookCode = _bookRepository.GetAll().Where(x => x.BookCategoryId == input.BookCategoryId).OrderByDescending(x => x.Id).Select(x => x.Code).FirstOrDefault();

                    if (!string.IsNullOrEmpty(lastBookCode))
                    {
                        var codeIndex = lastBookCode.Substring(input.Code.Length);
                        codeToInt = int.Parse(codeIndex);
                    }
                    for (int i = 1; i <= input.Amount; i++)
                    {
                        var code = input.Code + (codeToInt + i).ToString("D4");
                        var obj = ObjectMapper.Map<Book>(input);
                        obj.Code = code;
                        await _bookRepository.InsertOrUpdateAsync(obj);
                    }
                }
                else
                {
                    var obj = ObjectMapper.Map<Book>(input);
                    await _bookRepository.InsertOrUpdateAsync(obj);
                }
                return 1;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public async Task DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResultExtendDto<BookDto>> GetAll(PagedResultRequestExtendDto input)
        {
            var bookClassifyId = input.BookClassifyId.HasValue ? input.BookClassifyId.Value : -1;
            var bookFieldId = input.BookFieldId.HasValue ? input.BookFieldId.Value : -1;
            var keyword = input.Keyword.ToLower();
            var status = input.Status.HasValue ? input.Status : null;
            var listBooks = _bookRepository.GetAllIncluding(x => x.Publisher, x => x.BookCategory.BookField.BookClassify).Where(x => (bookClassifyId <= 0 ? true : x.BookCategory.BookField.BookClassifyId == bookClassifyId) &&
                                                                                                                                    (bookFieldId <= 0 ? true : x.BookCategory.BookFieldId == bookFieldId) &&
                                                                                                                                    (status == null ? true : x.Status == status) &&
                                                                                                                                     (x.Code.Contains(keyword) ||
                                                                                                                                     x.Name.ToLower().Contains(keyword) ||
                                                                                                                                     x.Author.ToLower().Contains(keyword) ||
                                                                                                                                     x.ReleaseYear.ToString() == keyword ||
                                                                                                                                     x.Language.ToLower().Contains(keyword) ||
                                                                                                                                     x.Publisher.Name.ToLower().Contains(keyword)));
            var items = listBooks.OrderBy("id DESC").PageBy(input).ToList();
            var listItems = ObjectMapper.Map<List<BookDto>>(items);
            return new PagedResultExtendDto<BookDto>(totalCount: listBooks.Count(), items: listItems, countStatus: null);
        }

        public async Task<BookDto> GetById(int id)
        {
            try
            {
                var book = _bookRepository.GetAllIncluding(x => x.Publisher, x => x.BookCategory.BookField.BookClassify).FirstOrDefault(x => x.Id == id);
                return ObjectMapper.Map<BookDto>(book);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return new BookDto();
            }
        }

        public async Task<List<BookDto>> GetListBookForBorrow(AdvanceSearchBookDto input)
        {
            var author = string.IsNullOrEmpty(input.Author) ? string.Empty : input.Author.ToLower();
            var query = _bookRepository.GetAllIncluding(x => x.Publisher);
            var listBook = query.Where(x => x.Author.ToLower().Contains(author) &&
                                        (input.ReleaseYear > 0 ? x.ReleaseYear == input.ReleaseYear : true) &&
                                        (input.PublisherId > 0 ? x.PublisherId == input.PublisherId : true) &&
                                        x.Status == (int)LRMEnum.BookStatus.UnUsed &&
                                        x.BookCategoryId == input.BookCategoryId)
                                .ToList();
            return ObjectMapper.Map<List<BookDto>>(listBook);
        }

        public async Task<int> UpdateBookStatus(List<int> listBookIds, int status)
        {
            try
            {
                foreach (int bookId in listBookIds)
                {
                    var book = await _bookRepository.GetAsync(bookId);
                    if (book != null)
                    {
                        book.Status = status;
                        await _bookRepository.UpdateAsync(book);
                    }
                }
                return 1;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return 0;
            }
        }

        public async Task<int> UpdateBookStatusLiquidation(int bookCategotyId, string author, int releaseYear, int publisherId)
        {
            try
            {
                var listBook = _bookRepository.GetAll().Where(x => x.Status == (int)LRMEnum.BookStatus.UnUsed &&
                                                                x.BookCategoryId == bookCategotyId &&
                                                                (string.IsNullOrEmpty(author) ? true : x.Author.ToLower().Contains(author.ToLower())) &&
                                                                (releaseYear > 0 ? x.ReleaseYear == releaseYear : true) &&
                                                                (publisherId > 0 ? x.PublisherId == publisherId : true)).ToList();
                foreach (Book book in listBook)
                {
                    book.Status = (int)LRMEnum.BookStatus.Liquidated;
                    await _bookRepository.UpdateAsync(book);
                }
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
