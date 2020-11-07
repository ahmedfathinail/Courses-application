using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseApplication.Data
{
    public class CoursesIdentityContext : IdentityDbContext<MyIdentityUser>
    {
        public CoursesIdentityContext():base("courses_connection")
        {
                
        }
    }

    public class MyIdentityUser : IdentityUser
    {

    }
}