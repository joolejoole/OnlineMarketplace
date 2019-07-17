using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using JoJo.Models;
using JoJo.Core;
using JoJo.Service;

namespace JoJo.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public ActionResult Registration()
        {
            UserModel one = new UserModel();
            return View(one);
        }

        private Services ss;

        //Registration POST action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(UserModel user, HttpPostedFileBase file)
        {
            bool Status = false;
            string message = "";
            //Extract Image File Name.
            string fileName = Path.GetFileName(file.FileName);
            //Set the Image File Path.
            string imgP = Path.Combine(Server.MapPath("~/Src/Users"), fileName);
            file.SaveAs(imgP);
            // Model Validation
            if (ModelState.IsValid)
            {
                #region //Email is already Exist

                var isExist = IsEmailExist(user.Email);
                if (isExist)
                {
                    ModelState.AddModelError("EmailExist", "Email already exist");
                    return View(user);
                }

                #endregion //Email is already Exist

                #region Password Hashing

                user.Password = Crypto.Hash(user.Password);
                user.ConfirmPassword = Crypto.Hash(user.ConfirmPassword);

                #endregion Password Hashing

                JoJoEntities dc = new JoJoEntities();
                ss = new Services(dc);
                ss.saveUserReg(user, imgP);

                //Send Email to User
                //SendVerificationLinkEmail(user.EmailID, user.ActivationCode.ToString());
                message = "Registration successfully done with ID" + user.Email;
                Status = true;
            }
            else
            {
                message = "Invalid Request";
            }

            ViewBag.Message = message;
            ViewBag.Status = Status;
            return View(user);
        }

        //Login
        [HttpGet]
        public ActionResult Login()
        {
            UserLogin one = new UserLogin();
            return View(one);
        }

        //Login POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin login, string ReturnUrl = "")
        {
            string message = "";
            using (JoJoEntities dc = new JoJoEntities())
            {
                ss = new Services(dc);
                var v = dc.Users.Where(a => a.Email == login.EmailID).FirstOrDefault();
                if (v != null)
                {
                    if (string.Compare(Crypto.Hash(login.Password), v.Password) == 0)
                    {
                        int timeout = login.RememberMe ? 525600 : 20; // 525600 min = 1 year
                        var ticket = new FormsAuthenticationTicket(login.EmailID, login.RememberMe, timeout);
                        string encrypted = FormsAuthentication.Encrypt(ticket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                        cookie.Expires = DateTime.Now.AddMinutes(timeout);
                        cookie.HttpOnly = true;
                        Response.Cookies.Add(cookie);

                        if (Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        message = "Please create an account or Enter correct credentials";
                    }
                }
                else
                {
                    message = "Please create an account or Enter correct credentials";
                }
            }
            ViewBag.Message = message;
            return View();
        }

        //Logout

        [HttpPost]
        public ActionResult Logout()
        {
            //FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User");
        }

        [NonAction]
        public bool IsEmailExist(string emailID)
        {
            JoJoEntities dc = new JoJoEntities();
            //ss = new Services(dc);

            var v = dc.Users.Where(a => a.Email == emailID).FirstOrDefault();
            return v != null;
            // return ss.isEmail(emailID);
        }
    }
}