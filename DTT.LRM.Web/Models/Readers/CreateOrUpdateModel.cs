using DTT.LRM.OrganizationUnits.Dto;
using DTT.LRM.Positions.Dto;
using DTT.LRM.Readers.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTT.LRM.Web.Models.Readers
{
    public class CreateOrUpdateModel
    {
        public ReaderDto Reader { get; set; }
        public List<OrganizationUnitBasicDto> ListOrganizationUnits { get; set; }
        public List<PositionDto> ListPositions { get; set; }
    }
}