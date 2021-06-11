using System;
using System.Threading.Tasks;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Core.Constants;
using VanakIndustry.Web.Identity.Services;
using ActionResult = VanakIndustry.Core.Api.Models.ActionResult;

namespace VanakIndustry.Web.Controllers.Auth.RefreshAccess
{
    public class RefreshAccessTokenHandler : ApiRequestHandler<RefreshAccessTokenRequest>
    {
        private readonly RefreshTokenService _refreshTokenService;
        private readonly AccessTokenService _accessTokenService;

        public RefreshAccessTokenHandler(RefreshTokenService refreshTokenService, AccessTokenService accessTokenService)
        {
            _refreshTokenService = refreshTokenService;
            _accessTokenService = accessTokenService;
        }

        protected override async Task<ActionResult> Execute(RefreshAccessTokenRequest request)
        {
            if(request == null || string.IsNullOrWhiteSpace(request.AccessToken) || string.IsNullOrWhiteSpace(request.RefreshToken)) throw new Exception(ApiMessages.InvalidRequest);

            var result = _accessTokenService.GetValidationResult(request.AccessToken);

            if (!result.IsValid)
            {
                return LoginRequiredResponse();
            }

            if (result.IsExpired)
            {
                return await RefreshTokenResponse(request.RefreshToken, result.UserId);
            }

            return CurrentTokensResponse(request.AccessToken, request.RefreshToken);

        }

        private ActionResult LoginRequiredResponse()
        {
            return ActionResult.Ok(new RefreshAccessTokenResponse
            {
                IsLoginRequired = true
            });
        }

        private ActionResult CurrentTokensResponse(string accessToken, string refreshToken)
        {
            return ActionResult.Ok(new RefreshAccessTokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }

        private async Task<ActionResult> RefreshTokenResponse(string refreshToken, long userId)
        {
            var updatedRefreshToken = await _refreshTokenService.GetUpdatedRefreshToken(refreshToken, userId);

            if (updatedRefreshToken == null) return LoginRequiredResponse();

            var newAccessToken = _accessTokenService.GenerateAccessToken(userId);

            return ActionResult.Ok(new RefreshAccessTokenResponse
            {
                RefreshToken = updatedRefreshToken.Token,
                AccessToken = newAccessToken
            });
        }
    }
}
