using Abp.Domain.Repositories;
using DTT.LRM.Borrows.Dto;
using DTT.LRM.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;

namespace DTT.LRM.Borrows
{
    public class BorrowAppService : LRMAppServiceBase, IBorrowAppService
    {
        private readonly IRepository<Borrow> _borrowRepository;
        public BorrowAppService(IRepository<Borrow> borrowRepository)
        {
            _borrowRepository = borrowRepository;
        }

        public async Task<string> AutoGetCode()
        {
            string codeTemp = "MS" + DateTime.Now.ToString("yy/MM/dd").Replace("/", string.Empty);
            var codeIndex = _borrowRepository.GetAll().Where(x => x.Code.Substring(0, codeTemp.Length) == codeTemp).OrderByDescending(x=>x.Code.Substring(codeTemp.Length)).Select(x=>x.Code.Substring(codeTemp.Length)).FirstOrDefault();
            if (string.IsNullOrEmpty(codeIndex))
                return codeTemp + "001";
            else
                return codeTemp + (int.Parse(codeIndex) + 1).ToString("D3");
        }

        public async Task<int> CreateOrUpdateAsync(CreateOrUpdateBorrowDto input)
        {
            var check = await _borrowRepository.FirstOrDefaultAsync(x => x.Code == input.Code && x.Id != input.Id);
            if (check != null)
                return 0;
            var obj = ObjectMapper.Map<Borrow>(input);
            return await _borrowRepository.InsertOrUpdateAndGetIdAsync(obj);
        }

        public async Task DeleteById(int id)
        {
            var borrow = await _borrowRepository.GetAsync(id);
            await _borrowRepository.DeleteAsync(borrow);
        }

        public async Task<PagedResultExtendDto<BorrowDto>> GetAll(PagedResultRequestExtendDto input)
        {
            var keyword = input.Keyword.ToLower();
            var status = input.Status.HasValue ? input.Status.Value : -1;
            var listBorrows = _borrowRepository.GetAllIncluding(x => x.Reader).Where(x=>x.Code.Contains(keyword) ||
                                                                                        x.Creator.ToLower().Contains(keyword)||
                                                                                        x.Reader.Name.ToLower().Contains(keyword)||
                                                                                        (status < 0 ? true : x.Status == status));
            var items = listBorrows.OrderBy("id DESC").PageBy(input).ToList();
            var listItems = ObjectMapper.Map<List<BorrowDto>>(items);
            return new PagedResultExtendDto<BorrowDto>(totalCount: listBorrows.Count(), items: listItems, countStatus: null);
        }

        public async Task<BorrowDto> GetById(int id)
        {
            var borrow = await _borrowRepository.GetAsync(id);
            return ObjectMapper.Map<BorrowDto>(borrow);
        }
    }
}
