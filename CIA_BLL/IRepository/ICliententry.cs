using CIA_MAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CIA_BLL.IRepository
{
    public interface ICliententry
    { //Client entry repos
        ResultStatus add_CE(List<ClientEntr_info> ClientEntry_list, string clicked_state);
        ResultStatus GetgridData();
        ResultStatus EditCE_row(string row_id);

        ResultStatus ClientEntry_ExcelDwn(string excel_name, string tab_value);
    }
}