using System.Collections.Generic;

namespace VanakIndustry.Web.Controllers.Entities.ElectionCandidates.Get
{
    public class ElectionCandidateGetResponse
    {
        public long ElectionId { get; set; }
        public string ElectionTitle { get; set; }
        public long ElectionCandidateTypeId { get; set; }
        public string ElectionCandidateTypeTitle { get; set; }
        public int LimitCount { get; set; }
        public List<ElectionCandidateGetResponseItem> ElectionCandidateGetResponseItems { get; set; }
    }

    public class ElectionCandidateGetResponseItem
    {
        public long Key { get; set; }
        public long UserId { get; set; }
        public string UserFullName { get; set; }
        public long CandidatePictureId { get; set; }
    }
}
