using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;

namespace DTT.LRM.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : LRMControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}