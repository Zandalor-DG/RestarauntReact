namespace RestarauntReact.Core.Entities
{
    #region << Using >>

    using FluentNHibernate.Mapping;

    #endregion

    public class EntityMapBase<T> : ClassMap<T> where T : EntityBase
    {
        #region Constructors

        public EntityMapBase()
        {
            Id(a => a.Id);
            Map(a => a.CrDt).Nullable();
        }

        #endregion
    }
}