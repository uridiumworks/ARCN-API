using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARCN.Domain.Entities.Map
{
    public class ApplicationRoleMap : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            #region Properties
            //builder.Property(p => p.Description)
            //    .HasMaxLength(200)
            //    .IsRequired(false);
            #endregion
        }
    }
}
