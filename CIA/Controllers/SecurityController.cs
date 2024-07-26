using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CIA_MAL.Model;
using CIA_BLL.IRepository;
using CIA_BLL.Repository;
using CIA_BLL.CommonClass;
using CIA_BLL.ErrorHandling;
using System.Drawing;
using System.Text;
using System.IO;

namespace CIA.Controllers
{
    public class SecurityController : Controller
    {
        ISecurityValdiate Login_valid = new SecurityValdiate();
        // GET: Security
        public ActionResult Login()
        {
            Session["CAPTCHA"] = GetRandomText();
            ViewBag.randomCaptcha = Session["CAPTCHA"].ToString();
            return View();
        }

        public JsonResult Loginchecks(string username, string password,string c_Captcha)
        {
            ResultStatus rs = new ResultStatus();
            try
            {
                Session["user_ad"] = username.ToString();

                string clientCaptcha = c_Captcha;
                string serverCaptcha = Session["CAPTCHA"].ToString();

                if (!clientCaptcha.Equals(serverCaptcha))
                {
                   // ViewBag.CaptchaError = "Sorry, please write exact text as written above.";
                    rs.Status = "Failure";
                    rs.MSG = "Sorry, Invalid Captcha! Write exact text as given.";
                    //return View();
                }
                else
                {
                    // continue with login validation
                    rs = Login_valid.Loginchecks(username, password);
                    if (rs.Status == "Success")
                    {
                        SessionObj.LoginName = username;
                        var res = (LoginDetail)rs.obj;
                        SessionObj.UserMst_ID = Convert.ToString(res.UserMst_ID);
                        SessionObj.LoginName = res.LoginName;
                        SessionObj.UsrName = res.UsrName;
                        SessionObj.Passwrd = res.Passwrd;
                        SessionObj.UsrRole = res.UsrRole;
                        SessionObj.is_Active = res.is_Active;
                        rs.Role = res.UsrRole;
                    }
                    else
                    {
                        rs.Status = "Failure";
                        //rs.MSG = "You are not authorized to ‘Sign in’";
                    }
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
            return Json(rs, JsonRequestBehavior.AllowGet);
        }

        private string GetRandomText()
        {
            StringBuilder randomText = new StringBuilder();
            string alphabets = "012345679ACEFGHKLMNPRSWXZabcdefghijkhlmnopqrstuvwxyz";
            Random r = new Random();
            for (int j = 0; j <= 5; j++)
            {
                randomText.Append(alphabets[r.Next(alphabets.Length)]);
            }
            return randomText.ToString();
        }
        
        public FileResult GetCaptchaImage()
        {
            string text = Session["CAPTCHA"].ToString();

            //first, create a dummy bitmap just to get a graphics object
            Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);

            Font font = new Font("Arial", 15);
            //measure the string to see how big the image needs to be
            SizeF textSize = drawing.MeasureString(text, font);

            //free up the dummy image and old graphics object
            img.Dispose();
            drawing.Dispose();

            //create a new image of the right size
            img = new Bitmap((int)textSize.Width + 40, (int)textSize.Height + 20);
            drawing = Graphics.FromImage(img);

            Color backColor = Color.SeaShell;
            Color textColor = Color.Red;
            //paint the background
            drawing.Clear(backColor);

            //create a brush for the text
            Brush textBrush = new SolidBrush(textColor);

            drawing.DrawString(text, font, textBrush, 20, 10);

            drawing.Save();

            font.Dispose();
            textBrush.Dispose();
            drawing.Dispose();

            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            img.Dispose();

            return File(ms.ToArray(), "image/png");
        }

        public ActionResult Logout()
        {
            if (Session["user_ad"] != null)
            {
                Login_valid.LogOutInser(Convert.ToString(Session["user_ad"]));
                Session["Display_name"] = null;
                Session["user_ad"] = null;
                Session["manager_ad_id"] = null;
                Session["is_hr"] = null;
                Session["is_pm"] = null;

                Session.Clear();
                Session.Abandon();
            }
            return View("Login");
        }

        public JsonResult BrowserTabTExit()
        {
            var a = HttpContext.Request.Cookies["RefreshFilter"];
            long result = 0;

            if (Session["user_ad"] != null)
            {
                //if (Session["clickedFrm"] == null)
                //{                            
                Login_valid.LogOutInser(Convert.ToString(Session["user_ad"]));
                Session["Display_name"] = null;
                Session["user_ad"] = null;
                Session["manager_ad_id"] = null;
                Session["is_hr"] = null;
                Session["is_pm"] = null;

                Session.Clear();
                Session.Abandon();
                //}
            }
            return Json(result);

        }
    }
}