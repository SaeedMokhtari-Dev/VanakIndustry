using System;
using System.Collections.Generic;

namespace VanakIndustry.Web.Controllers.Entities.Elections.AddCandidate
{
    public class ElectionAddCandidateRequest
    {
        public long ElectionId { get; set; }
        public long ElectionCandidateTypeId { get; set; }
        public List<long> UserIds { get; set; }
    }
}
