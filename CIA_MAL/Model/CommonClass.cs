using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CIA_MAL.Model
{
    public class CommonClass
    {
    }
    public class ResultStatus
    {
        public string Role { get; set; }
        public string Status { get; set; }
        public string MSG { get; set; }
        public object obj { get; set; }
    }


    public partial class LoginDetail
    {
        public int UserMst_ID { get; set; }
        public string LoginName { get; set; }
        public string UsrName { get; set; }
        public string Passwrd { get; set; }
        public string UsrRole { get; set; }
        public string is_Active { get; set; }

    }
    public class ClientEntr_info
    {
        public string clientNo { get; set; }
        public string client_name { get; set; }
        public string adresse { get; set; }
        public string codepostale { get; set; }
        public string ville { get; set; }
        public string pays { get; set; }
        public string mobileno { get; set; }
        public string courriel { get; set; }
        public string ligne_fixno { get; set; }
        public string organismsnom { get; set; }
        public string numero_professional { get; set; }
        public string facture_adresse { get; set; }
        public string O_codepostale { get; set; }
        public string Oville { get; set; }
        public string O_pays { get; set; }
        public string Ocorriel { get; set; }
        public string Oligne_fix { get; set; }
        public string O_mobileno { get; set; }
        public string payment_terms { get; set; }        
        public string comments { get; set; }
        public string CE_status { get; set; }
        public string created_by { get; set; }
        public string created_time { get; set; }
        public string upd_by { get; set; }
        public string upd_time { get; set; }
    }
    public class InvoiceEntr_info
    {
        public int CIE_id { get; set; }
        public string invoiceNo { get; set; }
        public string en_invno { get; set; }
        public DateTime sel_invc_date { get; set; }
        public DateTime invc_date { get; set; }
        public string ce_data { get; set; }
        public string invoc_genrated { get; set; }
        public string cl_org_address { get; set; }
        public string payment_trms { get; set; }        
        public string net_ht { get; set; }
        public string tva_euro { get; set; }
        public string net_non_taxble { get; set; }
        public string net_douance { get; set; }
        public string comments { get; set; }
        public string total_payble { get; set; }
        public string IE_status { get; set; }
        public string created_by { get; set; }
        public string created_time { get; set; }
        public string upd_by { get; set; }
        public string upd_time { get; set; }
    }
    public class InvoiceEntry_details
    {
        public string refernce { get; set; }
        public string declaration { get; set; }
        public string intrv_taxable { get; set; }
        public string intrv_nontaxable { get; set; }
        public string at_tva_douane { get; set; }
        public int tva { get; set; }
    }

    public class clientRptView
    {
        public string clientNo { get; set; }
        public string clientName { get; set; }
        public int? total_no_transactions { get; set; }
        public int? total_transaction_amt { get; set; }

    }
}