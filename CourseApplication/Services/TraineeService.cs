using CourseApplication.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseApplication.Services
{
    public interface ITraineeService
    {
        Trainee Create(Trainee trainee);
    }
    public class TraineeService : ITraineeService
    {
        private readonly courses_DBEntities db;
        public TraineeService()
        {
            db = new courses_DBEntities();
        }

        public Trainee Create(Trainee trainee)
        {
            db.Trainees.Add(trainee);
            int saveResult = db.SaveChanges();
            if (saveResult >0)
            {
                return trainee;
            }
            return null;
        }

      
    }
}