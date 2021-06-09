using System.Collections.Generic;

#nullable disable

namespace VanakIndustry.DataAccess.Entities
{
    public partial class ElectionCandidateType
    {
        public ElectionCandidateType()
        {
            ElectionCandidates = new HashSet<ElectionCandidate>();
            ElectionLimits = new HashSet<ElectionLimit>();
            ElectionResults = new HashSet<ElectionResult>();
        }

        public long Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<ElectionCandidate> ElectionCandidates { get; set; }
        public virtual ICollection<ElectionLimit> ElectionLimits { get; set; }
        public virtual ICollection<ElectionResult> ElectionResults { get; set; }
    }
}
