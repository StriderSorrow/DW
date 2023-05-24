using DW.Api.Models.Requests;
using DW.Api.Service;
using DW.Data.Database;
using DW.Data.Database.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DW.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly UserManager<DwUser> _users;
        private readonly DwDbContext _context;
        private readonly JwtConfig _jwtConfig;

        public UserController(DwDbContext context, UserManager<DwUser> users, IOptionsMonitor<JwtConfig> optionsMonitor)
        {
            _context = context;
            _users = users;
            _jwtConfig = optionsMonitor.CurrentValue;
        }

        [HttpPost("reg")]
        public async Task<IActionResult> Reg(RegRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var euser = await _users.FindByEmailAsync(request.Email);
            if (euser != null)
            {
                return Conflict("Аккаунт с таким адресом электронной почты уже зарегистрирован!");
            }
            var user = new DwUser { Email  = request.Email, UserName = request.Username, Nickname = request.Username};
            var result = await _users.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                return Ok(await user.SendConfirmationEmail(_context));
            }
            return BadRequest(result.Errors.Select(x=>x.Description).ToList());
        }

        [HttpPost("auth")]
        public async Task<IActionResult> Auth(AuthRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _users.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return NotFound();
            }
            if (await _users.CheckPasswordAsync(user, request.Password))
            {
                var code = await user.SendConfirmationEmail(_context);
                return Ok(code);
            }
            return NotFound();
        }
        [HttpPost("authconfirm")]
        public async Task<IActionResult> AuthConfirm(AuthConfirmRequest request)
        {
            var conf = _context.Confirmations.Include(x=>x.User).FirstOrDefault(x => x.Code == request.Code);
            if (conf == null)
            {
                return BadRequest();
            }
            if (conf.MailCode != request.EmailCode)
            {
                return BadRequest();
            }
            var user = conf.User;
            var jwt = await GenerateJwtToken(user);
            _context.Remove(conf);
            await _context.SaveChangesAsync();
            return Ok(jwt);
        }

        [HttpGet("ping")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Ping()
        {
            return Ok("pong");
        }

        private async Task<string> GenerateJwtToken(DwUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.Add(_jwtConfig.ExpiryTimeFrame),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);
            return jwtToken;
        }
    }
}
