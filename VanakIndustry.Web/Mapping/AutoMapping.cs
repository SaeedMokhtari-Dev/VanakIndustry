using System;
using System.Globalization;
using System.Linq;
using AutoMapper;
using VanakIndustry.Core.Constants;
using VanakIndustry.DataAccess.Entities;
using VanakIndustry.Web.Controllers.Auth.Register;
using VanakIndustry.Web.Controllers.Entities.Users.Add;
using VanakIndustry.Web.Controllers.Entities.Users.Detail;
using VanakIndustry.Web.Controllers.Entities.Users.Edit;
using VanakIndustry.Web.Controllers.Entities.Users.Get;
using VanakIndustry.Web.Extensions;

namespace VanakIndustry.Web.Mapping
{
    public class AutoMapping: Profile
    {
        public AutoMapping()
        {
            #region User

            CreateMap<RegisterRequest, User>()
                .ForMember(w => w.Password, opt => opt.Ignore())
                .ForMember(w => w.BirthDate, opt => 
                    opt.MapFrom(e => !string.IsNullOrEmpty(e.BirthDate) ? e.BirthDate.Replace("/", "") : string.Empty))
                .ForMember(w => w.CreatedAt, opt => opt.MapFrom(e => DateTime.Now))
                .ForMember(w => w.ModifiedAt, opt => opt.MapFrom(e => DateTime.Now));
            
            CreateMap<UserAddRequest, User>()
                .ForMember(w => w.Password, opt => opt.Ignore())
                .ForMember(w => w.BirthDate, opt => 
                    opt.MapFrom(e => !string.IsNullOrEmpty(e.BirthDate) ? e.BirthDate.Replace("/", "") : string.Empty))
                .ForMember(w => w.CreatedAt, opt => opt.MapFrom(e => DateTime.Now))
                .ForMember(w => w.ModifiedAt, opt => opt.MapFrom(e => DateTime.Now))
                .ForMember(w => w.IsActive, opt => opt.MapFrom(e => true));
            
            CreateMap<UserEditRequest, User>()
                .ForMember(w => w.Id, opt => opt.Ignore())
                .ForMember(w => w.Password, opt => opt.Ignore())
                .ForMember(w => w.BirthDate, opt => 
                    opt.MapFrom(e => !string.IsNullOrEmpty(e.BirthDate) ? e.BirthDate.Replace("/", "") : string.Empty))
                .ForMember(w => w.ModifiedAt, opt => opt.MapFrom(e => DateTime.Now));

            CreateMap<User, UserDetailResponse>()
                .ForMember(w => w.Key, opt => opt.MapFrom(e => e.Id))
                .ForMember(w => w.Password, opt => opt.Ignore())
                .ForMember(w => w.BirthDate, opt =>
                    opt.MapFrom(e =>
                        !string.IsNullOrEmpty(e.BirthDate)
                            ? e.BirthDate.Insert(6, "/").Insert(4, "/").ToString()
                            : string.Empty))
                .ForMember(w => w.CreatedAt, opt => opt.MapFrom(e => e.CreatedAt.ToPersianDateTime()))
                .ForMember(w => w.ModifiedAt, opt => opt.MapFrom(e => e.ModifiedAt.ToPersianDateTime()))
                .ForMember(w => w.LastLoginAt,
                    opt => opt.MapFrom(e =>
                        e.LastLoginAt.HasValue ? e.LastLoginAt.Value.ToPersianDateTime() : String.Empty))
                .ForMember(w => w.CardImage, opt =>
                    opt.MapFrom(e =>
                        e.CardId.HasValue ? String.Join("", e.Card.Image.Select(Convert.ToChar)) : String.Empty))
                .ForMember(w => w.SecondPageCertificateImage, opt =>
                    opt.MapFrom(e =>
                        e.SecondPageCertificateId.HasValue
                            ? String.Join("", e.SecondPageCertificate.Image.Select(Convert.ToChar))
                            : String.Empty))
                .ForMember(w => w.CandidatePictureImage, opt =>
                    opt.MapFrom(e =>
                        e.CandidatePictureId.HasValue
                            ? String.Join("", e.CandidatePicture.Image.Select(Convert.ToChar))
                            : String.Empty))
                .ForMember(w => w.PictureImage, opt =>
                    opt.MapFrom(e =>
                        e.PictureId.HasValue ? String.Join("", e.Picture.Image.Select(Convert.ToChar)) : String.Empty))
                .ForMember(w => w.FirstPageCertificateImage, opt =>
                    opt.MapFrom(e => String.Join("", e.FirstPageCertificate.Image.Select(Convert.ToChar))))
                .ForMember(w => w.NationalCardImage, opt =>
                    opt.MapFrom(e => String.Join("", e.NationalCard.Image.Select(Convert.ToChar))));

            CreateMap<User, UserGetResponseItem>()
                .ForMember(w => w.Key, opt => opt.MapFrom(e => e.Id))
                .ForMember(w => w.BirthDate, opt =>
                    opt.MapFrom(e =>
                        !string.IsNullOrEmpty(e.BirthDate)
                            ? e.BirthDate.Insert(6, "/").Insert(4, "/").ToString()
                            : string.Empty))
                .ForMember(w => w.CreatedAt, opt => opt.MapFrom(e => e.CreatedAt.ToPersianDateTime()))
                .ForMember(w => w.ModifiedAt, opt => opt.MapFrom(e => e.ModifiedAt.ToPersianDateTime()))
                .ForMember(w => w.LastLoginAt,
                    opt => opt.MapFrom(e =>
                        e.LastLoginAt.HasValue ? e.LastLoginAt.Value.ToPersianDateTime() : String.Empty));


            #endregion

        }
    }
}