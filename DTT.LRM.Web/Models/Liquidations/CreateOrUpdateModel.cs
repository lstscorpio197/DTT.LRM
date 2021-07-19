using DTT.LRM.BookCategories.Dto;
using DTT.LRM.BookLiquidations.Dto;
using DTT.LRM.Liquidations.Dto;
using DTT.LRM.Publishers.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTT.LRM.Web.Models.Liquidations
{
    public class CreateOrUpdateModel
    {
        public LiquidationDto Liquidation { get; set; }
        public List<BookLiquidationDto> ListBookLiquidations { get; set; }
        public List<PublisherDto> ListPublishers { get; set; }
        public List<BookCategoryDto> ListBookCategories { get; set; }
    }
}