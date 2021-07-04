using Abp.Domain.Repositories;
using DTT.LRM.BookGiveBacks.Dto;
using DTT.LRM.Books;
using DTT.LRM.Violates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.BookGiveBacks
{
    public class BookGiveBackAppService : LRMAppServiceBase, IBookGiveBackAppService
    {
        private readonly IRepository<BookGiveBack> _bookGiveBackRepository;
        private readonly IRepository<Book> _bookRepository;
        private readonly IViolateAppService _violateAppService;
        public BookGiveBackAppService(IRepository<BookGiveBack> bookGiveBackRepository, IRepository<Book> bookRepository, IViolateAppService violateAppService)
        {
            _bookGiveBackRepository = bookGiveBackRepository;
            _bookRepository = bookRepository;
            _violateAppService = violateAppService;
        }

        public async Task<int> CreateAndViolateAsync(List<BookGiveBackAndViolateDto> input, int giveBackId)
        {
            foreach(var item in input)
            {
                var violateId = await _violateAppService.CreateOrUpdate(item.Violate);
                if(violateId > 0)
                {
                    var entity = new BookGiveBack();
                    entity.GiveBackId = giveBackId;
                    entity.BookId = item.BookId;
                    entity.GiveBackDate = DateTime.Now;
                    entity.Note = item.Note;
                    entity.Status = item.Status;
                    entity.ViolateId = violateId;
                    await _bookGiveBackRepository.InsertAsync(entity);
                }
                var book = await _bookRepository.GetAsync(item.BookId);
                book.Status = item.Violate.ViolateError;
                await _bookRepository.UpdateAsync(book);
            }
            return 1;
        }

        public async Task<int> CreateOrUpdateAsync(List<CreateOrUpdateBookGiveBackDto> input, int giveBackId)
        {
            try
            {
                var listIds = input.Select(x => x.Id);
                await _bookGiveBackRepository.DeleteAsync(x => x.GiveBackId == giveBackId && listIds.Contains(x.Id) == false);
                foreach (var item in input)
                {
                    item.GiveBackId = giveBackId;
                    item.GiveBackDate = DateTime.Now;
                    var obj = ObjectMapper.Map<BookGiveBack>(item);
                    await _bookGiveBackRepository.InsertOrUpdateAndGetIdAsync(obj);
                    var book = await _bookRepository.GetAsync(item.BookId);
                    book.Status = (int)LRMEnum.BookStatus.UnUsed;
                    await _bookRepository.UpdateAsync(book);
                }
                return 1;
            }
            catch (Exception)
            {
                return -1;
                throw;
            }
        }

        public async Task<List<BookGiveBackDto>> GetAllByGiveBackId(int giveBackId)
        {
            var listEntities = _bookGiveBackRepository.GetAllIncluding(x => x.Book).Where(x => x.GiveBackId == giveBackId).ToList();
            return ObjectMapper.Map<List<BookGiveBackDto>>(listEntities);
        }
    }
}
