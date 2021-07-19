using Abp.Domain.Repositories;
using DTT.LRM.BookReaderUsings;
using DTT.LRM.MyBooks.Dto;
using DTT.LRM.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;

namespace DTT.LRM.MyBooks
{
    public class MyBookAppService : LRMAppServiceBase, IMyBookAppService
    {
        private readonly IRepository<BookReaderUsing> _bookReaderUsingRepository;
        public MyBookAppService(IRepository<BookReaderUsing> bookReaderUsingRepository)
        {
            _bookReaderUsingRepository = bookReaderUsingRepository;
        }
        public async Task<PagedResultExtendDto<MyBookDto>> GetAll(PagedResultRequestExtendDto input)
        {
            if (!string.IsNullOrEmpty(input.Keyword))
            {
                var keyword = input.Keyword.ToLower();
                var listReaders = _bookReaderUsingRepository.GetAllIncluding(x => x.Book);
                listReaders = listReaders.Where(x => x.ReaderId.ToString().Equals(keyword));
                var items = listReaders.OrderBy("id DESC").PageBy(input).ToList();
                var listItems = ObjectMapper.Map<List<MyBookDto>>(items);
                listItems = listItems.Select(x =>
                {
                    x.BookCode = x.Book?.Code;
                    x.BookName = x.Book?.Name;
                    x.Book = null;
                    return x;
                }).ToList();
                return new PagedResultExtendDto<MyBookDto>(totalCount: listReaders.Count(), items: listItems, countStatus: null);
            }
            else
                return new PagedResultExtendDto<MyBookDto>(totalCount: 0, items: new List<MyBookDto>(), countStatus: null);
        }
    }
}
