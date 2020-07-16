namespace RestarauntReact.UI.Models.ShoppingCartVM
{
    #region << Using >>

    using System.Collections.Generic;

    #endregion

    public class ShoppingCartFoodsItemsVM
    {
        #region Properties

        public List<ShoppingCartFoodItemVM> ShoppingCartFoodItem { get; set; }

        public int AllCountFoodsItems { get; set; }

        public bool Admin { get; set; }

        #endregion

        #region Constructors

        public ShoppingCartFoodsItemsVM()
        {
            ShoppingCartFoodItem = new List<ShoppingCartFoodItemVM>();
        }

        #endregion
    }
}