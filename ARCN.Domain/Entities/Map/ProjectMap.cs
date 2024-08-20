using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARCN.Domain.Entities.Map
{
    public class ProjectMap : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("tbl_Project");
            builder.HasKey(s=>s.ProjectId);

            #region Properties
            builder.Property(p => p.ProjectId)
            .HasColumnName("Project")
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

            builder.Property(p => p.UseBanner)
               .HasColumnName("UseBanner")
               .IsRequired(false);



            builder.Property(p => p.PublishOn)
                .HasColumnName("PublishOn")
                .IsRequired(false);


            #endregion
            #region Relationship
            builder.HasOne(d => d.ApplicationUser)
                   .WithMany(d => d.Projects)
                   .HasForeignKey(d => d.UserProfileId)
                   .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
