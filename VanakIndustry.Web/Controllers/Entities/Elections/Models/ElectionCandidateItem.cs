using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace VanakIndustry.Web.Controllers.Entities.Elections.Detail
{
    public class ElectionCandidateItem
    {
        public long Id { get; set; }
        public long ElectionId { get; set; }
        public long UserId { get; set; }
        public string UserFullName { get; set; }
        public long ElectionCandidateTypeId { get; set; }
        public string ElectionCandidateTypeTitle { get; set; }
    }
}