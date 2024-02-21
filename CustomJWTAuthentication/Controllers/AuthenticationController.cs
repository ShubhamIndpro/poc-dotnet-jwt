using CustomJWTAuthentication.Middleware;
using CustomJWTAuthentication.Models;
using CustomJWTAuthentication.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CustomJWTAuthentication.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserService _userService;
        public AuthenticationController(IAuthenticationService authenticationService, IUserService userService)
        {
            _authenticationService = authenticationService;
            _userService = userService;
        }

        [HttpPost("[action]")]
        public IActionResult Register(RegisterRequest model)
        {
            _authenticationService.Register(model);
            return Ok();
        }

        [HttpPost("[action]")]
        public IActionResult RegisterAdmin(RegisterRequest model)
        {
            _authenticationService.RegisterAdmin(model);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _authenticationService.Authenticate(model);
            return Ok(response);
        }
    }
}
