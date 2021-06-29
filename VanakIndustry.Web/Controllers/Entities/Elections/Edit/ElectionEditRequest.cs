using System;
using System.Collections.Generic;
using VanakIndustry.Web.Controllers.Entities.Elections.Add;

namespace VanakIndustry.Web.Controllers.Entities.Elections.Edit
{
    public class ElectionEditRequest
    {
        public long ElectionId { get; set; }
        public string Title { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public bool Iplimit { get; set; }
        public string Iplist { get; set; }

        public List<ElectionLimitItem> ElectionLimitItems { get; set; }
    }
}
