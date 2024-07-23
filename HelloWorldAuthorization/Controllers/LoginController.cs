using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using HelloWorldAuthorization.Models;


namespace HelloWorldAuthorization.Controllers
{
    [Route("api/Login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("Post")]
        public IActionResult Post([FromBody] LoginRequest loginRequest)
        {
            if (loginRequest.Username == "Admin" && loginRequest.Password == "Password")
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var sectoken = new JwtSecurityToken(_config["Jwt:Issuer"],
                    _config["Jwt:Issuer"],
                    null,
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: credentials);

                var token = new JwtSecurityTokenHandler().WriteToken(sectoken);

                return Ok(token);
            }
            
            return BadRequest("Invalid Login Request");
        }
    }
}