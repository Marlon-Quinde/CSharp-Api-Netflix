using Helpers;
using Microsoft.AspNetCore.Mvc;
using Models.DTO.Auth;
using Services.AuthService;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiNetflixApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _authServices;
        public AuthController(IAuthServices authServices)
        {
            _authServices = authServices;
        }

        // POST api/<AuthController>
        [HttpPost("Sign-In")]
        public async Task<IActionResult> SingInController(SignInDTO payload)
        {
            try
            {
                var response = await _authServices.SignInServices(payload);
                return StatusCode((int)response.Code, response);
            }
            catch (Exception ex)
            {

                return BadRequest(new Response<string>()
                {
                    Code = HttpStatusCode.InternalServerError,
                    Message = ex.Message,
                    Data = null,
                });
            }
        }
    }
}
