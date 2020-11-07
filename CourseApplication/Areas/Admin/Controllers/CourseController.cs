using AutoMapper;
using courseApplication.Models;
using courseApplication.Services;
using CourseApplication.Data;
using CourseApplication.Models;
using CourseApplication.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseApplication.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    public class CourseController : Controller
    {
        private readonly IMapper mapper;
        private readonly CourseService courseService;
        private readonly CategoryService categoryService;
        private readonly TrainerService trainerService;
        public CourseController()
        {
            mapper = AutoMapperConfig.Mapper;
            courseService = new CourseService();
            categoryService = new CategoryService();
            trainerService = new TrainerService();
        }
        // GET: Admin/Course
        public ActionResult Index( string query = null,int? categoryId = null,int? trainerId = null)
        {
            var coursesListData = new CourseListModel();
            init(ref coursesListData);
            var coursesList = courseService.ReadAll(query,trainerId,categoryId);
            var mappedCuorsesList = mapper.Map<List<CourseModel>>(coursesList);
            coursesListData.Courses = mappedCuorsesList;

            return View(coursesListData);
        }

        public ActionResult Create()
        {
            var courseModel = new CourseModel();
            init(ref courseModel);
            return View(courseModel);
        }
        [HttpPost]
        public ActionResult Create(CourseModel courseData)
        {
            init(ref courseData);
          
            try
            {
                if(ModelState.IsValid)
                {
                    courseData.Image_ID = SaveImageFile(courseData.ImageFile);

                    var courseDTO = mapper.Map<Course>(courseData);
                    courseDTO.Category = null;
                    courseDTO.Trainer = null;
                  
                    var result = courseService.Create(courseDTO);
                    if (result >= 1)
                    {
                        return RedirectToAction("Index");
                    }
                    ViewBag.Message = "An error accourd!";
                }
                return View(courseData);

            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(courseData);
               
            }
            
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var currentCourseData = courseService.GetById(id.Value);
            var courseModel = mapper.Map<CourseModel>(currentCourseData);
            init(ref courseModel);
            return View(courseModel);
        }
        [HttpPost]
        public ActionResult Edit(CourseModel courseData)
        {
            init(ref courseData);
            
            try
            {
                if (ModelState.IsValid)
                {

                    courseData.Image_ID = SaveImageFile(courseData.ImageFile,courseData.Image_ID);
                    var courseDTO = mapper.Map<Course>(courseData);
                    courseDTO.Category = null;
                    courseDTO.Trainer = null;

                    var result = courseService.Update(courseDTO);
                    if (result >= 1)
                    {
                        return RedirectToAction("Index");
                    }
                    ViewBag.Message = "An error accourd!";
                }
                return View(courseData);

            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(courseData);

            }

        }

        private void init(ref CourseModel courseModel)
        {
            var mappedCategoriesList = GetCategories();
            courseModel.Categories = new SelectList(mappedCategoriesList, "Id", "Name");

            var mappedTrainersList = GetTrainers();
            courseModel.Trainers = new SelectList(mappedTrainersList, "Id", "Name");
        }

        private void init(ref CourseListModel coursesList)
        {

            var mappedCategoriesList = GetCategories();
            coursesList.Categories = new SelectList(mappedCategoriesList, "Id", "Name");

            var mappedTrainersList = GetTrainers();
            coursesList.Trainers = new SelectList(mappedTrainersList, "Id", "Name");
        }

        private IEnumerable<categoryModel> GetCategories()
        {
            var categories = categoryService.ReadAll(); 
            return mapper.Map<IEnumerable<categoryModel>>(categories);
        }

        private IEnumerable<TrainerModel> GetTrainers()
        {
            var trainers = trainerService.ReadAll();
            return mapper.Map<IEnumerable<TrainerModel>>(trainers);

        }

        private string SaveImageFile(HttpPostedFileBase imageFile,string currentImageId ="")
        {
            if (imageFile != null)
            {
                var fileExtension = Path.GetExtension(imageFile.FileName);
                var imageGuid = Guid.NewGuid().ToString();
                string imageId = imageGuid + fileExtension;

                // Save File
                var filePath = Server.MapPath($"~/Uploads/Courses/{imageId}");
                imageFile.SaveAs(filePath);

                // Delet Old File - Update Action
                if (!string.IsNullOrEmpty(currentImageId))
                {
                    string oldFilePath = Server.MapPath($"~/Uploads/Courses/{currentImageId}");
                    System.IO.File.Delete(oldFilePath);
                }
                return imageId;
            }
            return currentImageId;
        }
    }
}