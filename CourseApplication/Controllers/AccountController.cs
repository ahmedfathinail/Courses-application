using CourseApplication.Data;
using CourseApplication.Models;
using CourseApplication.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CourseApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<MyIdentityUser> userManager;
        private readonly TraineeService traineeService;
        public AccountController()
        {
            var db = new CoursesIdentityContext();
            var userStore = new UserStore<MyIdentityUser>(db);
            userManager = new UserManager<MyIdentityUser>(userStore);
            traineeService = new TraineeService();
        }
        // GET: Account
        [AllowAnonymous]
        public ActionResult Login(string returnUrl="")
        {
           
            return View(new LoginViewModel
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel loginData)
        {
            if (ModelState.IsValid)
            { 
                 var existUser = await userManager.FindAsync(loginData.Email, loginData.Password);

                 if (existUser != null)
                 {
                     await signIn(existUser);
                     //Business
                     if (!string.IsNullOrEmpty(loginData.ReturnUrl))
                     {
                         return Redirect(loginData.ReturnUrl);
                     }

                     var userRoles = userManager.GetRoles(existUser.Id);
                     var role = userRoles.FirstOrDefault();
                     if (role == "Admin")
                     {
                         return RedirectToAction("Index", "Default",new { area = "Admin"});
                     }

                     return RedirectToAction("Index","Default");
                 }

                 loginData.Message = "Email or Password incorrect!";
            }

            return View(loginData);
        }

        private async Task signIn(MyIdentityUser myIdentityUser)
        {
            var identity = await userManager.CreateIdentityAsync(myIdentityUser, DefaultAuthenticationTypes.ApplicationCookie);
            var owinContext = Request.GetOwinContext();
            var authManager = owinContext.Authentication;
            authManager.SignIn(identity);
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel userInfo)
        {
            if (ModelState.IsValid)
            {
                var identityUser = new MyIdentityUser
                {
                    Email = userInfo.Email,
                    UserName = userInfo.Email
                };
                var creationResult = await userManager.CreateAsync(identityUser, userInfo.Password);
                // user created
                if (creationResult.Succeeded)
                {
                    var userId = identityUser.Id;
                    creationResult = userManager.AddToRole(userId, "Trainee");
                    // Role Assigned
                    if (creationResult.Succeeded)
                    {
                        // Save to trainee table
                        var savedTrainee = traineeService.Create(new Trainee
                        {
                            E_mail = userInfo.Email,
                            Name = userInfo.Name,
                            Is_Active = true,
                            Creation_date = DateTime.Now
                        });
                        if (savedTrainee == null)
                        {
                            userInfo.Message = "An Error while creating your Account";
                            return View(userInfo);
                        }
                        return RedirectToAction("Index", "Default");
                    }

                    
                }

                var message = creationResult.Errors.FirstOrDefault();
                userInfo.Message = message;

                return View(userInfo);
            }
            return View(userInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            var owinContext = Request.GetOwinContext();
            var authManager = owinContext.Authentication;
            authManager.SignOut("ApplicationCookie");
            Session.Abandon();
            return RedirectToAction("Index", "Default");
        }
    }
}