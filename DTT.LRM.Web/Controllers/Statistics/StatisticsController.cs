using Abp.Authorization;
using DTT.LRM.BookClassifies;
using DTT.LRM.BookFields;
using DTT.LRM.Share;
using DTT.LRM.Statistics;
using DTT.LRM.Web.Models.Statistics;
using GemBox.Spreadsheet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace DTT.LRM.Web.Controllers.Statistics
{
    [AbpAuthorize]
    public class StatisticsController : LRMControllerBase
    {
        private readonly IStatisticAppService _statisticAppService;
        private readonly IBookClassifyAppService _bookClassifyAppService;
        private readonly IBookFieldAppService _bookFieldAppService;
        public StatisticsController(IStatisticAppService statisticAppService, IBookClassifyAppService bookClassifyAppService, IBookFieldAppService bookFieldAppService)
        {
            _statisticAppService = statisticAppService;
            _bookClassifyAppService = bookClassifyAppService;
            _bookFieldAppService = bookFieldAppService;
        }
        // GET: Statistics
        public async Task<ActionResult> Index()
        {
            var model = new IndexModel();
            model.ListBookClassifies = await _bookClassifyAppService.GetAllForSelect();
            model.ListBookFields = await _bookFieldAppService.GetAllForSelect();
            return View(model);
        }

        public async Task<ActionResult> GetDataTable(string keyword = "", DateTime? startDate = null, DateTime? endDate = null, int? bookClassifyId = null, int? bookFieldId = null)
        {
            int start = Convert.ToInt32(Request["start"]);
            var filter = new PagedResultRequestExtendDto
            {
                SkipCount = start,
                Keyword = keyword,
                DateStart = startDate,
                DateEnd =endDate,
                BookClassifyId = bookClassifyId,
                BookFieldId =bookFieldId
            };
            var data = await _statisticAppService.GetAll(filter);
            return Json(new
            {
                data = data,
                startIndex = start + 1
            }, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> ExportExcel()
        {
            try
            {
                var dataResult = System.Web.HttpContext.Current.Request.Form["data"];
                if(dataResult != null)
                {
                    var filter = new JavaScriptSerializer().Deserialize<PagedResultRequestExtendDto>(dataResult);
                    var start = filter.DateStart.HasValue ? filter.DateStart.Value.ToString("dd/MM/yyyy") : "...";
                    var end = filter.DateEnd.HasValue ? filter.DateEnd.Value.ToString("dd/MM/yyyy") : DateTime.Now.ToString("dd/MM/yyyy");
                    SpreadsheetInfo.SetLicense("ETZX-IT28-33Q6-1HA2");
                    var workbook = ExcelFile.Load(Server.MapPath("/ExcelFile/Statistic.xlsx"));
                    var wordsheet = workbook.Worksheets[0];
                    wordsheet.Cells["A4"].Value = "(Từ ngày " + start + " đến ngày " + end + ")";

                    var data = await _statisticAppService.GetAllForExport(filter);
                    int rowStart = 7;
                    int index = 1;
                    foreach (var item in data)
                    {
                        wordsheet.Cells["A" + rowStart].Value = index;
                        wordsheet.Cells["B" + rowStart].Value = item.BookCategoryName;
                        wordsheet.Cells["C" + rowStart].Value = item.Total;
                        wordsheet.Cells["D" + rowStart].Value = item.TotalLiquidation;
                        wordsheet.Cells["E" + rowStart].Value = item.TotalLost;
                        wordsheet.Cells["F" + rowStart].Value = item.TotalBorrow;
                        wordsheet.Cells["G" + rowStart].Value = item.TotalGiveBack;
                        rowStart++;
                        index++;
                    }

                    string handle = Guid.NewGuid().ToString();
                    var stream = new MemoryStream();
                    workbook.Save(stream, SaveOptions.XlsxDefault);
                    stream.Position = 0;
                    TempData[handle] = stream.ToArray();
                    return Json(new { FileGuild = handle, FileName = "Thống kê mượn trả sách.xlsx" }, JsonRequestBehavior.AllowGet);
                }
                return Json(0);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual ActionResult Download(string fileGuild, string fileName)
        {
            if(TempData[fileGuild] != null)
            {
                byte[] data = TempData[fileGuild] as byte[];
                return File(data, "application/octet-stream", fileName);
            }
            return new EmptyResult();
        }
    }
}