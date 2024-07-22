
namespace ARCN.Domain.Entities.Map
{
    internal class ApplicationRoleClaimMap : IEntityTypeConfiguration<ApplicationRoleClaim>
    {
        public void Configure(EntityTypeBuilder<ApplicationRoleClaim> builder)
        {

            builder.HasKey(p => p.Id);
            #region Properties
            builder.Property(p => p.Description)
                .HasMaxLength(200)
                .IsRequired(false);
            builder.Property(p => p.Group)
                .HasMaxLength(200)
                .IsRequired(false);
            #endregion
        }
    }
}
