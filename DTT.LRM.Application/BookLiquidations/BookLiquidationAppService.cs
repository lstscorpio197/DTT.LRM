using Abp.Domain.Repositories;
using DTT.LRM.BookLiquidations.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.BookLiquidations
{
    public class BookLiquidationAppService : LRMAppServiceBase, IBookLiquidationAppService
    {
        private readonly IRepository<BookLiquidation> _bookLiquidationRepository;
        public BookLiquidationAppService(IRepository<BookLiquidation> bookLiquidationRepository)
        {
            _bookLiquidationRepository = bookLiquidationRepository;
        }
        public async Task<int> CreateOrUpdateAsync(List<CreateBookLiquidationDto> input, int liquidationId)
        {
            try
            {
                var listIds = input.Select(x => x.Id);
                await _bookLiquidationRepository.DeleteAsync(x => x.LiquidationId == liquidationId && listIds.Contains(x.Id) == false);
                foreach (var item in input)
                {
                    item.LiquidationId = liquidationId;
                    var obj = ObjectMapper.Map<BookLiquidation>(item);
                    await _bookLiquidationRepository.InsertOrUpdateAndGetIdAsync(obj);
                }
                return 1;
            }
            catch (Exception)
            {
                return -1;
                throw;
            }
        }

        public async Task<List<BookLiquidationDto>> GetAllByLiquidationId(int liquidationId)
        {
            var listEntities = _bookLiquidationRepository.GetAllIncluding(x => x.BookCategorie).Where(x => x.LiquidationId == liquidationId).ToList();
            return ObjectMapper.Map<List<BookLiquidationDto>>(listEntities);
        }
    }
}
