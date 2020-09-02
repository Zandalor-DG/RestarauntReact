namespace RestarauntReact.UI.Models.ShoppingCartVM
{
    #region << Using >>

    using RestarauntReact.Core.Entities;
    using RestarauntReact.UI.Models.UserVM;

    #endregion

    public class ShoppingCartFoodItemExtraVM
    {
        #region Properties

        public int Id { get; set; }

        public UserVM User { get; set; }

        public FoodItemExtra FoodItemExtra { get; set; }

        public int Count { get; set; }

        #endregion
    }
}