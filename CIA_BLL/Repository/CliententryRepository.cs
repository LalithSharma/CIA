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
    public class CliententryRepository : ICliententry
    {
        ResultStatus rs = new ResultStatus();
        CIEntities1 db = new CIEntities1();

        public ResultStatus GetgridData()
        {
            rs.Status = "Failure";
            try
            {
                string prefix_count = "";
                var counter_ID = db.CIClientEntries.Count();
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
                string reqid = "CNON-" + prefix_count + "" + getinstring + "";

                var getData = db.CIClientEntries.ToList();
                //rs.obj = getData;
                rs.obj = new
                {
                    Grid_listed = getData,
                    ClientNo = reqid,
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
        public ResultStatus add_CE(List<ClientEntr_info> ClientEntry_list, string clicked_state)
        {
            rs.Status = "Failure";
            try
            {
                if (clicked_state.Contains("add_client"))
                {
                    db.CIClientEntries.Add(new CIClientEntry
                    {
                        CIclientnon_ID = ClientEntry_list[0].clientNo,
                        CICE_nomclient = ClientEntry_list[0].client_name,
                        CICE_c_adresse = ClientEntry_list[0].adresse,
                        CICE_c_codepostale = ClientEntry_list[0].codepostale,
                        CICE_c_ville = ClientEntry_list[0].ville,
                        CICE_c_pays = ClientEntry_list[0].pays,
                        CICE_c_courriel = ClientEntry_list[0].courriel,
                        CICE_c_lignefixe_non = ClientEntry_list[0].ligne_fixno,
                        CICE_c_mobile_non = ClientEntry_list[0].mobileno,
                        CICE_nom_organisme = ClientEntry_list[0].organismsnom,
                        CICE_numero_enregistr_professional = ClientEntry_list[0].numero_professional,
                        CICE_facture_adresse = ClientEntry_list[0].facture_adresse,
                        CICE_o_codepostale = ClientEntry_list[0].O_codepostale,
                        CICE_o_ville = ClientEntry_list[0].Oville,
                        CICE_o_pays = ClientEntry_list[0].O_pays,
                        CICE_o_courriel = ClientEntry_list[0].Ocorriel,
                        CICE_o_lignefixe_non = ClientEntry_list[0].Oligne_fix,
                        CICE_o_mobile_non = ClientEntry_list[0].O_mobileno,
                        CICE_payment_terms = ClientEntry_list[0].payment_terms,
                        CICE_comments = ClientEntry_list[0].comments,
                        CICE_ActiveStatus = ClientEntry_list[0].CE_status,
                        CICE_CreateBy = HttpContext.Current.Session["LoginName"].ToString(),
                        CICE_CreateOn = DateTime.Now,
                        CICE_UpdateBy = null,
                        CICE_UpdateOn = null
                    });
                    db.SaveChanges();
                    rs.Status = "Success";
                    rs.MSG = "Client Added Successfully";
                }
                else 
                {
                    string CEnon = ClientEntry_list[0].clientNo.ToString();
                    var updateCE_data = db.CIClientEntries.Where(u => u.CIclientnon_ID == CEnon).ToList();
                    if (updateCE_data.Count > 0)
                    {
                        //updateCE_data[0].CIclientnon_ID = ClientEntry_list[0].clientNo;
                        updateCE_data[0].CICE_nomclient = ClientEntry_list[0].client_name;
                        updateCE_data[0].CICE_c_adresse = ClientEntry_list[0].adresse;
                        updateCE_data[0].CICE_c_codepostale = ClientEntry_list[0].codepostale;
                        updateCE_data[0].CICE_c_ville = ClientEntry_list[0].ville;
                        updateCE_data[0].CICE_c_pays = ClientEntry_list[0].pays;
                        updateCE_data[0].CICE_c_courriel = ClientEntry_list[0].courriel;
                        updateCE_data[0].CICE_c_lignefixe_non = ClientEntry_list[0].ligne_fixno;
                        updateCE_data[0].CICE_c_mobile_non = ClientEntry_list[0].mobileno;
                        updateCE_data[0].CICE_nom_organisme = ClientEntry_list[0].organismsnom;
                        updateCE_data[0].CICE_numero_enregistr_professional = ClientEntry_list[0].numero_professional;
                        updateCE_data[0].CICE_facture_adresse = ClientEntry_list[0].facture_adresse;
                        updateCE_data[0].CICE_o_codepostale = ClientEntry_list[0].O_codepostale;
                        updateCE_data[0].CICE_o_ville = ClientEntry_list[0].Oville;
                        updateCE_data[0].CICE_o_pays = ClientEntry_list[0].O_pays;
                        updateCE_data[0].CICE_o_courriel = ClientEntry_list[0].Ocorriel;
                        updateCE_data[0].CICE_o_lignefixe_non = ClientEntry_list[0].Oligne_fix;
                        updateCE_data[0].CICE_o_mobile_non = ClientEntry_list[0].O_mobileno;
                        updateCE_data[0].CICE_payment_terms = ClientEntry_list[0].payment_terms;
                        updateCE_data[0].CICE_comments = ClientEntry_list[0].comments;
                        updateCE_data[0].CICE_ActiveStatus = ClientEntry_list[0].CE_status;
                        //updateCE_data[0].CICE_CreateBy = HttpContext.Current.Session["LoginName"].ToString(),
                        //updateCE_data[0].CICE_CreateOn = 
                        updateCE_data[0].CICE_UpdateBy = HttpContext.Current.Session["LoginName"].ToString();
                        updateCE_data[0].CICE_UpdateOn = DateTime.Now;

                        db.SaveChanges();
                        rs.Status = "Success";
                        rs.MSG = "Client Updated Successfully";
                    }
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
        public ResultStatus EditCE_row(string row_id)
        {
            rs.Status = "Failure";
            try
            {
                if (!string.IsNullOrEmpty(row_id))
                {
                    var getonEdit_Data = db.CIClientEntries.Where(s=>s.CIclientnon_ID.Contains(row_id)).FirstOrDefault();
                    rs.obj = getonEdit_Data;
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

        public ResultStatus ClientEntry_ExcelDwn(string excel_name, string tab_value)
        {
            rs.Status = "Failure";
            try
            {
                DataTable final_dt = new DataTable();
                DataTable dt = new DataTable();
                ExceptionLogging obj = new ExceptionLogging();

                var IErpt_ = db.CIClientEntries
                    //.Select(CE => new {

                    //// ie_Id = IE.CIIE__ID,
                    //Invoice_No = CE.CIIE_invoice_no_ID,
                    //Client_Name = CE.CIIE_client_sel,
                    //Client_Address = CE.CIIE_clientororgan_adresse,
                    //Invoice_Generated_by = CE.CIIE_invoice_genrated_by,
                    //Payment_Terms = CE.CIIE_payment_trms,
                    //Status = CE.CIIE_ActiveStatus,
                    //Invoice_Date = SqlFunctions.DateName("yyyy", IE.CIIE_invoice_currdate) + "-" +
                    //          SqlFunctions.DateName("MM", IE.CIIE_invoice_currdate) + "-" +
                    //          SqlFunctions.DateName("dd", IE.CIIE_invoice_currdate),
                .ToList();

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