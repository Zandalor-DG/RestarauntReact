namespace RestarauntReact.Core.Entities
{
    #region << Using >>

    using System;

    #endregion

    public class EntityBase
    {
        #region Properties

        public virtual int Id { get; set; }

        public virtual DateTime? CrDt { get; }

        #endregion

        #region Constructors

        public EntityBase()
        {
            CrDt = new DateTime?(DateTime.UtcNow);
        }

        #endregion
    }
}