using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using DTT.LRM.BookFields.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;

namespace DTT.LRM.BookFields
{
    public class BookFieldAppService : LRMAppServiceBase, IBookFieldAppService
    {
        private readonly IRepository<BookField> _bookFieldRepository;
        public BookFieldAppService(IRepository<BookField> bookFieldRepository)
        {
            _bookFieldRepository = bookFieldRepository;
        }
        public async Task<int> CreateOrUpdateAsync(CreateOrUpdateBookFieldDto input)
        {
            var check = await _bookFieldRepository.FirstOrDefaultAsync(x => x.Code == input.Code && x.Id != input.Id);
            if (check != null)
                return 0;
            var obj = ObjectMapper.Map<BookField>(input);
            return await _bookFieldRepository.InsertOrUpdateAndGetIdAsync(obj);
        }

        public async Task DeleteById(int id)
        {
            var bookField = await _bookFieldRepository.GetAsync(id);
            await _bookFieldRepository.DeleteAsync(bookField);
        }

        public async Task<PagedResultDto<BookFieldDto>> GetAll(PagedResultRequestDto input)
        {
            var listBookFields = _bookFieldRepository.GetAll();
            listBookFields = listBookFields.OrderBy("id DESC").PageBy(input);
            return new PagedResultDto<BookFieldDto>
            {
                TotalCount = listBookFields.Count(),
                Items = listBookFields.MapTo<List<BookFieldDto>>()
            };
        }

        public async Task<BookFieldDto> GetById(int id)
        {
            var bookField = await _bookFieldRepository.GetAsync(id);
            return ObjectMapper.Map<BookFieldDto>(bookField);
        }
    }
}
