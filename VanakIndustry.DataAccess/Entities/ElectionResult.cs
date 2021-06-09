using System;

#nullable disable

namespace VanakIndustry.DataAccess.Entities
{
    public partial class ElectionResult
    {
        public long Id { get; set; }
        public long ElectionId { get; set; }
        public long PersonId { get; set; }
        public long ElectionCandidateTypeId { get; set; }
        public int VoteNumber { get; set; }
        public bool Winner { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual Election Election { get; set; }
        public virtual ElectionCandidateType ElectionCandidateType { get; set; }
        public virtual Person Person { get; set; }
    }
}
