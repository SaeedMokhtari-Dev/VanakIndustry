using System.Collections.Generic;

namespace VanakIndustry.Web.Controllers.Entities.ElectionCandidateTypes.Get
{
    public class ElectionCandidateTypeGetResponse
    {
        public int TotalCount { get; set; }
        public List<ElectionCandidateTypeGetResponseItem> Items { get; set; }
    }
    public class ElectionCandidateTypeGetResponseItem
    {
        public long Key { get; set; }
        public string Title { get; set; }
    }
}
