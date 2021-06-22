using System;
using AutoMapper;
using VanakIndustry.Core.Constants;
using VanakIndustry.DataAccess.Entities;
using VanakIndustry.Web.Controllers.Auth.Register;

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

            CreateMap<RegisterRequest, User>()
                .ForMember(w => w.Password, opt => opt.Ignore())
                .ForMember(w => w.BirthDate, opt => 
                    opt.MapFrom(e => !string.IsNullOrEmpty(e.BirthDate) ? e.BirthDate.Replace("/", "") : string.Empty))
                .ForMember(w => w.CreatedAt, opt => opt.MapFrom(e => DateTime.Now))
                .ForMember(w => w.ModifiedAt, opt => opt.MapFrom(e => DateTime.Now));
        }
    }
}