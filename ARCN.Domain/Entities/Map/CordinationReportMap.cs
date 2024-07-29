using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARCN.Domain.Entities.Map
{
    public class CordinationReportMap:IEntityTypeConfiguration<CordinationReport>
    {
        public void Configure(EntityTypeBuilder<CordinationReport> builder)
        {
            builder.ToTable("tbl_CordinationReport");
            builder.HasKey(s=>s.CordinationReportId);

            #region Properties
            builder.Property(p => p.CordinationReportId)
            .HasColumnName("CordinationReportId")
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
               .HasColumnName("AuthorName")
                .HasMaxLength(450)
               .IsRequired(false);

            builder.Property(p => p.AuthorEmail)
               .HasColumnName("AuthorEmail")
                .HasMaxLength(450)
               .IsRequired(false);

          
            builder.Property(p => p.PublishDate)
                .HasColumnName("PublishDate")
                .IsRequired(false);


            #endregion
            #region Relationship
            builder.HasOne(d => d.ApplicationUser)
                   .WithMany(d => d.CordinationReports)
                   .HasForeignKey(d => d.UserProfileId)
                   .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
