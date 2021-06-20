using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using DTT.LRM.BookCategories.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using DTT.LRM.Share;

namespace DTT.LRM.BookCategories
{
    public class BookCategoryAppService : LRMAppServiceBase, IBookCategoryAppService
    {
        private readonly IRepository<BookCategory> _bookCategoryRepository;
        public BookCategoryAppService(IRepository<BookCategory> bookCategoryRepository)
        {
            _bookCategoryRepository = bookCategoryRepository;
        }

        public async Task<IdAndCodeResultDto> CreateNewBookCategoryWithName(CreateOrUpdateBookCategoryDto input)
        {
            var returnObj = new IdAndCodeResultDto();
            var lastCategory = _bookCategoryRepository.GetAll().OrderByDescending(x => x.Code).Select(x => x.Code).FirstOrDefault();
            input.Code = lastCategory + 1;
            var obj = ObjectMapper.Map<BookCategory>(input);
            returnObj.Code = obj.Code;
            returnObj.Id = await _bookCategoryRepository.InsertAndGetIdAsync(obj);
            return returnObj;
        }

        public async Task<int> CreateOrUpdateAsync(CreateOrUpdateBookCategoryDto input)
        {
            var check = await _bookCategoryRepository.FirstOrDefaultAsync(x => x.Code == input.Code && x.Id != input.Id);
            if (check != null)
                return 0;
            var obj = ObjectMapper.Map<BookCategory>(input);
            return await _bookCategoryRepository.InsertOrUpdateAndGetIdAsync(obj);
        }

        public async Task DeleteById(int id)
        {
            var bookCategory = await _bookCategoryRepository.GetAsync(id);
            await _bookCategoryRepository.DeleteAsync(bookCategory);
        }

        public async Task<PagedResultExtendDto<BookCategoryDto>> GetAll(PagedResultRequestExtendDto input)
        {
            var listBookCategories = _bookCategoryRepository.GetAllIncluding(x=>x.BookField);
            var items = listBookCategories.OrderBy("id DESC").PageBy(input).ToList();
            var listItems = ObjectMapper.Map<List<BookCategoryDto>>(items);
            return new PagedResultExtendDto<BookCategoryDto>(totalCount: listBookCategories.Count(), items: listItems, countStatus: null);
        }

        public async Task<BookCategoryDto> GetById(int id)
        {
            var bookCategory = await _bookCategoryRepository.GetAsync(id);
            return ObjectMapper.Map<BookCategoryDto>(bookCategory);
        }

        public async Task<List<BookCategoryDto>> GetListForSelectByBookFieldId(int bookFieldId)
        {
            var listBookCategories = await _bookCategoryRepository.GetAllListAsync(x => x.BookFieldId == bookFieldId && x.Status ==true);
            return ObjectMapper.Map<List<BookCategoryDto>>(listBookCategories);
        }
    }
}
