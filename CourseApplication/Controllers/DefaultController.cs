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
    public class DefaultController : Controller
    {
        private readonly IMapper mapper;
        private readonly CourseService courseService;

        public DefaultController()
        {
            mapper = AutoMapperConfig.Mapper;
            courseService = new CourseService();
        }
        // GET: Default
        public ActionResult Index()
        {
            var courses = courseService.ReadAll();
            
            return View(mapper.Map<List<Course>, List<CourseModel>>(courses));
        }
    }
}