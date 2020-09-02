namespace RestarauntReact.UI.Models.FoodItemExtraVM
{
    #region << Using >>

    using System.Collections.Generic;
    using RestarauntReact.Core.Entities.SortOrder;

    #endregion

    public class FoodsItemsExtraVM
    {
        #region Properties

        public List<FoodItemExtraVM> FoodsItemsExtra { get; set; }

        public int FoodCategoryId { get; set; }

        public bool Descending { get; set; }

        public SortFoodItemExtra Sort { get; set; }

        public bool Admin { get; set; }

        #endregion

        #region Constructors

        public FoodsItemsExtraVM()
        {
            FoodsItemsExtra = new List<FoodItemExtraVM>();
        }

        #endregion
    }
}