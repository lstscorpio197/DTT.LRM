using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using DTT.LRM.BookClassifies.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace DTT.LRM.BookClassifies
{
    public class BookClassifyAppService : LRMAppServiceBase, IBookClassifyAppService
    {
        private readonly IRepository<BookClassify> _bookClassifyRepository;
        public BookClassifyAppService(IRepository<BookClassify> bookClassifyRepository)
        {
            _bookClassifyRepository = bookClassifyRepository;
        }

        public async Task<int> CreateOrUpdateAsync(CreateOrUpdateBookClassifyDto input)
        {
            var check = await _bookClassifyRepository.FirstOrDefaultAsync(x => x.Code == input.Code && x.Id != input.Id);
            if (check != null)
                return 0;
            var obj = ObjectMapper.Map<BookClassify>(input);
            return await _bookClassifyRepository.InsertOrUpdateAndGetIdAsync(obj);
        }

        public async Task DeleteById(int id)
        {
            var bookClassify = await _bookClassifyRepository.GetAsync(id);
            await _bookClassifyRepository.DeleteAsync(bookClassify);
        }

        public async Task<PagedResultDto<BookClassifyDto>> GetAll(PagedResultRequestDto input)
        {
            var listBookClassifies = _bookClassifyRepository.GetAll();
            listBookClassifies = listBookClassifies.OrderBy("id DESC").PageBy(input);
            return new PagedResultDto<BookClassifyDto>
            {
                TotalCount = listBookClassifies.Count(),
                Items = listBookClassifies.MapTo<List<BookClassifyDto>>()
            };
        }

        public async Task<BookClassifyDto> GetById(int id)
        {
            var bookClassify = await _bookClassifyRepository.GetAsync(id);
            return ObjectMapper.Map<BookClassifyDto>(bookClassify);
        }
    }
}
