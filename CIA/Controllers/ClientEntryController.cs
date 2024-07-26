using CIA_BLL.ErrorHandling;
using CIA_BLL.IRepository;
using CIA_BLL.Repository;
using CIA_MAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CIA.Controllers
{
    public class ClientEntryController : Controller
    {
        ICliententry CE_repos = new CliententryRepository();
        ResultStatus rs = new ResultStatus();
        // GET: ClientEntry
        public ActionResult ClientEntry()
        {
            return View();
        }

        public JsonResult ExistingCEData()
        {
            rs.Status = "Failure";
            try
            {
                rs = CE_repos.GetgridData();
            }
            catch (Exception ex)
            {
                rs.Status = "Failure";
                rs.MSG = ex.StackTrace;
                ExceptionLogging.LogException(ex);
            }
            return Json(rs, JsonRequestBehavior.AllowGet);
        }
        public JsonResult addClientEntry(List<ClientEntr_info> ClientEntry_list, string clicked_state)
        {            
            rs.Status = "Failure";
            try
            {
                rs = CE_repos.add_CE(ClientEntry_list, clicked_state);
            }
            catch (Exception ex)
            {
                rs.Status = "Failure";
                rs.MSG = ex.StackTrace;
                ExceptionLogging.LogException(ex);
            }
            return Json(rs, JsonRequestBehavior.AllowGet);
        }
        public JsonResult onEditCEData(string selid)
        {
            rs.Status = "Failure";
            try
            {
                rs = CE_repos.EditCE_row(selid);
            }
            catch (Exception ex)
            {
                rs.Status = "Failure";
                rs.MSG = ex.StackTrace;
                ExceptionLogging.LogException(ex);
            }
            return Json(rs, JsonRequestBehavior.AllowGet);
        }

        public JsonResult onClk_CEExcel(string tab_val)
        {
            ResultStatus rs = new ResultStatus();
            try
            {
                string excel_filename = "CI_ClientEntry_" + DateTime.Now.ToString("yyyymmddHHmmss") + ".xlsx";
                rs = CE_repos.ClientEntry_ExcelDwn(excel_filename, tab_val);
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
    }
}