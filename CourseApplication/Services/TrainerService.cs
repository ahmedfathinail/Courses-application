using CourseApplication.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseApplication.Services
{
    public interface ITrainerService
    {
        int Create(Trainer trainer);
        Trainer FindByEmail(string email);
        Trainer ReadById(int id);
        IEnumerable<Trainer> ReadAll();
    }
    public class TrainerService : ITrainerService
    {
        private readonly courses_DBEntities db;
        public TrainerService()
        {
            db = new courses_DBEntities();
        }
        public int Create(Trainer trainer)
        {
            var existsTrainer = FindByEmail(trainer.E_mail);
            if (existsTrainer != null)
            {
                return -2;
            }
            trainer.Ceartion_date = DateTime.Now;
            db.Trainers.Add(trainer);
            return db.SaveChanges();
        }

        public Trainer FindByEmail(string email)
        {
            return db.Trainers.Where(e => e.E_mail == email).FirstOrDefault(); 
        }

        public IEnumerable<Trainer> ReadAll()
        {
            return db.Trainers.ToList();
        }

        public Trainer ReadById(int id)
        {
            throw new NotImplementedException();
        }
    }
}