using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using DTT.LRM.Authorization;

namespace DTT.LRM.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : LRMControllerBase
    {
        public ActionResult Index()
        {
            if (IsGranted(PermissionNames.Pages_SearchInfos))
            {
                return Redirect(Url.Action("Index","SearchInfos"));
            }
            if (IsGranted(PermissionNames.Pages_MyBooks))
            {
                return Redirect(Url.Action("Index", "MyBooks"));
            }
            return View();
        }
	}
}