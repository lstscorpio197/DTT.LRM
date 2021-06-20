using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using DTT.LRM.BookClassifies.Dto;
using DTT.LRM.Share;
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

        public async Task<PagedResultExtendDto<BookClassifyDto>> GetAll(PagedResultRequestExtendDto input)
        {
            var listBookClassifies = _bookClassifyRepository.GetAll();
            if (!string.IsNullOrEmpty(input.Keyword))
            {
                var keyword = input.Keyword.ToLower();
                listBookClassifies = listBookClassifies.Where(x => x.Code.ToString().Contains(keyword)
                                                                || x.Name.Contains(keyword));
            }
            var items = listBookClassifies.OrderBy("id ASC").PageBy(input).ToList();
            var listItems = ObjectMapper.Map<List<BookClassifyDto>>(items);
            return new PagedResultExtendDto<BookClassifyDto>(totalCount: listBookClassifies.Count(), items: listItems, countStatus: null);
        }

        public async Task<List<BookClassifyDto>> GetAllForSelect()
        {
            var listBookClassifies = await _bookClassifyRepository.GetAllListAsync(x => x.Status == true);
            return ObjectMapper.Map<List<BookClassifyDto>>(listBookClassifies);
        }

        public async Task<BookClassifyDto> GetById(int id)
        {
            var bookClassify = await _bookClassifyRepository.GetAsync(id);
            return ObjectMapper.Map<BookClassifyDto>(bookClassify);
        }
    }
}
