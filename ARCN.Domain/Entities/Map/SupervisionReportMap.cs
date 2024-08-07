using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARCN.Domain.Entities.Map
{
    public class SupervisionReportMap : IEntityTypeConfiguration<SupervisionReport>
    {
        public void Configure(EntityTypeBuilder<SupervisionReport> builder)
        {
            builder.ToTable("tbl_SupervisionReport");
            builder.HasKey(s=>s.SupervisionReportId);

            #region Properties
            builder.Property(p => p.SupervisionReportId)
            .HasColumnName("SupervisionReport")
            .IsRequired();

            builder.Property(p => p.UserProfileId)
                .HasColumnName("UserProfileId")
                .IsRequired();

            builder.Property(p => p.Title)
                .HasColumnName("Title")
                 .HasMaxLength(250)
                .IsRequired(false);

            builder.Property(p => p.BannerUrl)
                .HasColumnName("BannerUrl")
                 .HasMaxLength(450)
                .IsRequired(false);

            builder.Property(p => p.Description)
                .HasColumnName("Description")
                .IsRequired(false);

            builder.Property(p => p.PublisherName)
               .HasColumnName("PublisherName")
                .HasMaxLength(450)
               .IsRequired(false);

            builder.Property(p => p.AuthorEmail)
               .HasColumnName("AuthorEmail")
                .HasMaxLength(450)
               .IsRequired(false);



            builder.Property(p => p.PublishOn)
                .HasColumnName("PublishOn")
                .IsRequired(false);


            #endregion
            #region Relationship
            builder.HasOne(d => d.ApplicationUser)
                   .WithMany(d => d.SupervisionReports)
                   .HasForeignKey(d => d.UserProfileId)
                   .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
