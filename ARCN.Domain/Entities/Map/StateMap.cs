namespace ARCN.Domain.Entities.Map
{
    public class StateMap : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {
            builder.ToTable("tbl_State");
            builder.HasKey(s => s.StateId);

            #region Properties

            #endregion

            #region Relationship

            #endregion
        }
    }
}
