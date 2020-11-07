using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseApplication.Models
{
    public class TraineeCourseModel
    {
        public int Course_Id { get; set; }
        public System.DateTime Registration_Date { get; set; }

        public TraineeModel Trainee { get; set; }
    }
    public class TraineeModel
    {
        public string Name { get; set; }
        public string E_mail { get; set; }
    }
}