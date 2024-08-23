using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARCN.Domain.Entities.Map
{
    public class ARCNProgramMap : IEntityTypeConfiguration<ARCNProgram>
    {
        public void Configure(EntityTypeBuilder<ARCNProgram> builder)
        {
            builder.ToTable("tbl_ARCNProgram");
            builder.HasKey(s=>s.ARCNProgramId);

            #region Properties
            builder.Property(p => p.ARCNProgramId)
            .HasColumnName("ARCNProgramId")
            .IsRequired();

            builder.Property(p => p.UserProfileId)
                .HasColumnName("UserProfileId")
                .IsRequired();

            builder.Property(p => p.Subject)
                .HasColumnName("Subject")
                 .HasMaxLength(250)
                .IsRequired(false);

            builder.Property(p => p.BannerUrl)
                .HasColumnName("BannerUrl")
                 .HasMaxLength(450)
                .IsRequired(false);

            builder.Property(p => p.Description)
                .HasColumnName("Description")
                .IsRequired(false);

            builder.Property(p => p.Author)
               .HasColumnName("Author")
                .HasMaxLength(450)
               .IsRequired(false);

            builder.Property(p => p.Venue)
                .HasColumnName("Venue")
                .IsRequired(false);

            builder.Property(p => p.EventStartDate)
                .HasColumnName("EventStartDate")
                .IsRequired(false);

            builder.Property(p => p.EventEndDate)
               .HasColumnName("EventEndDate")
               .IsRequired(false);


            #endregion
            #region Relationship
            builder.HasOne(d => d.ApplicationUser)
                   .WithMany(d => d.ARCNPrograms)
                   .HasForeignKey(d => d.UserProfileId)
                   .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
