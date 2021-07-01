using System.Collections.Generic;

namespace VanakIndustry.Web.Controllers.Entities.SelectElectionCandidates.Add
{
    public class SelectElectionCandidateAddRequest
    {
        public long ElectionId { get; set; }
        public long UserId { get; set; }
        public List<long> ElectionCandidateIds { get; set; }
    }
}
