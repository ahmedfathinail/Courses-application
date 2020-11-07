using AutoMapper;
using courseApplication.Models;
using courseApplication.Services;
using CourseApplication;
using CourseApplication.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace courseApplication.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly CategoryService categoryService;
        private readonly IMapper mapper;
        public CategoryController()
        {
            categoryService = new CategoryService();
            mapper = AutoMapperConfig.Mapper;
        }
        // GET: Admin/Category
        public ActionResult Index()
        {
            var categories = categoryService.ReadAll();
            var categoriesList = mapper.Map<List<categoryModel>>(categories);

            return View(categoriesList);
        }

        public ActionResult Create()
        {
            var categoryModel = new categoryModel();
            IniMainCategories(null, ref categoryModel);
            return View(categoryModel);
        }
        [HttpPost]
        public ActionResult Create(categoryModel data)
        {
            var newCategory = mapper.Map<Category>(data);
            newCategory.Category2 = null;
            int creationResult = categoryService.Create(newCategory);

            if (creationResult == -2)
            {
                IniMainCategories(null, ref data);
                ViewBag.Message = " Category Name is Exists";
                return View(data);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            var currentCategory = categoryService.ReadById(id.Value);
            if (currentCategory == null)
            {
                return HttpNotFound($"This Category ({id}) Not Found");
            }
            var CategoryModel = new categoryModel
            {
                Id = currentCategory.ID,
                Name = currentCategory.Name,
                ParentId = currentCategory.Parnt_Id
            };
            IniMainCategories(currentCategory.ID, ref CategoryModel);

            return View(CategoryModel);
        }
        [HttpPost]
        public ActionResult Edit(categoryModel data)
        {

            var updateCategory = new Category
            {
                ID = data.Id,
                Name = data.Name,
                Parnt_Id = data.ParentId
            };
            var result = categoryService.Update(updateCategory);

            if (result == -2)
            {
                ViewBag.Message = " Category Name is Exists";
                IniMainCategories(data.Id, ref data);
                return View(data);
            }
            else if (result > 0)
            {
                ViewBag.Success = true;
                ViewBag.Message = $"Category ({data.Id}) Update Successfully";
            }
            else
            {
                ViewBag.Message = "an Error occurred!";
            }
            IniMainCategories(data.Id, ref data);
            return View(data);
        }

        public ActionResult Delete(int? id)
        {

            if (id != null)
            {
                var category = categoryService.ReadById(id.Value);
                var categoryInfo = new categoryModel
                {
                    Id = category.ID,
                    Name = category.Name,
                    ParentName = category.Category2?.Name
                };
                return View(categoryInfo);
            }
            return RedirectToAction("Index");
        }

        public ActionResult DeleteConfirmed(int? id)
        {
            if (id != null)
            {
                var deleted = categoryService.Delete(id.Value);
                if (deleted)
                {
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Delete", new { Id = id });
            }
            return HttpNotFound();
        }

        private void IniMainCategories(int? categoryToExclude, ref categoryModel categorymodel)
        {
            var categoryList = categoryService.ReadAll();

            if (categoryToExclude != null)
            {
                var currentCategory = categoryList.Where(c => c.ID == categoryToExclude).FirstOrDefault();
                categoryList.Remove(currentCategory);
            }
            categorymodel.MainCategories = new SelectList(categoryList, "ID", "Name");
        }
    }
}