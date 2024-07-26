using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CIA_BLL.IRepository;
using CIA_DLL;
using CIA_MAL.Model;

namespace CIA_BLL.Repository
{
    public class SecurityValdiate : ISecurityValdiate
    {
        CIEntities1 db = new CIEntities1();
        public ResultStatus Loginchecks(string username,string password)
        {
            ResultStatus r_status = new ResultStatus();

            // user = aUser, pass = pass123
            var is_userAvailable = db.CIUserMasters.Where(u => u.CIUM_LoginName == username).FirstOrDefault();
            if(is_userAvailable != null)
            {
                if(!is_userAvailable.CIUM_LoginName.Contains(username))
                {
                    r_status.Status = "Failure";
                    r_status.MSG = "The entered LoginName are Incorrect.";
                    return r_status;
                }
                else if (!is_userAvailable.CIUM_Password.Contains(password))
                {
                    r_status.Status = "Failure";
                    r_status.MSG = "The entered Password are Incorrect.";
                    return r_status;
                }
                else
                {
                    r_status.Status = "Success";
                    r_status.obj = new LoginDetail
                    {
                        UserMst_ID = is_userAvailable.CIUserMst_ID,
                        LoginName = is_userAvailable.CIUM_LoginName,
                        UsrName = is_userAvailable.CIUM_UserName,
                        Passwrd = is_userAvailable.CIUM_Password,
                        UsrRole = is_userAvailable.CIUM_UserRole,
                        is_Active = is_userAvailable.CIUM_ActiveStatus,                        
                    };
                }
            }
            else
            {
                r_status.Status = "Failure";
                r_status.MSG = "The Login user are not available.";
            }
           
            return r_status;
        }

        public ResultStatus LogOutInser(string username)
        {
            ResultStatus r_status = new ResultStatus();

            r_status.Status = "Success";

            return r_status;
        }
    }
}