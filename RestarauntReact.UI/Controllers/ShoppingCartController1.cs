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
    using RestarauntReact.UI.Models.ShoppingCartVM;

    [Authorize]
    public class ShoppingCartController : Controller
    {
        public IActionResult Index()
        {
            var shoppingCartItemVM = ViewModel();
            return Json(shoppingCartItemVM);
        }

        public IActionResult IndexJson()
        {
            var shoppingCartItemVM = ViewModel();
            return Json(shoppingCartItemVM);
        }

        public ShoppingsCartFoodsItemsVM ViewModel()
        {
            var id = int.Parse(User.Identity.Name);
            
            ShoppingsCartFoodsItemsVM shoppingCartItemVM;
            using (var session = NHibernateRepositories.OpenSession())
            {
                var items = session.Query<ShoppingCartFoodItem>()
                                   .Where(a => a.User.Id == id)
                                   .Select(a => new ShoppingCartFoodItemVM()
                                                {
                                                        Id = a.Id,
                                                        Count = a.Count,
                                                        UserId = a.User.Id,
                                                        UserName = a.User.Name,
                                                        Price = a.FoodItem.Price,
                                                        FoodItemId = a.FoodItem.Id,
                                                        FoodName = a.FoodItem.Name
                                                }).ToList();


                shoppingCartItemVM = new ShoppingsCartFoodsItemsVM()
                                     {
                                             ShoppingCartFoodItem = items,
                                             Admin = User.IsInRole(NHibernateRepositories.RoleAdmin),
                                             AllCountFoodsItems = items.Sum(a => a.Count)
                                     };
            }

            return shoppingCartItemVM;
        }

        [HttpGet]
        public IActionResult AddFoodItemToShoppingCart(int foodItemId)
        {
            var id = int.Parse(User.Identity.Name);
            var foodItem = NHibernateRepositories.GetSingleOrDefault<FoodItem>(a => a.Id == foodItemId);
            var user = NHibernateRepositories.GetSingleOrDefault<User>(a => a.Id == id);

            var shoppingCartFoodItem = NHibernateRepositories.GetSingleOrDefault<ShoppingCartFoodItem>(a => a.FoodItem.Id == foodItem.Id && a.User.Id == user.Id) ??
                                       new ShoppingCartFoodItem
                                       {
                                               FoodItem = foodItem,
                                               User = user
                                       };

            shoppingCartFoodItem.Count += 1;
            NHibernateRepositories.SaveOrUpdate(shoppingCartFoodItem);

            return Ok();
        }

        public IActionResult RemoveItemToShoppingCart(int foodItemId)
        {
            var id = int.Parse(User.Identity.Name);
            var foodItem = NHibernateRepositories.GetSingleOrDefault<FoodItem>(a => a.Id == foodItemId);
            var user = NHibernateRepositories.GetSingleOrDefault<User>(a => a.Id == id);
            if (foodItem == null || user == null)
                return BadRequest();

            var shoppingCartFoodItem = NHibernateRepositories.GetSingleOrDefault<ShoppingCartFoodItem>(a => a.FoodItem.Id == foodItem.Id && a.User.Id == user.Id);
            if (shoppingCartFoodItem.Count == 1)
                return DeleteItemToShoppingCart(shoppingCartFoodItem.Id);

            shoppingCartFoodItem.Count -= 1;

            NHibernateRepositories.SaveOrUpdate(shoppingCartFoodItem);

            return Ok();
        }

        public IActionResult DeleteItemToShoppingCart(int shoppingCartFoodItemId)
        {
            var shoppingCartFoodItem = NHibernateRepositories.GetSingleOrDefault<ShoppingCartFoodItem>(a => a.Id == shoppingCartFoodItemId);
            NHibernateRepositories.DeleteEntities(shoppingCartFoodItem);
            return Ok();
        }
    }
}
