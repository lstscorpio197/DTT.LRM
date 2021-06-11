using DTT.LRM.BookClassifies.Dto;
using DTT.LRM.BookFields.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTT.LRM.Web.Models.BookFields
{
    public class CreateOrUpdateModel
    {
        public BookFieldDto BookField { get; set; }
        public List<BookClassifyDto> ListBookClassify { get; set; }
    }
}