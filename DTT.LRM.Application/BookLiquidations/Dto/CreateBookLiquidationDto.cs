using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.BookLiquidations.Dto
{
    [AutoMapTo(typeof(BookLiquidation))]
    public class CreateBookLiquidationDto : AuditedEntityDto
    {
        public int LiquidationId { get; set; }
        public int BookCategorieId { get; set; }
        public decimal LiquidationPrice { get; set; }
        public string Author { get; set; }
        public int? ReleaseYear { get; set; }
        public int? PublisherId { get; set; }
    }
}
