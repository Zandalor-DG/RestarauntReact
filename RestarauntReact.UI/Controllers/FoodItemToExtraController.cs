namespace RestarauntReact.UI.Controllers
{
    #region << Using >>

    using System.Linq;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using RestarauntReact.Core;
    using RestarauntReact.Core.Entities;
    using RestarauntReact.UI.Models.FoodItemToExtraVM;

    #endregion

    [Authorize]
    public class FoodItemToExtraController : Controller
    {
        public ActionResult Index()
        {
            var foodItemToExtra = NHibernateRepositories.GetEntities<FoodItemToExtra>();
            var foodItemToExtraVM = foodItemToExtra.Select(a => new FoodItemToExtraVM()
                                                                {
                                                                        Id = a.Id
                                                                }).ToList();

            return Json(foodItemToExtraVM);
        }

        [HttpGet]
        [Authorize(Roles = NHibernateRepositories.RoleAdmin)]
        public ActionResult CreateOrUpdateFoodItemToExtra(int id)
        {
            var foodItemToExtra = NHibernateRepositories.GetSingleOrDefault<FoodItemToExtra>(a => a.Id == id);

            var foodItems = NHibernateRepositories.GetEntities<FoodItem>()
                                                  .Select(a => new SelectListItem()
                                                               {
                                                                       Value = a.Id.ToString(),
                                                                       Text = a.Name
                                                               }).ToList();

            var foodItemExtras = NHibernateRepositories.GetEntities<FoodItemExtra>()
                                                       .Select(a => new SelectListItem()
                                                                    {
                                                                            Value = a.Id.ToString(),
                                                                            Text = a.Name
                                                                    }).ToList();

            var foodItemToExtraVM = new CreateOrUpdateFoodItemToExtraVM()
                                    {
                                            Id = foodItemToExtra?.Id,
                                            FoodItemId = foodItemToExtra?.FoodItem.Id,
                                            FoodItemExtraId = foodItemToExtra?.FoodItemExtra.Id,
                                            FoodItems = foodItems,
                                            FoodItemExtras = foodItemExtras
                                    };

            return Json(foodItemToExtraVM);
        }

        [HttpPost]
        [Authorize(Roles = NHibernateRepositories.RoleAdmin)]
        public ActionResult CreateOrUpdateFoodItemToExtra(FoodItemToExtraVM foodItemToExtraVm)
        {
            var foodItemToExtra = NHibernateRepositories.GetSingleOrDefault<FoodItemToExtra>(a => a.Id == foodItemToExtraVm.Id) ?? new FoodItemToExtra();

            var foodItem = NHibernateRepositories.GetSingleOrDefault<FoodItem>(a => a.Id == foodItemToExtraVm.FoodItemId);
            var foodItemExtra = NHibernateRepositories.GetSingleOrDefault<FoodItemExtra>(a => a.Id == foodItemToExtraVm.FoodItemExtraId);

            foodItemToExtra.FoodItem = foodItem;
            foodItemToExtra.FoodItemExtra = foodItemExtra;

            NHibernateRepositories.SaveOrUpdate(foodItemToExtra);

            return RedirectToAction("Index", "FoodItemToExtra");
        }

        public ActionResult ConfirmDeleteFoodItemToExtra(int id)
        {
            var delFoodItemToExtra = NHibernateRepositories.GetSingleOrDefault<FoodItemToExtra>(a => a.Id == id);
            var foodItemToExtraVm = new FoodItemToExtraVM()
                                    {
                                            Id = delFoodItemToExtra.Id
                                    };

            return Json(foodItemToExtraVm);
        }

        [HttpPost]
        public ActionResult DeleteFoodItemToExtra(int id)
        {
            var delFoodItemToExtra = NHibernateRepositories.GetSingleOrDefault<FoodItemToExtra>(a => a.Id == id);

            NHibernateRepositories.DeleteEntities<FoodItemToExtra>(delFoodItemToExtra);

            return RedirectToAction("Index", "FoodItemToExtra");
        }
    }
}