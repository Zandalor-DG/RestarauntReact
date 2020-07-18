namespace RestarauntReact.UI.Controllers
{
    #region << Using >>

    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using RestarauntReact.Core;
    using RestarauntReact.Core.Entities;
    using RestarauntReact.Core.Entities.SortOrder;
    using RestarauntReact.UI.Models.AllItems;
    using RestarauntReact.UI.Models.FoodCategoryVM;
    using RestarauntReact.UI.Models.FoodItemExtraVM;
    using RestarauntReact.UI.Models.FoodItemVM;

    #endregion

    [Authorize]
    public class HomeController : Controller
    {
        #region Properties

        private readonly ILogger<HomeController> _logger;

        #endregion

        #region Constructors

        public HomeController(ILogger<HomeController> logger)
        {
            this._logger = logger;
        }

        #endregion

        public IActionResult Index(string txt, bool descending, SortFoodCategory sortOrder)
        {
            var result = NHibernateRepositories.GetSingleOrDefault<User>(a => a.Email == "admin@gmail.com");
            if (result == null)
            {
                var createAdmin = new User
                                  {
                                          Name = "Дмитрий",
                                          Role = Role.Admin,
                                          Email = "admin@gmail.com",
                                          Password = "123456"
                                  };

                NHibernateRepositories.SaveOrUpdate(createAdmin);
            }

            var foodsCategoriesVM = ViewModel(txt, descending, sortOrder);

            return Json(foodsCategoriesVM);
        }

        [Route("indexAJAX")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult IndexAJAX(string txt, bool descending, SortFoodCategory sortOrder)
        {
            var foodsCategoriesVM = ViewModel(txt, descending, sortOrder);

            return Json(foodsCategoriesVM);
        }

        private FoodsCategoriesVM ViewModel(string txt, bool descending, SortFoodCategory sortOrder)
        {
            var foodCategory = NHibernateRepositories.GetEntities<FoodCategory>();
            var foodCategoryVM = new List<FoodCategoryVM>();
            if (!string.IsNullOrWhiteSpace(txt))
            {
                txt = txt.ToLower();
                foodCategoryVM = foodCategory.Where(compare =>
                                                            compare.Name.ToLower().Contains(txt))
                                             .Select(a => new FoodCategoryVM()
                                                          {
                                                                  Name = a.Name,
                                                                  Id = a.Id
                                                          }).ToList();
            }
            else
            {
                foodCategoryVM = foodCategory.Select(a => new FoodCategoryVM()
                                                          {
                                                                  Name = a.Name,
                                                                  Id = a.Id
                                                          }).ToList();
            }

            foodCategoryVM = sortOrder switch
            {
                    SortFoodCategory.Name when !descending => foodCategoryVM.OrderBy(s => s.Name).ToList(),
                    SortFoodCategory.Name when descending => foodCategoryVM.OrderByDescending(s => s.Name).ToList(),
                    _ => foodCategoryVM.OrderByDescending(s => s.Name).ToList(),
            };

            var foodsCategoriesVM = new FoodsCategoriesVM()
                                    {
                                            FoodsCategories = foodCategoryVM,
                                            Descending = descending,
                                            Sort = sortOrder,
                                            Admin = User.IsInRole(NHibernateRepositories.RoleAdmin)
                                    };

            return foodsCategoriesVM;
        }

        public IActionResult SearchAllEntities(string txt)
        {
            var foodItem = NHibernateRepositories.GetEntities<FoodItem>();
            var foodItemExtra = NHibernateRepositories.GetEntities<FoodItemExtra>();

            AllItemsVM allItems;
            if (!string.IsNullOrWhiteSpace(txt))
            {
                txt = txt.ToLower();
                allItems = new AllItemsVM()
                           {
                                   FoodsItems = foodItem.Where(compare => compare.Name.ToLower()
                                                                                 .Contains(txt))
                                                        .Select(a => new FoodItemVM()
                                                                     {
                                                                             Name = a.Name,
                                                                             Price = a.Price,
                                                                             Id = a.Id
                                                                     }).ToList(),
                                   FoodsItemsExtraVM = foodItemExtra.Where(compare => compare.Name.ToLower()
                                                                                             .Contains(txt))
                                                                    .Select(a => new FoodItemExtraVM()
                                                                                 {
                                                                                         Name = a.Name,
                                                                                         Price = a.Price,
                                                                                         Id = a.Id
                                                                                 }).ToList()
                           };
            }
            else
            {
                allItems = new AllItemsVM()
                           {
                                   FoodsItems = foodItem.Select(a => new FoodItemVM()
                                                                     {
                                                                             Name = a.Name,
                                                                             Price = a.Price,
                                                                             Id = a.Id
                                                                     }).ToList(),
                                   FoodsItemsExtraVM = foodItemExtra.Select(a => new FoodItemExtraVM()
                                                                                 {
                                                                                         Name = a.Name,
                                                                                         Price = a.Price,
                                                                                         Id = a.Id
                                                                                 }).ToList()
                           };
            }

            return Json(allItems);
        }
    }
}