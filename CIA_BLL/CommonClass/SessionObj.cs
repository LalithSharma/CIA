using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace CIA_BLL.CommonClass
{
    public class SessionObj : IRequiresSessionState
    {
        public static string UserMst_ID
        {
            get { return (string)HttpContext.Current.Session["UserMst_ID"]; }
            set { HttpContext.Current.Session.Add("UserMst_ID", value); }
        }
        public static string LoginName
        {
            get { return (string)HttpContext.Current.Session["LoginName"]; }
            set { HttpContext.Current.Session.Add("LoginName", value); }
        }

        public static string Passwrd
        {
            get { return (string)HttpContext.Current.Session["Passwrd"]; }
            set { HttpContext.Current.Session.Add("Passwrd", value); }
        }

        public static string UsrName
        {
            get { return (string)HttpContext.Current.Session["UsrName"]; }
            set { HttpContext.Current.Session.Add("UsrName", value); }
        }

        public static string UsrRole
        {
            get { return (string)HttpContext.Current.Session["UsrRole"]; }
            set { HttpContext.Current.Session.Add("UsrRole", value); }
        }

        public static string is_Active
        {
            get { return (string)HttpContext.Current.Session["is_Active"]; }
            set { HttpContext.Current.Session.Add("is_Active", value); }
        }
    }
}