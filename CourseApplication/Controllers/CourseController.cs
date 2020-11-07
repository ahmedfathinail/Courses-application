using AutoMapper;
using CourseApplication.Data;
using CourseApplication.Models;
using CourseApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseApplication.Controllers
{
    public class CourseController : Controller
    {
        private readonly IMapper mapper;
        private readonly CourseService courseService;

        public CourseController()
        {
            mapper = AutoMapperConfig.Mapper;
            courseService = new CourseService();
        }
        // GET: Course
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Info(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return HttpNotFound("Course Not Found");
            }
            var courseInfo = courseService.GetById(Id.Value);
            if (courseInfo == null)
            {
                return HttpNotFound("Course Not Found");
            }
            var mappedcourseInfo = mapper.Map<Course, CourseModel>(courseInfo);
            return View(mappedcourseInfo);
        }

        public ActionResult Subscribe(int id)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account", new { returnUrl = $"/Course/Subscribe/{id}" });
            }
            return View();
        }
    }
}