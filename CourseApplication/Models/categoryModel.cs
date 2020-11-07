using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace courseApplication.Models
{
    public class categoryModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Category Name is Required")]
        [StringLength(200, MinimumLength = 4, ErrorMessage = "Category Name should between 4:200")]
        public string Name { get; set; }

        public int? ParentId { get; set; }

        public string ParentName { get; set; }

        public SelectList MainCategories { get; set; }
    }
}