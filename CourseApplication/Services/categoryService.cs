﻿using System.Collections.Generic;
using System.Linq;
using System;
using CourseApplication.Data;

namespace courseApplication.Services
{
    public interface ICategoryService
    {
        List<Category> ReadAll();
        int Create(Category newCategory);
        Category ReadById(int id);
        int Update(Category updatedCategory);
        bool Delete(int id);

    }
    public class CategoryService : ICategoryService
    {
        private readonly courses_DBEntities db;
        public CategoryService()
        {
            db = new courses_DBEntities();
        }

        public int Create(Category newCategory)
        {
            var categoryName = newCategory.Name.ToLower();
            var categoryNameExists = db.Categories.Where(c => c.Name.ToLower() == categoryName).Any();

            if (categoryNameExists)
            {
                return -2;
            }
            db.Categories.Add(newCategory);
            return db.SaveChanges();

        }

        public bool Delete(int id)
        {
            var category = ReadById(id);
            if (category != null)
            {
                db.Categories.Remove(category);
                return db.SaveChanges() > 0 ? true : false;
            }
            return false;
        }

        public List<Category> ReadAll()
        {
            return db.Categories.ToList();
        }

        public Category ReadById(int id)
        {
            return db.Categories.Find(id);
        }

        public int Update(Category updatedCategory)
        {
            var categoryName = updatedCategory.Name.ToLower();
            var categoryNameExists = db.Categories.Where(c => c.Name.ToLower() != categoryName);

            if (categoryNameExists.Where(c => c.Name.ToLower() == categoryName).Any())
            {
                return -2;
            }
            db.Categories.Attach(updatedCategory);
            db.Entry(updatedCategory).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }
    }
}