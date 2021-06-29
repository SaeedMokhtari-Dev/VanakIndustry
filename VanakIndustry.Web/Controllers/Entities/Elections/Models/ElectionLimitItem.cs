namespace VanakIndustry.Web.Controllers.Entities.Elections.Add
{
    public class ElectionLimitItem
    {
        public long? Id { get; set; }
        public long ElectionId { get; set; }
        public long ElectionCandidateTypeId { get; set; }
        public string ElectionCandidateTypeTitle { get; set; }
        public int LimitCount { get; set; }
    }
}