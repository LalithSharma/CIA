using CIA_BLL.ErrorHandling;
using CIA_BLL.IRepository;
using CIA_DLL;
using CIA_MAL.Model;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace CIA_BLL.Repository
{
    public class ReportRepository : IReport
    {
        ResultStatus rs = new ResultStatus();
        CIEntities1 db = new CIEntities1();
        public ResultStatus _Rptgridshow(int report_typ, string seldbranch, DateTime frmDate, DateTime endDate, string clientName, string report_options)
        {
            rs.Status = "Failure";
            try
            {
                if (report_typ == 1)
                {
                    List<clientRptView> a = new List<clientRptView>();
                    var getClientEntr_ = db.CIClientEntries
                    .Select(CE => new clientRptView
                    {
                        clientNo = CE.CIclientnon_ID,
                        clientName = CE.CICE_nomclient
                    }).ToList();

                    var getclientEntr2 = db.ci_reportview_(1, "", "", DateTime.Now, DateTime.Now).ToList();

                    for (int i = 0; i < getclientEntr2.Count; i++)
                    {
                        //getClientEntr_[i].clientName = getclientEntr2[i].CIIE_client_sel;
                        getClientEntr_[i].total_no_transactions = getclientEntr2[i].Total_no_transactions;
                        getClientEntr_[i].total_transaction_amt = getclientEntr2[i].Total_amt_transactions;
                    }

                    rs.obj = new
                    {
                        client_details = getClientEntr_,
                        //Inv_detail = getInv_detailData,
                        //Invoice_No = reqid,
                        //clientdrpdwn = getCE_drpdwn,
                    };
                    rs.Status = "Success";
                }
                else if(report_typ == 2 && report_options.Contains("a"))
                {

                }
                else if (report_typ == 2 && report_options.Contains("b"))
                {

                }
                else if (report_typ == 2 && report_options.Contains("c"))
                {
                     
                }
                else
                {
                    rs.Status = "Failure";
                    rs.MSG = "Invalid selection.";
                }
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
            return rs;
        }

        public void Excel_formatting_ce(DataTable dt, string exl_name)
        {
            try
            {
                //dt.Rows.RemoveAt(0);
                string excel_name = exl_name;
                string sheet_name = "CE_";

                ExcelPackage.LicenseContext = LicenseContext.Commercial;
                //using (ExcelPackage epackage = new ExcelPackage(file))
                using (ExcelPackage epackage = new ExcelPackage())
                {
                    string attachment = "attachment; filename=" + excel_name + "";
                    ExcelWorksheet excel = epackage.Workbook.Worksheets.Add(sheet_name);
                    excel.Cells["A1"].LoadFromDataTable(dt, true);

                    for (int i = 2; i < dt.Rows.Count + 2; i++)
                    {
                        string progress = null;
                        if (excel.Cells[i, 13].Value != null)
                        {
                            progress = excel.Cells[i, 13].Value.ToString().ToUpper();
                        }
                        else
                        {
                            progress = null;
                        }
                        if (progress == "Yes")
                        {
                            Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#f1eb25");
                            excel.Cells[i, 6].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            excel.Cells[i, 6].Style.Fill.BackgroundColor.SetColor(colFromHex);
                        }
                        else if (progress == "No")
                        {
                            Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#ff9994");
                            excel.Cells[i, 6].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            excel.Cells[i, 6].Style.Fill.BackgroundColor.SetColor(colFromHex);
                        }
                    }

                    for (int r = excel.Dimension.Start.Row; r < excel.Dimension.End.Row + 1; r++)
                    {
                        for (int c = excel.Dimension.Start.Column; c < excel.Dimension.End.Column + 1; c++)
                        {
                            var cellFont = excel.Cells[r, c].Style.Font;
                            if (excel.Cells[r, c].Value != null)
                            {
                                string cellValue = excel.Cells[r, c].Value.ToString();
                                if (r == 1)
                                {
                                    //rename Column name
                                    char[] chars = cellValue.ToCharArray();
                                    chars[0] = char.ToUpper(chars[0]);
                                    excel.Cells[r, c].Value = excel.Cells[r, c].Value.ToString().Replace("_", " ");

                                    //set background color
                                    Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#fdfd96");
                                    excel.Cells[r, c].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                    excel.Cells[r, c].Style.Fill.BackgroundColor.SetColor(colFromHex);

                                    // cellFont.SetFromFont(new Font("Calibri", 11));
                                    cellFont.Bold = true;
                                    excel.Cells[r, c].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                }
                                else
                                {
                                    //cellFont.SetFromFont(new Font("Calibri", 10));
                                    excel.Cells[r, c].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    excel.Cells[r, c].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                }

                                if (excel.Cells[r, c].Value.ToString() == "Empty")
                                {
                                    excel.Cells[r, c].Value = "";
                                }

                                //apply border
                                excel.Cells[r, c].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                excel.Cells[r, c].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                excel.Cells[r, c].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                excel.Cells[r, c].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            }
                            else
                            {
                                if (r == 1)
                                {
                                    //set background color
                                    Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#fd96fd");
                                    excel.Cells[r, c].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                    excel.Cells[r, c].Style.Fill.BackgroundColor.SetColor(colFromHex);

                                    //cellFont.SetFromFont(new Font("Calibri", 11));
                                    cellFont.Bold = true;
                                    excel.Cells[r, c].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                }
                                else
                                {
                                    //cellFont.SetFromFont(new Font("Calibri", 10));
                                    excel.Cells[r, c].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    excel.Cells[r, c].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                }


                                //apply border
                                excel.Cells[r, c].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                excel.Cells[r, c].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                excel.Cells[r, c].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                excel.Cells[r, c].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                //excel.Cells[i, j].Value = "-";
                            }
                        }
                    }


                    for (int r = excel.Dimension.Start.Row; r < excel.Dimension.End.Row + 1; r++)
                    {
                        for (int c = excel.Dimension.Start.Column; c < excel.Dimension.End.Column + 1; c++)
                        {
                            var cellFont = excel.Cells[r, c].Style.Font;
                            excel.Cells[r, c].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                            excel.DefaultRowHeight = 25.25;

                            if (excel.Cells[r, c].Value != null)
                            {
                                string cellValue = excel.Cells[r, c].Value.ToString();
                                if (r == 1)
                                {
                                    excel.Row(r).Height = 15;
                                    excel.Cells[r, c].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                    //if (cellValue.ToString() == "ie_Id")
                                    //{
                                    //    excel.Column(c).Width = 8.43;
                                    //}
                                    if (cellValue.ToString() == "Invoice No.")
                                    {
                                        excel.Column(c).Width = 10.86;
                                    }
                                    if (cellValue.ToString() == "Client Name")
                                    {
                                        excel.Column(c).Width = 12.43;
                                    }
                                    if (cellValue.ToString() == "Client Address")
                                    {
                                        excel.Column(c).Width = 40.00;
                                    }
                                    if (cellValue.ToString() == "Generated by")
                                    {
                                        excel.Column(c).Width = 26.57;
                                    }
                                    if (cellValue.ToString() == "Payment Terms")
                                    {
                                        excel.Column(c).Width = 10.86;
                                    }
                                    if (cellValue.ToString() == "Status")
                                    {
                                        excel.Column(c).Width = 4.57;
                                    }
                                    if (cellValue.ToString() == "Invoice Date")
                                    {
                                        excel.Column(c).Width = 15.57;
                                    }
                                }
                                else
                                {
                                    excel.Cells[r, c].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                }
                            }
                        }
                    }
                    //excel.Cells.AutoFitColumns();
                    excel.Cells.Style.WrapText = true;
                    excel.Protection.IsProtected = false;
                    excel.Protection.AllowSelectLockedCells = false;

                    string path_name = "~\\ExcelExport\\Report_" + DateTime.Now.ToString("yyyy") + "\\" + DateTime.Now.ToString("MMM-yyyy") + "\\" + DateTime.Now.ToString("yyyy-MM-dd");
                    string file_path = System.Web.HttpContext.Current.Server.MapPath(path_name);
                    string file_name = excel_name;
                    if (!Directory.Exists(file_path))
                    {
                        Directory.CreateDirectory(file_path);
                    }

                    var fileName = Path.Combine(file_path, file_name);
                    var file = new FileInfo(fileName);
                    epackage.SaveAs(file);
                    HttpContext.Current.Session["fPath"] = fileName;
                    epackage.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}