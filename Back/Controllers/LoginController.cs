using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Back.Models;


namespace Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly CruzRojaContext _context;

        public LoginController(IConfiguration config, CruzRojaContext context)
        {
            _configuration = config;
            _context = context;
        }

        [HttpPost]
        //El Usuario se autentifica ingresando su Email/Dni y password 

        public async Task<IActionResult> Post(Users _userData)
        {

            if (_userData != null && _userData.Dni != null && _userData.Password != null)
            {
                var user = await GetUser(_userData.Dni, _userData.Password);

                if (user != null)
                {
                    // se crean los claim que permiten agregrar mayor detalles sobre el usuario que se esta autenticando
                    var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("Id:", user.IdUsers.ToString()),
                    new Claim("Name:", user.Name),
                    new Claim("LastName:", user.LastName),
                    new Claim("Dni:", user.Dni),
                    new Claim("Phone:", user.Phone),
                    new Claim("Email:",user.Email),
                    new Claim("Gender:",user.Gender),
                    new Claim("State:",user.State)
                   };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));


                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddYears(5), signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest(); // respuesta de solicitud incorrecta error 400
            }
        }

        private async Task<Users> GetUser(string dni, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Dni == dni && u.Password == password);
        }
    }
}