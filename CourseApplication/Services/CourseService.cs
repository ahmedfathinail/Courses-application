using CourseApplication.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseApplication.Services
{
    public interface ICourseService
    {
        int Create(Course course);
        List<Course> ReadAll(string query=null,int? categoryId = null,int? trainerId = null);
        Course GetById(int id);
        int Update(Course updatedCourse);
    }
    public class CourseService : ICourseService
    {
        private readonly courses_DBEntities db;
        public CourseService()
        {
            db = new courses_DBEntities();
        }
        public int Create(Course course)
        {
            course.Creation_Date = DateTime.Now;
            db.Courses.Add(course);
            return db.SaveChanges();
        }

        public Course GetById(int id)
        {
            return db.Courses.Find(id);
        }

        public List<Course> ReadAll(string query = null, int? trainerId = null, int? categoryId = null)
        {
            return db.Courses.Where(e => (trainerId == null || e.Trainer_Id == trainerId) 
                                    && (categoryId == null || e.Category_Id == categoryId)
                                    && (query == null || e.Name.Contains(query))).ToList();
            
        }

        public int Update(Course updatedCourse)
        {
            db.Courses.Attach(updatedCourse);
            db.Entry(updatedCourse).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }
    }
}