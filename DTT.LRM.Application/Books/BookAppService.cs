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
            var listBooks = _bookRepository.GetAllIncluding(x=>x.Publisher,x=>x.BookCategory.BookField.BookClassify);
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
    }
}
