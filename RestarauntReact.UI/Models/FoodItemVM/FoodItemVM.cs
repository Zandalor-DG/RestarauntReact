namespace RestarauntReact.UI.Models.FoodItemVM
{
    public class FoodItemVM
    {
        #region Properties

        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int FoodCategoryId { get; set; }

        #endregion
    }
}