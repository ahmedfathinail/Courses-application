using AutoMapper;
using CourseApplication.Data;
using CourseApplication.Models;
using CourseApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseApplication.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    public class TraineeController : Controller
    {
        private readonly TraineeCourseService traineeCourseService;
        private readonly IMapper mapper;
        public TraineeController()
        {
            traineeCourseService = new TraineeCourseService();
            mapper = AutoMapperConfig.Mapper;
        }
        // GET: Admin/Trainee
        public ActionResult Index(int? CId)
        {
            if(CId == null)
            {
                return RedirectToAction("Index", "Default", new { area = "Admin" });
            }
            var trainees = traineeCourseService.GetTrainees(CId.Value);

            var courseTrainees = mapper.Map<IEnumerable<Trainee_Courses>, IEnumerable<TraineeCourseModel>>(trainees);
            return View(courseTrainees);
        }
    }
}