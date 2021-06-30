using System;
using System.Collections.Generic;

#nullable disable

namespace VanakIndustry.DataAccess.Entities
{
    public partial class Election
    {
        public Election()
        {
            ElectionCandidates = new HashSet<ElectionCandidate>();
            ElectionLimits = new HashSet<ElectionLimit>();
            ElectionPresentUsers = new HashSet<ElectionPresentUser>();
            ElectionResults = new HashSet<ElectionResult>();
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Iplimit { get; set; }
        public string Iplist { get; set; }
        public bool Finalize { get; set; }
        public DateTime? FinalizeDate { get; set; }
        public bool Deleted { get; set; }

        public virtual ICollection<ElectionCandidate> ElectionCandidates { get; set; }
        public virtual ICollection<ElectionLimit> ElectionLimits { get; set; }
        public virtual ICollection<ElectionPresentUser> ElectionPresentUsers { get; set; }
        public virtual ICollection<ElectionResult> ElectionResults { get; set; }
    }
}
