using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Core.Api.Models;
using VanakIndustry.Core.Constants;
using VanakIndustry.Core.Enums;
using VanakIndustry.DataAccess.Contexts;
using VanakIndustry.DataAccess.Entities;
using VanakIndustry.Web.Identity.Services;

namespace VanakIndustry.Web.Controllers.Entities.Users.Add
{
    public class UserAddHandler : ApiRequestHandler<UserAddRequest>
    {
        private readonly VanakIndustryContext _context;
        private readonly IMapper _mapper;
        private readonly PasswordService _passwordService;
        
        public UserAddHandler(
            VanakIndustryContext context, IMapper mapper, PasswordService passwordService)
        {
            _context = context;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        protected override async Task<ActionResult> Execute(UserAddRequest request)
        {
            var isUsernameDuplicate =
                _context.Users.Any(w => w.Username.Trim().ToUpper() == request.Username.Trim().ToUpper());
            if (isUsernameDuplicate)
            {
                return ActionResult.Error(ApiMessages.DuplicateUserName);
            }
            var isNationalIdDuplicate =
                _context.Users.Any(w => w.NationalId.Trim().ToUpper() == request.NationalId.Trim().ToUpper());
            if (isNationalIdDuplicate)
            {
                return ActionResult.Error(ApiMessages.DuplicateNationalId);
            }

            if (!string.IsNullOrEmpty(request.Email))
            {
                var isEmailDuplicate =
                    _context.Users.Any(w => w.Email.Trim().ToUpper() == request.Email.Trim().ToUpper());
                if (isEmailDuplicate)
                {
                    return ActionResult.Error(ApiMessages.DuplicateEmail);
                }
            }
            if (!string.IsNullOrEmpty(request.Barcode))
            {
                var isBarcodeDuplicate =
                    _context.Users.Any(w => w.Barcode.Trim().ToUpper() == request.Barcode.Trim().ToUpper());
                if (isBarcodeDuplicate)
                {
                    return ActionResult.Error(ApiMessages.DuplicateBarcode);
                }
            }
            User user = await AddUser(request);
            
            return ActionResult.Ok(ApiMessages.UserMessage.AddedSuccessfully);
        }
        
        private async Task<User> AddUser(UserAddRequest request)
        {
            User user = await _context.ExecuteTransactionAsync(async () =>
            {
                User newUser = _mapper.Map<User>(request);

                if (!string.IsNullOrEmpty(request.CardImage))
                {
                    newUser.Card = new Attachment()
                    {
                        CreatedAt = DateTime.Now,
                        Image = request.CardImage.ToCharArray().Select(Convert.ToByte).ToArray()
                    };
                }
                if (!string.IsNullOrEmpty(request.PictureImage))
                {
                    newUser.Picture = new Attachment()
                    {
                        CreatedAt = DateTime.Now,
                        Image = request.PictureImage.ToCharArray().Select(Convert.ToByte).ToArray()
                    };
                }
                if (!string.IsNullOrEmpty(request.CandidatePictureImage))
                {
                    newUser.CandidatePicture = new Attachment()
                    {
                        CreatedAt = DateTime.Now,
                        Image = request.CandidatePictureImage.ToCharArray().Select(Convert.ToByte).ToArray()
                    };
                }
                if (!string.IsNullOrEmpty(request.NationalCardImage))
                {
                    newUser.NationalCard = new Attachment()
                    {
                        CreatedAt = DateTime.Now,
                        Image = request.NationalCardImage.ToCharArray().Select(Convert.ToByte).ToArray()
                    };
                }
                if (!string.IsNullOrEmpty(request.FirstPageCertificateImage))
                {
                    newUser.FirstPageCertificate = new Attachment()
                    {
                        CreatedAt = DateTime.Now,
                        Image = request.FirstPageCertificateImage.ToCharArray().Select(Convert.ToByte).ToArray()
                    };
                }
                if (request.Married && !string.IsNullOrEmpty(request.SecondPageCertificateImage))
                {
                    newUser.SecondPageCertificate = new Attachment()
                    {
                        CreatedAt = DateTime.Now,
                        Image = request.SecondPageCertificateImage.ToCharArray().Select(Convert.ToByte).ToArray()
                    };
                }

                newUser.Password = _passwordService.GetPasswordHash(request.Password);
                
                
                newUser.Roles.Add(new UserRole()
                {
                    Role = RoleType.User
                });
                
                newUser = (await _context.Users.AddAsync(newUser)).Entity;
                await _context.SaveChangesAsync();

                return newUser;
            });
            return user;
        }
    }
}