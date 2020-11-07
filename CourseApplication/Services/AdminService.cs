using CourseApplication.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace courseApplication.Services
{
    public interface IAdminService
    {
        bool Login(string Email, string Password);
        bool ChangePassword(string Email, string Password);
        bool ForgetPassword(string Email);
    }

    public class AdminService : IAdminService
    {
        public courses_DBEntities context { get; set; }
        public AdminService()
        {
            context = new courses_DBEntities();
        }

        public bool Login(string Email, string Password)
        {
            return context.Admins.Where(e => e.E_mail == Email && e.Password == Password).Any();
        }
        public bool ChangePassword(string Email, string Password)
        {
            throw new NotImplementedException();
        }

        public bool ForgetPassword(string Email)
        {
            throw new NotImplementedException();
        }


    }
}