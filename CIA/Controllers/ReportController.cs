using CIA_BLL.ErrorHandling;
using CIA_BLL.IRepository;
using CIA_BLL.Repository;
using CIA_MAL.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CIA.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report

        IReport rpt_repos = new ReportRepository();
        ResultStatus rs = new ResultStatus();
        public ActionResult Report()
        {
            return View();
        }

        public JsonResult oncliked_view(int report_typ, string seldbranch, DateTime frmDate, DateTime endDate, string clientName, string report_options)
        {
            ResultStatus rs = new ResultStatus();
            try
            {
                string excel_filename = "";
                //excel_filename = "ClientReport_" + DateTime.Now.ToString("yyyymmddHHmmss") + ".xlsx";
                rs = rpt_repos._Rptgridshow(report_typ, seldbranch, frmDate, endDate, clientName, report_options);
                
            }
            catch (Exception ex)
            {
                rs.Status = "Failure";
                if (ex.Message != null || ex.Message != "")
                {
                    rs.MSG = ex.Message;
                }
                else
                {
                    rs.MSG = ex.StackTrace;
                }
                ExceptionLogging.LogException(ex);
            }
            return Json(rs, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Download_Excel()
        {
            ExceptionLogging obj = new ExceptionLogging();
            if (Session["fPath"] == null)
            {
                obj.Logcreation("DownloadExcel Report Error - File path is null.");
                return RedirectToAction("Dashboard");
            }
            else
            {
                string fpath = Session["fPath"].ToString();
                obj.Logcreation("DownloadExcel Report Success in path : " + fpath);
                Session["fPath"] = null;
                FileInfo file = new FileInfo(fpath);
                byte[] fileBytes = System.IO.File.ReadAllBytes(fpath);
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, file.Name);
            }
        }
    }
}