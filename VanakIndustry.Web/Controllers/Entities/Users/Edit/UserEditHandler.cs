using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Core.Api.Models;
using VanakIndustry.Core.Constants;
using VanakIndustry.DataAccess.Contexts;
using VanakIndustry.DataAccess.Entities;
using VanakIndustry.Web.Identity.Services;

namespace VanakIndustry.Web.Controllers.Entities.Users.Edit
{
    public class UserEditHandler : ApiRequestHandler<UserEditRequest>
    {
        private readonly VanakIndustryContext _context;
        private readonly IMapper _mapper;
        private readonly PasswordService _passwordService;

        public UserEditHandler(
            VanakIndustryContext context, IMapper mapper, PasswordService passwordService)
        {
            _context = context;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        protected override async Task<ActionResult> Execute(UserEditRequest request)
        {
            User editUser = await _context.Users
                .FindAsync(request.UserId);

            if (editUser == null)
            {
                return ActionResult.Error(ApiMessages.ResourceNotFound);
            }

            var isUsernameDuplicate =
                _context.Users.Any(w => w.Username.Trim().ToUpper() == request.Username.Trim().ToUpper()
                                        && w.Id != request.UserId);
            if (isUsernameDuplicate)
            {
                return ActionResult.Error(ApiMessages.DuplicateUserName);
            }

            if (!string.IsNullOrEmpty(request.Email))
            {
                var isEmailDuplicate =
                    _context.Users.Any(w => w.Email.Trim().ToUpper() == request.Email.Trim().ToUpper()
                                            && w.Id != request.UserId);
                if (isEmailDuplicate)
                {
                    return ActionResult.Error(ApiMessages.DuplicateEmail);
                }
            }

            await EditUser(editUser, request);
            return ActionResult.Ok(ApiMessages.UserMessage.EditedSuccessfully);
        }

        private async Task EditUser(User editUser, UserEditRequest request)
        {
            await _context.ExecuteTransactionAsync(async () =>
            {
                //User newUser = _mapper.Map<User>(request);
                _mapper.Map(request, editUser);

                if (request.CardImageChanged && !string.IsNullOrEmpty(request.CardImage))
                {
                    editUser.Card = new Attachment()
                    {
                        CreatedAt = DateTime.Now,
                        Image = request.CardImage.ToCharArray().Select(Convert.ToByte).ToArray()
                    };
                }

                if (request.PictureImageChanged && !string.IsNullOrEmpty(request.PictureImage))
                {
                    editUser.Picture = new Attachment()
                    {
                        CreatedAt = DateTime.Now,
                        Image = request.PictureImage.ToCharArray().Select(Convert.ToByte).ToArray()
                    };
                }

                if (request.CandidatePictureImageChanged && !string.IsNullOrEmpty(request.CandidatePictureImage))
                {
                    editUser.CandidatePicture = new Attachment()
                    {
                        CreatedAt = DateTime.Now,
                        Image = request.CandidatePictureImage.ToCharArray().Select(Convert.ToByte).ToArray()
                    };
                }

                if (request.NationalCardImageChanged && !string.IsNullOrEmpty(request.NationalCardImage))
                {
                    editUser.NationalCard = new Attachment()
                    {
                        CreatedAt = DateTime.Now,
                        Image = request.NationalCardImage.ToCharArray().Select(Convert.ToByte).ToArray()
                    };
                }

                if (request.FirstPageCertificateImageChanged && !string.IsNullOrEmpty(request.FirstPageCertificateImage))
                {
                    editUser.FirstPageCertificate = new Attachment()
                    {
                        CreatedAt = DateTime.Now,
                        Image = request.FirstPageCertificateImage.ToCharArray().Select(Convert.ToByte).ToArray()
                    };
                }

                if (request.SecondPageCertificateImageChanged && request.Married && !string.IsNullOrEmpty(request.SecondPageCertificateImage))
                {
                    editUser.SecondPageCertificate = new Attachment()
                    {
                        CreatedAt = DateTime.Now,
                        Image = request.SecondPageCertificateImage.ToCharArray().Select(Convert.ToByte).ToArray()
                    };
                }

                if(request.PasswordChanged)
                    editUser.Password = _passwordService.GetPasswordHash(request.Password);

                editUser = (await _context.Users.AddAsync(editUser)).Entity;
                await _context.SaveChangesAsync();

                return editUser;
            });
        }
    }
}