using System;

#nullable disable

namespace VanakIndustry.DataAccess.Entities
{
    public partial class PasswordResetToken
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public Guid Token { get; set; }
        public DateTime ResetRequestDate { get; set; }

        public virtual User User { get; set; }
    }
}
