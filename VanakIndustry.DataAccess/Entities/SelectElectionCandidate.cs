#nullable disable

namespace VanakIndustry.DataAccess.Entities
{
    public partial class SelectElectionCandidate
    {
        public long Id { get; set; }
        public long PersonId { get; set; }
        public long ElectionCandidateId { get; set; }
        public bool Finalize { get; set; }

        public virtual ElectionCandidate ElectionCandidate { get; set; }
        public virtual Person Person { get; set; }
    }
}
