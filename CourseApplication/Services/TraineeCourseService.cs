using CourseApplication.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseApplication.Services
{
    public interface ITraineeCourseService
    {
        IEnumerable<Trainee_Courses> GetTrainees(int? CourseId = null);
    }
    public class TraineeCourseService : ITraineeCourseService
    {
        private readonly courses_DBEntities db;
        public TraineeCourseService()
        {
            db = new courses_DBEntities();
        }
        public IEnumerable<Trainee_Courses> GetTrainees(int? CourseId = null)
        {
            return db.Trainee_Courses.Where(t => CourseId == null || t.Course_Id == CourseId);
        }
    }
}