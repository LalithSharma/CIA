using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CIA_MAL.Model;

namespace CIA_BLL.IRepository
{
    public interface ISecurityValdiate
    {
        ResultStatus Loginchecks(string username,string password);
        ResultStatus LogOutInser(string username);

    }
}