namespace RestarauntReact.UI.Models.UserVM
{
    #region << Using >>

    using RestarauntReact.Core.Entities;

    #endregion

    public class UserVM
    {
        #region Properties

        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public Role Role { get; set; }

        #endregion
    }
}