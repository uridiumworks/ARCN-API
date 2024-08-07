using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARCN.Domain.Entities.Map
{
    public class EntrepreneurshipMap : IEntityTypeConfiguration<Entrepreneurship>
    {
        public void Configure(EntityTypeBuilder<Entrepreneurship> builder)
        {
            builder.ToTable("tbl_Entrepreneurship");
            builder.HasKey(s=>s.EntrepreneurshipId);

            #region Properties
            builder.Property(p => p.EntrepreneurshipId)
            .HasColumnName("EntrepreneurshipId")
            .IsRequired();

            builder.Property(p => p.UserProfileId)
                .HasColumnName("UserProfileId")
                .IsRequired();

            builder.Property(p => p.Subject)
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

            builder.Property(p => p.AuthorName)
               .HasColumnName("AuthorName")
                .HasMaxLength(450)
               .IsRequired(false);

            builder.Property(p => p.EventStartDate)
               .HasColumnName("EventStartDate")
               .IsRequired(false);

            builder.Property(p => p.EventEndDate)
               .HasColumnName("EventEndDate")
               .IsRequired(false);

            builder.Property(p => p.Venue)
               .HasColumnName("Visibility")
                .HasMaxLength(450)
               .IsRequired(false);

            builder.Property(p => p.DurationPerDay)
            .HasColumnName("DurationPerDay")
             .HasMaxLength(150)
            .IsRequired(false);


            #endregion
            #region Relationship
            builder.HasOne(d => d.ApplicationUser)
                   .WithMany(d => d.Entrepreneurships)
                   .HasForeignKey(d => d.UserProfileId)
                   .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
