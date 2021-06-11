using System.Collections.Generic;
using VanakIndustry.Core.Enums;

namespace VanakIndustry.Web.Controllers.Auth.GetUserInfo
{
    public class GetUserInfoResponse
    {
        public long Id { get; set; }

        public List<RoleType> Roles { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
    }
}
