namespace RestarauntReact.UI.Models.AllItems
{
    #region << Using >>

    using System.Collections.Generic;
    using RestarauntReact.UI.Models.FoodItemExtraVM;
    using RestarauntReact.UI.Models.FoodItemVM;

    #endregion

    public class AllItemsVM
    {
        #region Properties

        public List<FoodItemVM> FoodsItems { get; set; }

        public List<FoodItemExtraVM> FoodsItemsExtraVM { get; set; }

        #endregion

        #region Constructors

        public AllItemsVM()
        {
            FoodsItems = new List<FoodItemVM>();
            FoodsItemsExtraVM = new List<FoodItemExtraVM>();
        }

        #endregion
    }
}