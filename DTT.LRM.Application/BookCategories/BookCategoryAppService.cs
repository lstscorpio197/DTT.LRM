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

namespace DTT.LRM.BookCategories
{
    public class BookCategoryAppService : LRMAppServiceBase, IBookCategoryAppService
    {
        private readonly IRepository<BookCategory> _bookCategoryRepository;
        public BookCategoryAppService(IRepository<BookCategory> bookCategoryRepository)
        {
            _bookCategoryRepository = bookCategoryRepository;
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

        public async Task<PagedResultDto<BookCategoryDto>> GetAll(PagedResultRequestDto input)
        {
            var listBookCategories = _bookCategoryRepository.GetAll();
            listBookCategories = listBookCategories.OrderBy("id DESC").PageBy(input);
            return new PagedResultDto<BookCategoryDto>
            {
                TotalCount = listBookCategories.Count(),
                Items = listBookCategories.MapTo<List<BookCategoryDto>>()
            };
        }

        public async Task<BookCategoryDto> GetById(int id)
        {
            var bookCategory = await _bookCategoryRepository.GetAsync(id);
            return ObjectMapper.Map<BookCategoryDto>(bookCategory);
        }
    }
}
