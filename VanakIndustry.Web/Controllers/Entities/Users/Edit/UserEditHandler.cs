using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Itenso.TimePeriod;
using Microsoft.EntityFrameworkCore;
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
            User editUser = await _context.Users.Include(w => w.Picture)
                .Include(w => w.Card)
                .Include(w => w.Roles)
                .Include(w => w.CandidatePicture)
                .Include(w => w.NationalCard)
                .Include(w => w.FirstPageCertificate)
                .Include(w => w.SecondPageCertificate)
                .FirstOrDefaultAsync(w => w.Id == request.UserId);

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
            var isNationalIdDuplicate =
                _context.Users.Any(w => w.NationalId.Trim().ToUpper() == request.NationalId.Trim().ToUpper()
                                        && w.Id != request.UserId);
            if (isNationalIdDuplicate)
            {
                return ActionResult.Error(ApiMessages.DuplicateNationalId);
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

            if (!string.IsNullOrEmpty(request.Barcode))
            {
                var isBarcodeDuplicate =
                    _context.Users.Any(w => w.Barcode.Trim().ToUpper() == request.Barcode.Trim().ToUpper()
                                            && w.Id != request.UserId);
                if (isBarcodeDuplicate)
                {
                    return ActionResult.Error(ApiMessages.DuplicateBarcode);
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
                    editUser.Card.CreatedAt = DateTime.Now;
                    editUser.Card.Image = request.CardImage.ToCharArray().Select(Convert.ToByte).ToArray();
                }

                if (request.PictureImageChanged && !string.IsNullOrEmpty(request.PictureImage))
                {
                    editUser.Picture.CreatedAt = DateTime.Now;
                    editUser.Picture.Image = request.PictureImage.ToCharArray().Select(Convert.ToByte).ToArray();
                }

                if (request.CandidatePictureImageChanged && !string.IsNullOrEmpty(request.CandidatePictureImage))
                {
                    editUser.CandidatePicture.CreatedAt = DateTime.Now;
                    editUser.CandidatePicture.Image =
                        request.CandidatePictureImage.ToCharArray().Select(Convert.ToByte).ToArray();
                }

                if (request.NationalCardImageChanged && !string.IsNullOrEmpty(request.NationalCardImage))
                {
                    editUser.NationalCard.CreatedAt = DateTime.Now;
                    editUser.NationalCard.Image =
                        request.NationalCardImage.ToCharArray().Select(Convert.ToByte).ToArray();
                }

                if (request.FirstPageCertificateImageChanged && !string.IsNullOrEmpty(request.FirstPageCertificateImage))
                {
                    editUser.FirstPageCertificate.CreatedAt = DateTime.Now;
                    editUser.FirstPageCertificate.Image = request.FirstPageCertificateImage.ToCharArray()
                        .Select(Convert.ToByte).ToArray();
                }

                if (request.SecondPageCertificateImageChanged && request.Married && !string.IsNullOrEmpty(request.SecondPageCertificateImage))
                {
                    editUser.SecondPageCertificate.Image = request.SecondPageCertificateImage.ToCharArray()
                        .Select(Convert.ToByte).ToArray();
                    editUser.SecondPageCertificate.CreatedAt = DateTime.Now;
                }

                if(request.PasswordChanged)
                    editUser.Password = _passwordService.GetPasswordHash(request.Password);

                //editUser = (await _context.Users.AddAsync(editUser)).Entity;
                await _context.SaveChangesAsync();

                return editUser;
            });
        }
    }
}