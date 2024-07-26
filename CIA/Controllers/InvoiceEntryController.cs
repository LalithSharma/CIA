using CIA_BLL.ErrorHandling;
using CIA_BLL.IRepository;
using CIA_BLL.Repository;
using CIA_MAL.Model;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CIA.Controllers
{
    public class InvoiceEntryController : Controller
    {
        // GET: InvoiceEntry
        IInvoiceentry IE_repos = new InvoiceRepository();
        ResultStatus rs = new ResultStatus();
        public ActionResult InvoiceEntry()
        {
            return View();
        }
        public JsonResult CEdata_load()
        {
            rs.Status = "Failure";
            try
            {
                rs = IE_repos.load_InvEntry();
            }
            catch (Exception ex)
            {
                rs.Status = "Failure";
                rs.MSG = ex.StackTrace;
                ExceptionLogging.LogException(ex);
            }
            return Json(rs, JsonRequestBehavior.AllowGet);
        }
        public JsonResult addInvoiceEntry(List<InvoiceEntr_info> InvoiceEntry_header, List<InvoiceEntry_details> InvoiceEntry_dtls, string clicked_state)
        {
            rs.Status = "Failure";
            try
            {
                rs = IE_repos.add_IE(InvoiceEntry_header, InvoiceEntry_dtls, clicked_state);
            }
            catch (Exception ex)
            {
                rs.Status = "Failure";
                rs.MSG = ex.StackTrace;
                ExceptionLogging.LogException(ex);
            }
            return Json(rs, JsonRequestBehavior.AllowGet);
        }
        public JsonResult onEditIEData(int selid)
        {
            rs.Status = "Failure";
            try
            {
                rs = IE_repos.EditIE_row(selid);
            }
            catch (Exception ex)
            {
                rs.Status = "Failure";
                rs.MSG = ex.StackTrace;
                ExceptionLogging.LogException(ex);
            }
            return Json(rs, JsonRequestBehavior.AllowGet);
        }
        public FileResult getPDFView()
        {
            //string ReportURL = "{Your File Path}";
            string ReportURL = Server.MapPath("~/Content/PdfPaperSet");
            byte[] FileBytes = System.IO.File.ReadAllBytes(ReportURL);
            return File(FileBytes, "application/pdf");
        }

        public FileResult Export(string GridHtml)
        {
            using (MemoryStream stream = new System.IO.MemoryStream())
            {
                StringReader sr = new StringReader(GridHtml);
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                pdfDoc.Close();
                return File(stream.ToArray(), "application/pdf", "Grid.pdf");
            }
        }

        public JsonResult onClk_IEExcel(string tab_val)
        {
            ResultStatus rs = new ResultStatus();
            try
            {
                string excel_filename = "CI_InvoiceEntry_" + DateTime.Now.ToString("yyyymmddHHmmss") + ".xlsx";
                rs = IE_repos.InvoicEntry_ExcelDwn(excel_filename, tab_val);
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