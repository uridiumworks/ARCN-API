
using ARCN.Domain.Commons;

namespace ARCN.Domain.Entities
{
    public class LocalGovernmentArea : AuditableEntity
    {
        public LocalGovernmentArea()
        {
            Naris = new HashSet<Naris>();
        }
        public int LocalGovernmentAreaId { get; set; }
        public int StateId { get; set; }
        public string LocalGovernmentName { get; set; }
        public virtual State? State { get; set; }
        public virtual ICollection<Naris> Naris { get; set; }
      

    }
}
