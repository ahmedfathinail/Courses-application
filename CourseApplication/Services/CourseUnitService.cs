using CourseApplication.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseApplication.Services
{
    public interface ICourseUnitsService
    {
        int Create(Course_Uints courseUnit);
        IEnumerable<Course_Uints> ReadCourseUnits(int courseId);
        Course_Uints GetById(int id);
        int Update(Course_Uints updatedCourse);
    }
    public class CourseUnitService : ICourseUnitsService
    {
        private readonly courses_DBEntities db;
        public CourseUnitService()
        {
            db = new courses_DBEntities();
        }
        public int Create(Course_Uints courseUnit)
        {
            db.Course_Uints.Add(courseUnit);
            return db.SaveChanges();
        }

        public Course_Uints GetById(int id)
        {
            return db.Course_Uints.Find(id);
        }

        public IEnumerable<Course_Uints> ReadCourseUnits(int courseId)
        {
            return db.Course_Uints.Where(u => u.Course_Id == courseId);
        }

        public int Update(Course_Uints updatedCourse)
        {
            db.Course_Uints.Attach(updatedCourse);
            db.Entry(updatedCourse).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }
    }
}