using JWTAuthority.API.Models;
using JWTAuthority.Models;
using JWTAuthority.Service;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuthority.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthorityController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IRegisterService _registerService;

        public AuthorityController(IAuthenticationService authenticationService, IRegisterService registerService)
        {
            _authenticationService = authenticationService;
            _registerService = registerService;
        }

        [HttpPost("token")]
        public IActionResult GetToken([FromBody] AuthorizationModel model)
        {
            var result = _authenticationService.Authenticate(model);

            if(result == null)
            {
                return Unauthorized();
            }

            return Ok(result);
        }

        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody] RegisterModel model)
        {
            _registerService.Register(model);

            return Ok();
        }
    }
}
