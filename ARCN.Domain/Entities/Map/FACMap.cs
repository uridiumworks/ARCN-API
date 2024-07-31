using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARCN.Domain.Entities.Map
{
    public class FCAMap:IEntityTypeConfiguration<FCA>
    {
        public void Configure(EntityTypeBuilder<FCA> builder)
        {
            builder.ToTable("tbl_FCA");
            builder.HasKey(s=>s.FCAId);

            #region Properties
            builder.Property(p => p.FCAId)
            .HasColumnName("FCAId")
            .IsRequired();

            builder.Property(p => p.UserProfileId)
                .HasColumnName("UserProfileId")
                .IsRequired();

            builder.Property(p => p.PhoneNumber)
                .HasColumnName("PhoneNumber")
                 .HasMaxLength(250)
                .IsRequired(false);

            builder.Property(p => p.Email)
                .HasColumnName("Email")
                 .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(p => p.LogoUrl)
                .HasColumnName("LogoUrl")
                 .HasMaxLength(450)
                .IsRequired(false);

            builder.Property(p => p.Description)
                .HasColumnName("Description")
                .IsRequired(false);

            builder.Property(p => p.Website)
               .HasColumnName("Website")
                .HasMaxLength(450)
               .IsRequired(false);

            builder.Property(p => p.StateId)
               .HasColumnName("StateId")
               .IsRequired(false);

            builder.Property(p => p.LocalGovernmentAreaId)
               .HasColumnName("LocalGovernmentAreaId")
               .IsRequired(false);

            builder.Property(p => p.EstablishDate)
               .HasColumnName("EstablishDate")
               .IsRequired(false);

            builder.Property(p => p.JoinDate)
               .HasColumnName("JoinDate")
               .IsRequired(false);

           
            #endregion
            #region Relationship
            builder.HasOne(d => d.ApplicationUser)
                   .WithMany(d => d.FCAs)
                   .HasForeignKey(d => d.UserProfileId)
                   .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
