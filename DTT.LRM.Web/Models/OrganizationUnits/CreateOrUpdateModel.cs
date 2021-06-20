using DTT.LRM.OrganizationUnits.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTT.LRM.Web.Models.OrganizationUnits
{
    public class CreateOrUpdateModel
    {
        public OrganizationUnitBasicDto OrganizationUnit { get; set; }
        public List<OrganizationUnitBasicDto> ListOrganizationUnits { get; set; }
    }
}