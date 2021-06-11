#nullable disable

namespace VanakIndustry.DataAccess.Entities
{
    public partial class SelectElectionCandidate
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long ElectionCandidateId { get; set; }
        public bool Finalize { get; set; }

        public virtual ElectionCandidate ElectionCandidate { get; set; }
        public virtual User User { get; set; }
    }
}
