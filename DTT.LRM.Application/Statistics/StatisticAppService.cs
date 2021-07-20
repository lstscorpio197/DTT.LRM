using Abp.Domain.Repositories;
using DTT.LRM.BookReaderUsings;
using DTT.LRM.Books;
using DTT.LRM.Share;
using DTT.LRM.Statistics.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;

namespace DTT.LRM.Statistics
{
    public class StatisticAppService : LRMAppServiceBase, IStatisticAppService
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<BookReaderUsing> _bookReaderUsingRepository;
        public StatisticAppService(IRepository<Book> bookRepository, IRepository<BookReaderUsing> bookReaderUsingRepository)
        {
            _bookRepository = bookRepository;
            _bookReaderUsingRepository = bookReaderUsingRepository;
        }
        public async Task<PagedResultExtendDto<StatisticDto>> GetAll(PagedResultRequestExtendDto input)
        {
            var bookClassifyId = input.BookClassifyId.HasValue ? input.BookClassifyId.Value : -1;
            var bookFieldId = input.BookFieldId.HasValue ? input.BookFieldId.Value : -1;
            DateTime? startDate = input.DateStart;
            DateTime endDate = input.DateEnd.HasValue ? input.DateEnd.Value : DateTime.Today;
            if(!startDate.HasValue)
                return new PagedResultExtendDto<StatisticDto>(totalCount: 0, items: new List<StatisticDto>(), countStatus: null);
            var listBook = _bookRepository.GetAllIncluding(x=>x.BookCategory.BookField).Where(x=>(bookClassifyId <=0 ? true : x.BookCategory.BookField.BookClassifyId == bookClassifyId)&&
                                                                                                 (bookFieldId <=0 ? true : x.BookCategory.BookFieldId == bookFieldId));
            var listBookReader = _bookReaderUsingRepository.GetAllIncluding(x=>x.Book).GroupBy(x=>x.Book.BookCategoryId);
            var listRecord = listBook.GroupBy(x=>x.BookCategoryId).Select(x=>new StatisticDto
                                                                    {
                                                                        BookCategory = x.First().BookCategory,
                                                                        TotalCount = x.Count(),
                                                                        TotalLiquidation = x.Where(i=>i.Status == (int)LRMEnum.BookStatus.Liquidated).Count(),
                                                                        TotalLost = x.Where(i=>i.Status == (int)LRMEnum.BookStatus.Lost).Count(),
                                                                        TotalBorrow = 0,
                                                                        TotalGiveBack = 0
                                                                    });
            foreach(var record in listRecord)
            {
                var bookCategoryId = record.BookCategory.Id;
                var listByCategoryId = listBookReader.FirstOrDefault(x => x.Key == bookCategoryId);
                record.TotalBorrow = listByCategoryId.Where(x => DateTime.Compare(x.BorrowDate, startDate.Value) >= 0 &&
                                                               DateTime.Compare(x.BorrowDate, endDate) <= 0).Count();
                record.TotalGiveBack = listByCategoryId.Where(x => x.GiveBackDate.HasValue &&
                                                                DateTime.Compare(x.GiveBackDate.Value, startDate.Value) >= 0 &&
                                                                DateTime.Compare(x.GiveBackDate.Value, endDate) <= 0).Count();
            }
            var listItems = listRecord.OrderBy("bookCategoryName ASC").PageBy(input).ToList();
            return new PagedResultExtendDto<StatisticDto>(totalCount: listRecord.Count(), items: listItems, countStatus: null);
        }
    }
}
