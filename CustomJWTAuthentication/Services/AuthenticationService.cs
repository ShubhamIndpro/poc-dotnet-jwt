using CustomJWTAuthentication.Models.Entities;
using CustomJWTAuthentication.Models;
using CustomJWTAuthentication.Middleware;
using CustomJWTAuthentication.Helpers;
using Microsoft.Extensions.Options;
using System.Linq;

namespace CustomJWTAuthentication.Services
{
    public interface IAuthenticationService
    {
        bool Register(RegisterRequest model);

        bool RegisterAdmin(RegisterRequest model);

        AuthenticateResponse Authenticate(AuthenticateRequest model);
    }
    public class AuthenticationService : IAuthenticationService
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly AppSettings _appSettings;

        public AuthenticationService(DataContext context,
            IJwtUtils jwtUtils,
            IOptions<AppSettings> appSettings)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _appSettings = appSettings.Value;
        }

        public bool Register(RegisterRequest model)
        {
            if (_context.Users.Any(x => x.Username == model.Username))
            {
                return false;
            }
            var user = _context.Users.Add(new User(model, Role.User));
            _context.SaveChanges();
            return user != null;
        }

        public bool RegisterAdmin(RegisterRequest model)
        {
            if (_context.Users.Any(x => x.Username == model.Username))
            {
                return false;
            }
            var user = _context.Users.Add(new User(model, Role.Admin));
            _context.SaveChanges();
            return user != null;
        }


        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _context.Users.SingleOrDefault(x => x.Username == model.Username);

            if (user == null)
            {
                throw new Exception("Username not found");
            }
            var jwtToken = _jwtUtils.GenerateJwtToken(user);

            return new AuthenticateResponse(user, jwtToken);
        }
    }
}
