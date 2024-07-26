using CIA_BLL.ErrorHandling;
using CIA_BLL.IRepository;
using CIA_DLL;
using CIA_MAL.Model;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.SqlServer;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace CIA_BLL.Repository
{
    public class InvoiceRepository : IInvoiceentry
    {
        ResultStatus rs = new ResultStatus();
        CIEntities1 db = new CIEntities1();

        public ResultStatus load_InvEntry()
        {
            rs.Status = "Failure";
            try
            {
                string prefix_count = "";
                var counter_ID = db.CIInvoiceEntries.Count();
                string getinstring = counter_ID.ToString();
                if (getinstring.Length == 1)
                {
                    prefix_count = "0000";
                }
                else if (getinstring.Length == 2)
                {
                    prefix_count = "000";
                }
                else if (getinstring.Length == 3)
                {
                    prefix_count = "00";
                }
                else if (getinstring.Length == 4)
                {
                    prefix_count = "0";
                }
                else
                {
                    prefix_count = "";
                }
                string reqid = "BR1-" + prefix_count + "" + getinstring + "";

                var getInv_headerData = db.CIInvoiceEntries.Select(IE => new { 
                
                    ie_Id = IE.CIIE__ID,
                    ie_invNo = IE.CIIE_invoice_no_ID,
                    ie_client = IE.CIIE_client_sel,
                    ie_clientAdress = IE.CIIE_clientororgan_adresse,
                    ie_invgenrtdBy = IE.CIIE_invoice_genrated_by,
                    ie_paymntTrms = IE.CIIE_payment_trms,
                    ie_status = IE.CIIE_ActiveStatus,
                    ie_invDdate = SqlFunctions.DateName("yyyy", IE.CIIE_invoice_currdate)+ "-" +
                                  SqlFunctions.DateName("MM", IE.CIIE_invoice_currdate) + "-" +
                                  SqlFunctions.DateName("dd", IE.CIIE_invoice_currdate),
                }).ToList();
                var getInv_detailData = db.CIInvoiceDetails.ToList();
                //rs.obj = getData;

                var getCE_drpdwn = db.CIClientEntries.Where(a=>a.CICE_ActiveStatus == "A").Select(s => new
                {
                    ce_name = s.CICE_nomclient,
                    ce_id = s.CIclientnon_ID
                });

                var getClientEntr_ = db.CIClientEntries.Where(o => o.CICE_ActiveStatus.Contains("A")).ToList();
                 
                var getBranchMst_ = db.CIBranchMsts.Where(u => u.CBM_ActiveStatus.Contains("A")).ToList();

                rs.obj = new
                {
                    Grid_listed = getInv_headerData,
                    Inv_detail = getInv_detailData,
                    Invoice_No = reqid,
                    clientdrpdwn = getCE_drpdwn,
                    client_details = getClientEntr_,
                    brchMst_details = getBranchMst_

                };
                rs.Status = "Success";
            }
            catch (Exception ex)
            {
                rs.Status = "Failure";
                rs.MSG = ex.StackTrace;
                ExceptionLogging.LogException(ex);
            }
            return rs;
        }
        public ResultStatus add_IE(List<InvoiceEntr_info> InvoiceEntry_header, List<InvoiceEntry_details> InvoiceEntry_dtls, string clicked_state)
        {
            rs.Status = "Failure";
            try
            {
                if (clicked_state.Contains("post_invoice"))
                {
                    db.CIInvoiceEntries.Add(new CIInvoiceEntry
                    {
                        CIIE_invoice_no_ID = InvoiceEntry_header[0].invoiceNo,
                      //  CIIE_entrd_invoice_no = InvoiceEntry_header[0].en_invno,
                        CIIE_invoice_currdate = InvoiceEntry_header[0].invc_date,
                      //  CIIE_invoice_seldate = InvoiceEntry_header[0].sel_invc_date,
                        CIIE_client_sel = InvoiceEntry_header[0].ce_data,
                        CIIE_invoice_genrated_by = InvoiceEntry_header[0].invoc_genrated,
                        CIIE_clientororgan_adresse = InvoiceEntry_header[0].cl_org_address,
                        CIIE_payment_trms = InvoiceEntry_header[0].payment_trms,
                        CIIE_net_ht = InvoiceEntry_header[0].net_ht,
                        CIIE_tva_euro = InvoiceEntry_header[0].tva_euro,
                        CIIE_net_non_taxable = InvoiceEntry_header[0].net_non_taxble,
                        CIIE_net_douane = InvoiceEntry_header[0].net_douance,
                        CIIE_total_payment_amt = Convert.ToInt32(InvoiceEntry_header[0].total_payble),
                        CIIE_comments = InvoiceEntry_header[0].comments,
                        CIIE_ActiveStatus = InvoiceEntry_header[0].IE_status,
                        CIIE_CreateBy = HttpContext.Current.Session["LoginName"].ToString(),
                        CIIE_CreateOn = DateTime.Now,
                        CIIE_UpdateBy = null,
                        CIIE_UpdateOn = null
                    });

                    foreach (var m in InvoiceEntry_dtls) {
                      db.CIInvoiceDetails.Add(new CIInvoiceDetail
                        {
                            CIIE_invoice_no_ID = InvoiceEntry_header[0].invoiceNo,
                            CIID_refernce = InvoiceEntry_dtls[0].refernce,
                            CIID_n_declaration = InvoiceEntry_dtls[0].declaration,
                            CIID_intervention_taxable = InvoiceEntry_dtls[0].intrv_taxable,
                            CIID_intervention_nontaxable = InvoiceEntry_dtls[0].intrv_nontaxable,
                            CIID_at_tva_douane = InvoiceEntry_dtls[0].at_tva_douane,
                            CIID_tva_perctage = InvoiceEntry_dtls[0].tva,
                            CIID_UpdateBy = HttpContext.Current.Session["LoginName"].ToString(),
                            CIID_UpdateOn = DateTime.Now
                        });
                    }
                    db.SaveChanges();
                    rs.Status = "Success";
                    rs.MSG = "Invoice Added Successfully";
                }
                else
                {
                    string invoice_noToupd = InvoiceEntry_header[0].en_invno.ToString();

                    var update_IE = db.CIInvoiceEntries.Where(u => u.CIIE_invoice_no_ID == invoice_noToupd).ToList();

                    if (update_IE.Count > 0)
                    {
                        update_IE[0].CIIE_comments = InvoiceEntry_header[0].comments;
                        update_IE[0].CIIE_ActiveStatus = InvoiceEntry_header[0].IE_status;
                        update_IE[0].CIIE_UpdateBy = HttpContext.Current.Session["LoginName"].ToString();
                        update_IE[0].CIIE_UpdateOn = DateTime.Now;
                    }
                        db.SaveChanges();
                        rs.Status = "Success";
                        rs.MSG = "Invoice Updated Successfully";
                    }
                
            }
            catch (Exception ex)
            {
                rs.Status = "Failure";
                rs.MSG = ex.StackTrace;
                ExceptionLogging.LogException(ex);
            }
            return rs;
        }
        public ResultStatus EditIE_row(int row_id)
        {
            rs.Status = "Failure";
            try
            {
                if (row_id !=0)
                {
                    //var getonEdit_header = db.CIInvoiceEntries.Where(s => s.CIIE__ID == row_id).FirstOrDefault();

                    var getonEdit_header = db.CIInvoiceEntries.Where(s => s.CIIE__ID == row_id).Select(IE => new {

                        ie_Id = IE.CIIE__ID,
                        ie_invNo = IE.CIIE_invoice_no_ID,
                        ie_client = IE.CIIE_client_sel,
                        ie_clientAdress = IE.CIIE_clientororgan_adresse,
                        ie_invgenrtdBy = IE.CIIE_invoice_genrated_by,
                        ie_paymntTrms = IE.CIIE_payment_trms,
                        ie_status = IE.CIIE_ActiveStatus,
                        ie_invDdate = SqlFunctions.DateName("m", IE.CIIE_invoice_currdate) + "-" +
                                  SqlFunctions.DateName("dd", IE.CIIE_invoice_currdate) + "-" +
                                  SqlFunctions.DateName("yyyy", IE.CIIE_invoice_currdate),
                        ie_netht = IE.CIIE_net_ht,
                        ie_tvaeuro = IE.CIIE_tva_euro,
                        ie_netnontaxble = IE.CIIE_net_non_taxable,
                        ie_netdouane = IE.CIIE_net_douane,
                        ie_totalpay = IE.CIIE_total_payment_amt,
                        ie_comment = IE.CIIE_comments
                    }).FirstOrDefault();

                    string invc_code = getonEdit_header.ie_invNo.ToString();
                    var getonEdit_details = db.CIInvoiceDetails.Where(u => u.CIIE_invoice_no_ID == invc_code).Select(ie_dtl =>new {
                        
                        refernce = ie_dtl.CIID_refernce,
                        declaration = ie_dtl.CIID_n_declaration,
                        intrv_taxable = ie_dtl.CIID_intervention_taxable,
                        intrv_nontaxable = ie_dtl.CIID_intervention_nontaxable,
                        at_tva_douane = ie_dtl.CIID_at_tva_douane,
                        tvaperc = ie_dtl.CIID_tva_perctage

                    }).ToList();

                    rs.obj = new
                    {
                        getonEdit_header = getonEdit_header,
                        getonEdit_details = getonEdit_details
                    };                    
                    rs.Status = "Success";
                }
            }
            catch (Exception ex)
            {
                rs.Status = "Failure";
                rs.MSG = ex.StackTrace;
                ExceptionLogging.LogException(ex);
            }
            return rs;
        }

        public ResultStatus InvoicEntry_ExcelDwn(string excel_name, string tab_value)
        {
            rs.Status = "Failure";
            try
            {
                DataTable final_dt = new DataTable();
                DataTable dt = new DataTable();
                ExceptionLogging obj = new ExceptionLogging();
              
                    var IErpt_ = db.CIInvoiceEntries.Select(IE => new {

                        // ie_Id = IE.CIIE__ID,
                        Invoice_No = IE.CIIE_invoice_no_ID,
                        Client_Name = IE.CIIE_client_sel,
                        Client_Address = IE.CIIE_clientororgan_adresse,
                        Invoice_Generated_by = IE.CIIE_invoice_genrated_by,
                        Payment_Terms = IE.CIIE_payment_trms,
                        Status = IE.CIIE_ActiveStatus,
                        Invoice_Date = SqlFunctions.DateName("yyyy", IE.CIIE_invoice_currdate) + "-" +
                                  SqlFunctions.DateName("MM", IE.CIIE_invoice_currdate) + "-" +
                                  SqlFunctions.DateName("dd", IE.CIIE_invoice_currdate),
                    }).ToList();

                DataTable t_act = obj.ListToDataTable(IErpt_);
                    t_act.Rows.Add();
                    t_act.AcceptChanges();

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        dt.Merge(t_act);
                    }
                    else
                    {
                        dt = t_act;
                    }
                    dt.AcceptChanges();

                    dt.AcceptChanges();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        Excel_formatting_cmpled(dt, excel_name);
                    }
                    rs.Status = "Success";
                    
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

        public void Excel_formatting_cmpled(DataTable dt, string exl_name)
        {
            try
            {
                //dt.Rows.RemoveAt(0);
                string excel_name = exl_name;
                string sheet_name = "IE_";

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