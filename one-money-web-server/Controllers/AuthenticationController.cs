using common.ResponseBuilder.Contracts;
using common.WebApi;
using common.WebApi.RoutingConfiguration;
using domain.Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
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
                await userAuthenticationService.SignUpUserAsync(userDto);

                return new OkObjectResult(SuccessResponseBody(null));
            } catch (Exception exc) {
                return new BadRequestObjectResult(ErrorResponseBody(exc.Message, HttpStatusCode.BadRequest));
            }
        }
    }
}
