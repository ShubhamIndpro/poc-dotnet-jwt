using Microsoft.AspNetCore.Mvc;
using CustomJWTAuthentication.Middleware;
using CustomJWTAuthentication.Services;
using CustomJWTAuthentication.Models.Entities;
using CustomJWTAuthentication.Models;

namespace CustomJWTAuthentication.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Role.Admin)]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var currentUser = (User)HttpContext.Items["User"];
            if (id != currentUser.Id )
                return Unauthorized(new { message = "Unauthorized" });

            var user = _userService.GetById(id);
            return Ok(user);
        }
    }
}
