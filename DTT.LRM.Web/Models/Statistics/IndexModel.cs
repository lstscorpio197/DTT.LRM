using DTT.LRM.BookClassifies.Dto;
using DTT.LRM.BookFields.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTT.LRM.Web.Models.Statistics
{
    public class IndexModel
    {
        public List<BookClassifyDto> ListBookClassifies { get; set; }
        public List<BookFieldDto> ListBookFields { get; set; }
    }
}