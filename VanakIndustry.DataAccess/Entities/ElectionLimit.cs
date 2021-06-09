#nullable disable

namespace VanakIndustry.DataAccess.Entities
{
    public partial class ElectionLimit
    {
        public long Id { get; set; }
        public long ElectionId { get; set; }
        public long ElectionCandidateTypeId { get; set; }
        public int LimitCount { get; set; }

        public virtual Election Election { get; set; }
        public virtual ElectionCandidateType ElectionCandidateType { get; set; }
    }
}
