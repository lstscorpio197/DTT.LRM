using DTT.LRM.BookGiveBacks.Dto;
using DTT.LRM.GiveBacks.Dto;
using DTT.LRM.Readers.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTT.LRM.Web.Models.GiveBacks
{
    public class CreateOrUpdateModel
    {
        public GiveBackDto GiveBack { get; set; }
        public List<BookGiveBackDto> ListBookGiveBacks { get; set; }
        public List<ReaderDto> ListReader { get; set; }
    }
}