using Abp.Application.Services;
using DTT.LRM.BookGiveBacks.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.BookGiveBacks
{
    public interface IBookGiveBackAppService : IApplicationService
    {
        Task<int> CreateOrUpdateAsync(List<CreateOrUpdateBookGiveBackDto> input, int giveBackId);
        Task<List<BookGiveBackDto>> GetAllByGiveBackId(int giveBackId);
        Task<int> CreateAndViolateAsync(List<BookGiveBackAndViolateDto> input, int giveBackId);
    }
}
