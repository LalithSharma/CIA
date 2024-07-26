using CIA_MAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CIA_BLL.IRepository
{
    public interface IInvoiceentry
    {
        ResultStatus load_InvEntry();
        ResultStatus add_IE(List<InvoiceEntr_info> InvoiceEntry_header, List<InvoiceEntry_details> InvoiceEntry_dtls, string clicked_state);
        ResultStatus EditIE_row(int seldID);

        ResultStatus InvoicEntry_ExcelDwn(string excel_name, string tab_value);
    }
}