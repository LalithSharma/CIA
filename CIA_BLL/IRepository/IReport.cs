using CIA_MAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CIA_BLL.IRepository
{
    public interface IReport
    {
        //ResultStatus DetailedRpt_ExcelDwn(string excel_name, string sheet_name, string table_id, string fyear, string quart, string assessment_type);
        //ResultStatus CmpltedRpt_ExcelDwn(string excel_name, string sheet_name, string table_id, string fyear, string quart, string typ_report);

        ResultStatus _Rptgridshow(int report_typ, string seldbranch, DateTime frmDate, DateTime endDate, string clientName, string report_options);
    }
}