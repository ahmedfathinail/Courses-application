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
    [Authorize(Roles = "Admin")]
    public class TrainerController : Controller
    {
        private readonly IMapper mapper;
        private readonly TrainerService trianerService;

        public TrainerController()
        {
            mapper = AutoMapperConfig.Mapper;
            trianerService = new TrainerService();
        }
        // GET: Admin/Trainer
        public ActionResult Index()
        {
            var trainers = trianerService.ReadAll();
            var trainerList = mapper.Map<List<TrainerModel>>(trainers);
            return View(trainerList);
        }

        // GET: Admin/Trainer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Trainer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Trainer/Create
        [HttpPost]
        public ActionResult Create(TrainerModel trainerData)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   var trainerDTO = mapper.Map<Trainer>(trainerData);
                   var result = trianerService.Create(trainerDTO);
                   if(result>=1)
                   {
                       return RedirectToAction("Index");
                   }
                   else if(result == -2)
                   {
                       ViewBag.Message = "already exists Trainer With Email";
                   }
                   else
                   {
                       ViewBag.Message = "an Error accourd!";
                   }
                }
                return View(trainerData);
            }
            catch(Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(trainerData);
            }
        
    }

        // GET: Admin/Trainer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Trainer/Edit/5
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

        // GET: Admin/Trainer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Trainer/Delete/5
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
