using Abp.Domain.Repositories;
using DTT.LRM.BookReaderUsings;
using DTT.LRM.SearchInfos.Dto;
using DTT.LRM.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using AutoMapper;

namespace DTT.LRM.SearchInfos
{
    public class SearchInfoAppService : LRMAppServiceBase, ISearchInfoAppService
    {
        private readonly IRepository<BookReaderUsing> _bookReaderUsingRepository;
        public SearchInfoAppService(IRepository<BookReaderUsing> bookReaderUsingRepository)
        {
            _bookReaderUsingRepository = bookReaderUsingRepository;
        }
        public async Task<PagedResultExtendDto<SearchInfoDto>> GetAll(PagedResultRequestExtendDto input)
        {
            if (!string.IsNullOrEmpty(input.Keyword))
            {
                var keyword = input.Keyword.ToLower();
                var listReaders = _bookReaderUsingRepository.GetAllIncluding(x => x.Book, x => x.Reader);
                listReaders = listReaders.Where(x => x.Book.Code.Equals(keyword) ||
                            x.Book.Name.ToLower().Contains(keyword) ||
                            x.Reader.Code.Equals(keyword) ||
                            x.Reader.Name.ToLower().Contains(keyword));
                var items = listReaders.OrderBy("id DESC").PageBy(input).ToList();
                var listItems = ObjectMapper.Map<List<SearchInfoDto>>(items);
                listItems = listItems.Select(x =>
                {
                    x.BookCode = x.Book?.Code;
                    x.BookName = x.Book?.Name;
                    x.ReaderCode = x.Reader?.Code;
                    x.ReaderName = x.Reader?.Name;
                    x.Book = null;
                    x.Reader = null;
                    return x;
                }).ToList();
                return new PagedResultExtendDto<SearchInfoDto>(totalCount: listReaders.Count(), items: listItems, countStatus: null);
            }
            else
                return new PagedResultExtendDto<SearchInfoDto>(totalCount: 0, items: new List<SearchInfoDto>(), countStatus: null);
        }
    }
}
