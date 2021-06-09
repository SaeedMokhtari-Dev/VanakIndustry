using System;

#nullable disable

namespace VanakIndustry.DataAccess.Entities
{
    public partial class RefreshToken
    {
        public long Id { get; set; }
        public long PersonId { get; set; }
        public string Token { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }

        public virtual Person Person { get; set; }
    }
}
