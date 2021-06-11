using VanakIndustry.Core.Enums;

#nullable disable

namespace VanakIndustry.DataAccess.Entities
{
    public partial class UserRole
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public RoleType Role { get; set; }

        public virtual User User { get; set; }
    }
}
