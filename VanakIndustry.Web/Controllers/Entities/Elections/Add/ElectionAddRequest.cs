using System;
using System.Collections.Generic;

namespace VanakIndustry.Web.Controllers.Entities.Elections.Add
{
    public class ElectionAddRequest
    {
        public string Title { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public bool Iplimit { get; set; }
        public string Iplist { get; set; }

        public List<ElectionLimitItem> ElectionLimitItems { get; set; }
    }
}
