using HospitalManagementSystem.JWTModels;
using HospitalManagementSystem.JWTServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [Authorize(Roles = "Patient")]
        [HttpPost("RegisterPatient")]
        public async Task<IActionResult> RegisterPatientAsync([FromBody]RegisterPatientModel registerPatientModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var registeredPatient = await _authService.RegisterPatientAsync(registerPatientModel);
            return Ok(registeredPatient);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody]LoginModel loginModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var loginedUser = await _authService.LoginAsync(loginModel);
            return Ok(loginedUser);
        }
    }
}
