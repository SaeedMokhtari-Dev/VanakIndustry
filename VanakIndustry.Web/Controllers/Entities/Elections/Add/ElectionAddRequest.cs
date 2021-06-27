using System;
using System.Collections.Generic;

namespace VanakIndustry.Web.Controllers.Entities.Elections.Add
{
    public class ElectionAddRequest
    {
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Iplimit { get; set; }
        public string Iplist { get; set; }

        public List<ElectionLimitItem> ElectionLimitItems { get; set; }
    }

    public class ElectionLimitItem
    {
        public long? Id { get; set; }
        public long ElectionCandidateTypeId { get; set; }
        public string ElectionCandidateTypeTitle { get; set; }
        public int LimitCount { get; set; }
    }
}
