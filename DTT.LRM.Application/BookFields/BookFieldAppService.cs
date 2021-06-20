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
using DTT.LRM.Share;

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
            try
            {
                var check = await _bookFieldRepository.FirstOrDefaultAsync(x => x.Code == input.Code && x.Id != input.Id);
                if (check != null)
                    return 0;
                var obj = ObjectMapper.Map<BookField>(input);
                return await _bookFieldRepository.InsertOrUpdateAndGetIdAsync(obj);
            }
            catch(Exception ex)
            {
                return -1;
            }
        }

        public async Task DeleteById(int id)
        {
            var bookField = await _bookFieldRepository.GetAsync(id);
            await _bookFieldRepository.DeleteAsync(bookField);
        }

        public async Task<PagedResultExtendDto<BookFieldDto>> GetAll(PagedResultRequestExtendDto input)
        {
            var listBookFields = _bookFieldRepository.GetAllIncluding(x=>x.BookClassify);
            var items = listBookFields.OrderBy("id ASC").PageBy(input).ToList();
            var listItems = ObjectMapper.Map<List<BookFieldDto>>(items);
            return new PagedResultExtendDto<BookFieldDto>(totalCount: listBookFields.Count(), items: listItems, countStatus: null);
        }

        public async Task<List<BookFieldDto>> GetAllForSelect()
        {
            var listBookFields = await _bookFieldRepository.GetAllListAsync(x=>x.Status == true);
            return ObjectMapper.Map<List<BookFieldDto>>(listBookFields);
        }

        public async Task<BookFieldDto> GetById(int id)
        {
            var bookField = await _bookFieldRepository.GetAsync(id);
            return ObjectMapper.Map<BookFieldDto>(bookField);
        }

        public async Task<List<BookFieldDto>> GetListForSelectByBookClassifyId(int bookClassifyId)
        {
            var listBookFields = await _bookFieldRepository.GetAllListAsync(x => x.Status == true && x.BookClassifyId == bookClassifyId);
            return ObjectMapper.Map<List<BookFieldDto>>(listBookFields);
        }
    }
}
