using DTT.LRM.BookClassifies.Dto;
using DTT.LRM.BookFields.Dto;
using DTT.LRM.Publishers.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTT.LRM.Web.Models.Publishers
{
    public class CreateOrUpdateModel
    {
        public PublisherDto Publisher { get; set; }
    }
}