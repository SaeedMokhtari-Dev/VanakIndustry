using System.Collections.Generic;

namespace VanakIndustry.Web.Controllers.Entities.SelectElectionCandidates.Get
{
    public class SelectElectionCandidateGetResponse
    {
        public long Key { get; set; }
        public long ElectionId { get; set; }
        public string ElectionTitle { get; set; }
        public long ElectionCandidateTypeId { get; set; }
        public string ElectionCandidateTypeTitle { get; set; }
        public long UserId { get; set; }
        public string UserFullName { get; set; }
        public long CandidatePictureId { get; set; }
    }
}
