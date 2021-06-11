using DTT.LRM.BookCategories.Dto;
using DTT.LRM.BookFields.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTT.LRM.Web.Models.BookCategories
{
    public class CreateOrUpdateModel
    {
        public BookCategoryDto BookCategory { get; set; }
        public List<BookFieldDto> ListBookField { get; set; }
    }
}