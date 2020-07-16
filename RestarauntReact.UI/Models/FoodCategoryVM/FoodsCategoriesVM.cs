namespace RestarauntReact.UI.Models.FoodCategoryVM
{
    #region << Using >>

    using System.Collections.Generic;
    using RestarauntReact.Core.Entities.SortOrder;

    #endregion

    public class FoodsCategoriesVM
    {
        #region Properties

        public List<FoodCategoryVM> FoodsCategories { get; set; }

        public bool Descending { get; set; }

        public SortFoodCategory Sort { get; set; }

        public bool Admin { get; set; }

        #endregion

        #region Constructors

        public FoodsCategoriesVM()
        {
            FoodsCategories = new List<FoodCategoryVM>();
        }

        #endregion
    }
}