using api.Features.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace api.Features.Auth
{

    public class AuthController : BaseApiController
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;

        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.configuration = configuration;
        }

        [HttpPost("signin")]
        public async Task<object> ApiSignIn([FromBody] SignInCredentials creds)
        {
            var user = await userManager.FindByEmailAsync(creds.Email);
            var result = await signInManager.CheckPasswordSignInAsync(user,
            creds.Password, true);
            if (result.Succeeded)
            {
                var descriptor = new SecurityTokenDescriptor
                {
                    Subject = (await signInManager.CreateUserPrincipalAsync(user))
                .Identities.First(),
                    Expires = DateTime.Now.AddMinutes(int.Parse(
                configuration["BearerTokens:ExpiryMins"])),
                    SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                configuration["BearerTokens:Key"])),
               SecurityAlgorithms.HmacSha256Signature)
                };
                var handler = new JwtSecurityTokenHandler();
                SecurityToken secToken = new JwtSecurityTokenHandler()
                .CreateToken(descriptor);
                return new { success = true, token = handler.WriteToken(secToken) };
            }
            return new { success = false };
        }

        //[HttpPost("signout")]
        //public async Task<IActionResult> ApiSignOut()
        //{
        //    await signInManager.SignOutAsync();
        //    return Ok();
        //}

        public class SignInCredentials
        {
            [Required]
            public string Email { get; set; }
            [Required]
            public string Password { get; set; }
        }
    }
}
