using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARCN.Domain.Entities.Map
{
    public class JournalsMap : IEntityTypeConfiguration<Journals>
    {
        public void Configure(EntityTypeBuilder<Journals> builder)
        {
            builder.ToTable("tbl_Journals");
            builder.HasKey(s=>s.JournalsId);

            #region Properties
            builder.Property(p => p.JournalsId)
            .HasColumnName("JournalsId")
            .IsRequired();

            builder.Property(p => p.UserProfileId)
                .HasColumnName("UserProfileId")
                .IsRequired();

            builder.Property(p => p.Title)
                .HasColumnName("Title")
                 .HasMaxLength(250)
                .IsRequired(false);

            builder.Property(p => p.Category)
                .HasColumnName("Category")
                 .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(p => p.BannerUrl)
                .HasColumnName("BannerUrl")
                 .HasMaxLength(450)
                .IsRequired(false);

            builder.Property(p => p.Description)
                .HasColumnName("Description")
                .IsRequired(false);

            builder.Property(p => p.AuthorName)
               .HasColumnName("AuthorName")
                .HasMaxLength(450)
               .IsRequired(false);

            builder.Property(p => p.AuthorEmail)
               .HasColumnName("AuthorEmail")
                .HasMaxLength(450)
               .IsRequired(false);

            builder.Property(p => p.AuthorPhoneNumber)
               .HasColumnName("AuthorPhoneNumber")
                .HasMaxLength(35)
               .IsRequired(false);

            builder.Property(p => p.Visibility)
               .HasColumnName("Visibility")
                .HasMaxLength(450)
               .IsRequired(false);

            builder.Property(p => p.UseBanner)
            .HasColumnName("UseBanner")
            .IsRequired(false);

            builder.Property(p => p.PublishDate)
                .HasColumnName("PublishDate")
                .IsRequired(false);


            #endregion
            #region Relationship
            builder.HasOne(d => d.ApplicationUser)
                   .WithMany(d => d.Journals)
                   .HasForeignKey(d => d.UserProfileId)
                   .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
