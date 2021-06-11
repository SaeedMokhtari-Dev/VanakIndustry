using AutoMapper;
using VanakIndustry.Core.Constants;
using VanakIndustry.DataAccess.Entities;

namespace VanakIndustry.Web.Mapping
{
    public class AutoMapping: Profile
    {
        public AutoMapping()
        {
            /*CreateMap<AuditingCompany, DetailAuditingCompanyResponse>()
                .ForMember(w => w.Logo, opt => opt.Ignore());

            CreateMap<UserAddRequest, ApiMessages.User>()
                .ForMember(w => w.Password, opt => opt.Ignore());*/
            
            //CreateMap<User, UserDetailResponse>();
        }
    }
}