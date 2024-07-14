using CodePulse.Api.Models.DTO;
using CodePulse.Api.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace categories.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository iTokenRepository;
        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository iTokenRepository)
        {
            this.userManager = userManager;
            this.iTokenRepository = iTokenRepository;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto requestDto)
        {
            //check email
            var identityUser = await userManager.FindByEmailAsync(requestDto.Email);
            if (identityUser is not null)
            {
                var checkPasswordResult = await userManager
                      .CheckPasswordAsync(identityUser, requestDto.Password);
                if (checkPasswordResult)
                {
                    var roles = await userManager.GetRolesAsync(identityUser);
                    var token = iTokenRepository.CreateJwtToken(identityUser, roles.ToList());
                    var response = new LoginResponseDto()
                    {
                        Email = requestDto.Email,
                        Roles= roles.ToList(),
                        Token = token
                    };
                    return Ok(response);
                } 
            }
            ModelState.AddModelError("", "Email or password in incorrect");
            return ValidationProblem(ModelState);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            var user = new IdentityUser()
            {
                Email = request.Email,
                UserName = request.Email
            };
            var identityResult = await userManager.CreateAsync(user, request.Password);
            if (identityResult.Succeeded)
            {
                identityResult = await userManager.AddToRoleAsync(user, "Reader");
                if (identityResult.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    if (identityResult.Errors.Any())
                    {
                        foreach (var error in identityResult.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
            }
            else
            {
                if (identityResult.Errors.Any())
                {
                    foreach (var error in identityResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return ValidationProblem(ModelState);
        }
    }
}
