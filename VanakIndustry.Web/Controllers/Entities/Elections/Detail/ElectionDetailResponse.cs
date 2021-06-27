using System;
using System.Collections.Generic;
using VanakIndustry.DataAccess.Entities;
using VanakIndustry.Web.Controllers.Entities.Elections.Add;

namespace VanakIndustry.Web.Controllers.Entities.Elections.Detail
{
    public class ElectionDetailResponse
    {
        public long Key { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Iplimit { get; set; }
        public string Iplist { get; set; }

        public List<ElectionLimitItem> ElectionLimitItems { get; set; }
        
        public List<ElectionCandidateItem> ElectionCandidateItems { get; set; }
    }

    public class ElectionCandidateItem
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string UserFullName { get; set; }
        public long ElectionCandidateTypeId { get; set; }
        public string ElectionCandidateTypeTitle { get; set; }
    }
}
