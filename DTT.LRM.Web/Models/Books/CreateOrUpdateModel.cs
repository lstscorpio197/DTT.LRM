using DTT.LRM.BookClassifies.Dto;
using DTT.LRM.Books.Dto;
using DTT.LRM.Publishers.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTT.LRM.Web.Models.Books
{
    public class CreateOrUpdateModel
    {
        public BookDto Book { get; set; }
        public List<BookClassifyDto> ListBookClassifies { get; set; }
        public List<PublisherDto> ListPublishers { get; set; }
    }
}