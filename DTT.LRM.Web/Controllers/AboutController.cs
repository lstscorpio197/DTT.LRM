using System.Web.Mvc;

namespace DTT.LRM.Web.Controllers
{
    public class AboutController : LRMControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}