﻿namespace FastFood.Core.Controllers
{
    using System.Linq;
    using System.Collections.Generic;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNetCore.Mvc;
    
    using Data;
    using FastFood.Models;
    using ViewModels.Categories;

    public class CategoriesController : Controller
    {
        private readonly FastFoodContext context;
        private readonly IMapper mapper;

        public CategoriesController(FastFoodContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(CreateCategoryInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.RedirectToAction("Error", "Home");
            }

            Category category = this.mapper.Map<Category>(model);

            this.context.Categories.Add(category);
            this.context.SaveChanges();

            return this.RedirectToAction("All");
        }

        public IActionResult All()
        {
            List<CategoryAllViewModel> categories = this.context
                .Categories
                .ProjectTo<CategoryAllViewModel>(this.mapper.ConfigurationProvider)
                .ToList();

            return this.View(categories);
        }
    }
}
