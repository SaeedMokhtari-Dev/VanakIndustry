﻿using System.Collections.Generic;

#nullable disable

namespace VanakIndustry.DataAccess.Entities
{
    public partial class ElectionCandidate
    {
        public ElectionCandidate()
        {
            SelectElectionCandidates = new HashSet<SelectElectionCandidate>();
        }

        public long Id { get; set; }
        public long ElectionId { get; set; }
        public long PersonId { get; set; }
        public long ElectionCandidateTypeId { get; set; }

        public virtual Election Election { get; set; }
        public virtual ElectionCandidateType ElectionCandidateType { get; set; }
        public virtual Person Person { get; set; }
        public virtual ICollection<SelectElectionCandidate> SelectElectionCandidates { get; set; }
    }
}
