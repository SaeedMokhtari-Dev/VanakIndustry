using System;

#nullable disable

namespace VanakIndustry.DataAccess.Entities
{
    public partial class ElectionPresentUser
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long ElectionId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ExpireDate { get; set; }

        public virtual Election Election { get; set; }
        public virtual User User { get; set; }
    }
}
