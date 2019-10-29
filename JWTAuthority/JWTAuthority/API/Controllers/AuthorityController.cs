using JWTAuthority.API.Models;
using JWTAuthority.Models;
using JWTAuthority.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuthority.API.Controllers
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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetToken([FromBody] AuthorizationModel model)
        {
            var result = _authenticationService.Authenticate(model);

            if (result == null)
            {
                return Unauthorized("Wrong email or password!");
            }

            return Ok(result);
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult RegisterUser([FromBody] RegisterModel model)
        {
            _registerService.Register(model);

            return Ok();
        }
    }
}