using courseApplication.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseApplication.Models
{
    public class CourseModel
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name = "Creation Date")]
        public System.DateTime Creation_Date { get; set; }
        [Required]
        [Display(Name ="Category")]
        public int Category_Id { get; set; }
        [Display(Name = "Category Name")]
        public string Category_Name { get; set; }
        [Required]
        [Display(Name = "Trainer")]
        public Nullable<int> Trainer_Id { get; set; }
        [Display(Name = "Trainer Name")]
        public string TrainerName{ get; set; }

        public string Description { get; set; }

        private string _ImageId;

        public string Image_ID {
            set
            {
                _ImageId = string.IsNullOrEmpty(value) ? "Empty.png" : value;
            }
            get
            {
                return _ImageId;
            }

            
        }

        
        public HttpPostedFileBase ImageFile { get; set; }

        public SelectList Trainers { get; set; }
        public SelectList Categories { get; set; }
    }

    public class CourseListModel
    {
        public IEnumerable<CourseModel> Courses { get; set; }
        public string Query { get; set; }
        public int TrainerId { get; set; }
        public int CategoryId { get; set; }

        public SelectList Trainers { get; set; }
        public SelectList Categories { get; set; }
    }

    public class CourseUnitsModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Course_Id { get; set; }
        public string CourseName { get; set; }
    }
}