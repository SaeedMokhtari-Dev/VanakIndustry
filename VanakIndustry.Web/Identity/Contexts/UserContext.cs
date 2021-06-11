using System.Collections.Generic;
using VanakIndustry.Core.Enums;
using VanakIndustry.Core.Interfaces;
using VanakIndustry.DataAccess.Entities;

namespace VanakIndustry.Web.Identity.Contexts
{
    public class UserContext: IScoped
    {
        public long Id { get; set; }

        public bool IsAuthenticated { get; set; }

        public List<UserRole> Roles { get; set; }

        public bool IsActive { get; set; }
    }
}
