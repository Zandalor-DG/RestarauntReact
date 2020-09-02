namespace RestarauntReact.UI.Controllers
{
    #region << Using >>

    using System.Linq;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RestarauntReact.Core;
    using RestarauntReact.Core.Entities;
    using RestarauntReact.Core.Entities.SortOrder;
    using RestarauntReact.UI.Models.FoodItemVM;

    #endregion

    [Authorize]
    public class FoodItemController : Controller
    {
        public IActionResult Index(int id, bool descending, SortFoodItem sortOrder)
        {
            var foodsItemsVM = ViewModel(id, descending, sortOrder);

            return Json(foodsItemsVM);
        }

        public IActionResult IndexAJAX(int id, bool descending, SortFoodItem sortOrder)
        {
            var foodsItemsVM = ViewModel(id, descending, sortOrder);

            return Json(foodsItemsVM);
        }

        public FoodsItemsVM ViewModel(int id, bool descending, SortFoodItem sortOrder)
        {
            var foodItem = NHibernateRepositories.GetEntities<FoodItem>(a => a.FoodCategory.Id == id);

            var foodItemVM = foodItem.Select(a => new FoodItemVM()
                                                  {
                                                          Id = a.Id,
                                                          Name = a.Name,
                                                          Price = a.Price,
                                                          FoodCategoryId = id
                                                  }).ToList();

            foodItemVM = sortOrder switch
            {
                    SortFoodItem.Name when !descending => foodItemVM.OrderBy(s => s.Name).ToList(),
                    SortFoodItem.Name when descending => foodItemVM.OrderByDescending(s => s.Name).ToList(),
                    SortFoodItem.Price when !descending => foodItemVM.OrderBy(s => s.Price).ToList(),
                    SortFoodItem.Price when descending => foodItemVM.OrderByDescending(s => s.Price).ToList(),
                    _ => foodItemVM.OrderByDescending(s => s.Name).ToList(),
            };

            var foodsItemsVM = new FoodsItemsVM()
                               {
                                       FoodsItems = foodItemVM,
                                       FoodCategoryId = id,
                                       Descending = descending,
                                       Sort = sortOrder,
                                       Admin = User.IsInRole(NHibernateRepositories.RoleAdmin)
                               };

            return foodsItemsVM;
        }

        [HttpGet]
        [Authorize(Roles = NHibernateRepositories.RoleAdmin)]
        public IActionResult CreateOrUpdateFoodItem(int? id, int foodCategoryId)
        {
            var foodItem = NHibernateRepositories.GetSingleOrDefault<FoodItem>(a => a.Id == id);
            FoodItemVM foodItemVm;
            foodItemVm = foodItem == null ?
                                 new FoodItemVM() :
                                 new FoodItemVM()
                                 {
                                         Name = foodItem.Name,
                                         Id = foodItem.Id,
                                         Price = foodItem.Price,
                                         FoodCategoryId = foodCategoryId
                                 };

            return Json(foodItemVm);
        }

        [HttpPost]
        [Authorize(Roles = NHibernateRepositories.RoleAdmin)]
        public IActionResult CreateOrUpdateFoodItem(FoodItemVM foodItemVm)
        {
            var foodCategory = NHibernateRepositories.GetSingleOrDefault<FoodCategory>(a => a.Id == foodItemVm.FoodCategoryId);
            var foodItem = NHibernateRepositories.GetSingleOrDefault<FoodItem>(a => a.Id == foodItemVm.Id) ??
                           new FoodItem()
                           {
                                   Name = foodItemVm.Name,
                                   Id = foodItemVm.Id,
                                   Price = foodItemVm.Price,
                                   FoodCategory = foodCategory
                           };

            foodItem.Name = foodItemVm.Name;
            foodItem.Price = foodItemVm.Price;

            NHibernateRepositories.SaveOrUpdate(foodItem);

            return RedirectToAction("Index", "FoodItem", new
                                                         {
                                                                 id = foodItemVm.FoodCategoryId
                                                         });
        }

        [HttpGet]
        [Authorize(Roles = NHibernateRepositories.RoleAdmin)]
        public IActionResult ConfirmDeleteFoodItem(int id)
        {
            var delFoodItem = NHibernateRepositories.GetSingleOrDefault<FoodItem>(a => a.Id == id);
            var foodItemVM = new FoodItemVM()
                             {
                                     Name = delFoodItem.Name,
                                     Id = delFoodItem.Id,
                                     Price = delFoodItem.Price,
                                     FoodCategoryId = delFoodItem.FoodCategory.Id
                             };

            return Json(foodItemVM);
        }

        [HttpPost]
        [Authorize(Roles = NHibernateRepositories.RoleAdmin)]
        public IActionResult DeleteFoodItem(int id, int foodCategoryId)
        {
            var delFoodItem = NHibernateRepositories.GetSingleOrDefault<FoodItem>(a => a.Id == id);

            NHibernateRepositories.DeleteEntities<FoodItem>(delFoodItem);

            return RedirectToAction("Index", "FoodItem", new
                                                         {
                                                                 id = foodCategoryId
                                                         });
        }
    }
}