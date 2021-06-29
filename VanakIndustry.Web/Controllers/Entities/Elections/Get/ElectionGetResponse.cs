using System;
using System.Collections.Generic;
using VanakIndustry.DataAccess.Entities;
using VanakIndustry.Web.Controllers.Entities.Elections.Add;

namespace VanakIndustry.Web.Controllers.Entities.Elections.Get
{
    public class ElectionGetResponse
    {
        public int TotalCount { get; set; }
        public List<ElectionGetResponseItem> Items { get; set; }
    }
    public class ElectionGetResponseItem
    {
        public long Key { get; set; }
        public string Title { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public bool Iplimit { get; set; }
        public string Iplist { get; set; }
        public bool Finalize { get; set; }
        public DateTime? FinalizeDate { get; set; }
        public List<ElectionLimitItem> ElectionLimitItems { get; set; }
    }
}
