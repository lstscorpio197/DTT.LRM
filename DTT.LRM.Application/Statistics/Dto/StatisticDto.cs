using Abp.Application.Services.Dto;
using DTT.LRM.BookCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.Statistics.Dto
{
    public class StatisticDto
    {
        public BookCategory BookCategory { get; set; }
        public int Total { get; set; }
        public int TotalBorrow { get; set; }
        public int TotalLiquidation { get; set; }
        public int TotalLost { get; set; }
        public int TotalGiveBack { get; set; }

        public string BookCategoryName { get; set; }
    }
}
