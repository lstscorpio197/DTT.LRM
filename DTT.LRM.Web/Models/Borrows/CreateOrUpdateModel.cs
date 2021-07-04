using DTT.LRM.BookBorrows.Dto;
using DTT.LRM.BookCategories.Dto;
using DTT.LRM.Borrows.Dto;
using DTT.LRM.Readers.Dto;
using DTT.LRM.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTT.LRM.Web.Models.Borrows
{
    public class CreateOrUpdateModel
    {
        public BorrowDto Borrow { get; set; }
        public List<BookBorrowDto> ListBook { get; set; }
        public List<BookCategoryDto> ListBookCategory { get; set; }
        public List<ReaderDto> ListReader { get; set; }
    }
}