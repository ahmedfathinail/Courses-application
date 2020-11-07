using courseApplication.Services;
using CourseApplication.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseApplication.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        // GET: Admin/Account/Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel logInfo)
        {
            var adminService = new AdminService();
            var isLoggedIn = adminService.Login(logInfo.Email, logInfo.Password);
            if (isLoggedIn)
            {
                return RedirectToAction("Index", "Default");
            }
            else
            {
                logInfo.Message = "Email or Password is incorrect!";
                return View(logInfo);
            }
        }
    }
}