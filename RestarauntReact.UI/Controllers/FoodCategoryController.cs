using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RestarauntReact.UI.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using RestarauntReact.Core;
    using RestarauntReact.Core.Entities;
    using RestarauntReact.UI.Models.FoodCategoryVM;

    [Authorize]
    public class FoodCategoryController : Controller
    {
        [HttpGet]
        [Authorize(Roles = NHibernateRepositories.RoleAdmin)]
        public IActionResult CreateOrUpdateFoodCategory(int? id)
        {
            var foodCategory = NHibernateRepositories.GetSingleOrDefault<FoodCategory>(a => a.Id == id);
            FoodCategoryVM categoryVM;
            categoryVM = foodCategory == null ?
                                 new FoodCategoryVM() :
                                 new FoodCategoryVM()
                                 {
                                         Name = foodCategory.Name, Id = foodCategory.Id
                                 };

            return Json(categoryVM);
        }

        [HttpPost]
        [Authorize(Roles = NHibernateRepositories.RoleAdmin)]
        public IActionResult CreateOrUpdateFoodCategory(FoodCategoryVM foodCategoryVm)
        {
            var foodCategory = NHibernateRepositories.GetSingleOrDefault<FoodCategory>(a => a.Id == foodCategoryVm.Id) ??
                               new FoodCategory()
                               {
                                       Name = foodCategoryVm.Name,
                                       Id = foodCategoryVm.Id
                               };

            foodCategory.Name = foodCategoryVm.Name;
            NHibernateRepositories.SaveOrUpdate<FoodCategory>(foodCategory);

            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = NHibernateRepositories.RoleAdmin)]
        public IActionResult ConfirmDeleteFoodCategory(int id)
        {
            var delFoodCategory = NHibernateRepositories.GetSingleOrDefault<FoodCategory>(a => a.Id == id);
            var foodCategoryVM = new FoodCategoryVM()
                                 {
                                         Name = delFoodCategory.Name,
                                         Id = delFoodCategory.Id
                                 };

            return Json(foodCategoryVM);
        }

        [Authorize(Roles = NHibernateRepositories.RoleAdmin)]
        public IActionResult DeleteFoodCategory(int id)
        {
            var delFoodCategory = NHibernateRepositories.GetSingleOrDefault<FoodCategory>(a => a.Id == id);
            var foodItem = NHibernateRepositories.GetEntities<FoodItem>(a => a.FoodCategory.Id == id);
            var foodItemExtra = NHibernateRepositories.GetEntities<FoodItemExtra>(a => a.FoodCategory.Id == id);

            if (foodItem.Count != 0 && foodItemExtra.Count != 0)
            {
                foreach (var delFoodItem in foodItem)
                    NHibernateRepositories.DeleteEntities<FoodItem>(delFoodItem);

                foreach (var delFoodItemExtra in foodItemExtra)
                    NHibernateRepositories.DeleteEntities<FoodItemExtra>(delFoodItemExtra);
            }

            NHibernateRepositories.DeleteEntities<FoodCategory>(delFoodCategory);

            return RedirectToAction("Index", "Home");
        }
    }
}
