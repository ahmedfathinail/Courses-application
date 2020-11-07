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
    public class CourseUnitsController : Controller
    {
        private readonly IMapper mapper;
        private readonly CourseUnitService courseUnitService;
        public CourseUnitsController()
        {
            mapper = AutoMapperConfig.Mapper;
            courseUnitService = new CourseUnitService();
        }
        // GET: Admin/CourseUnits?courseId
        public ActionResult Index(int? courseId)
        {
            if (courseId == null)
            {
                return HttpNotFound();
            }
            var units = courseUnitService.ReadCourseUnits(courseId.Value);
            var mappedUnits = mapper.Map<IEnumerable<Course_Uints>, IEnumerable<CourseUnitsModel>>(units);
            ViewBag.courseName = mappedUnits.FirstOrDefault()?.Name;
            return View(mappedUnits);
        }

        // GET: Admin/CourseUnits/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/CourseUnits/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/CourseUnits/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/CourseUnits/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/CourseUnits/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/CourseUnits/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/CourseUnits/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
