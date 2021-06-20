using DTT.LRM.BookClassifies.Dto;
using DTT.LRM.PositionQuotas.Dto;
using DTT.LRM.Positions.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTT.LRM.Web.Models.Positions
{
    public class CreateOrUpdateModel
    {
        public PositionDto Position { get; set; }
        public List<BookClassifyDto> ListBookClassify { get; set; }
        public List<PositionQuotaDto> ListPositionQuotas { get; set; }
    }
}