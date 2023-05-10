using common.ResponseBuilder.Contracts;
using common.WebApi;
using common.WebApi.RoutingConfiguration;
using domain.Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using service.Services.UserAuthentication.Contracts;
using System;
using System.Net;
using System.Threading.Tasks;

namespace one_money_web_server.Controllers {
    [AllowAnonymous]
    [AssignControllerRoute(WebApiEnvironment.Current, WebApiVersion.ApiVersion1, "auth")]
    public class AuthenticationController : WebApiControllerBase {
        private readonly IUserAuthenticationService userAuthenticationService;

        public AuthenticationController(
            IResponseFactory responseFactory,
            IUserAuthenticationService userAuthenticationService) : base(responseFactory) {
            this.userAuthenticationService = userAuthenticationService;
        }

        [HttpPost]
        [AssignActionRoute("signup")]
        public async Task<IActionResult> SignUpAsync([FromBody] UserRegistrationDto userDto) {
            try {
                IdentityResult identityResult = await userAuthenticationService.SignUpUserAsync(userDto);

                if (identityResult.Succeeded) {
                    return Ok(SuccessResponseBody(identityResult));
                }

                return BadRequest(ErrorResponseBody(identityResult.Errors.ToString(), HttpStatusCode.BadRequest));
            } catch (Exception exc) {
                return BadRequest(ErrorResponseBody(exc.Message, HttpStatusCode.BadRequest));
            }
        }
    }
}
