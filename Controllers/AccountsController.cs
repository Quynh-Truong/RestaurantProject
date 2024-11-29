using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RestaurantProject.Data;
using RestaurantProject.Models;
using RestaurantProject.Models.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RestaurantProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        //one should divide this code to services and repos...
        private readonly RestaurantContext _context;
        private readonly IConfiguration _configuration;//added for GenerateJwtToken-method

        public AccountsController(RestaurantContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterAccountDTO registerAccount)
        {
            var existingAccount = await _context.Accounts.SingleOrDefaultAsync(a => a.Email == registerAccount.Email);

            if (existingAccount != null)
            {
                return BadRequest("E-mail is already registered");
            }

            //added BCrypt.Net-Next nuget package 
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(registerAccount.Password);

            var newAccount = new Account
            {
                FirstName = registerAccount.FirstName,
                LastName = registerAccount.LastName,
                Email = registerAccount.Email,
                PasswordHash = passwordHash
            };

            _context.Accounts.Add(newAccount);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginAccountDTO loginAccount)
        {
            var account = await _context.Accounts.SingleOrDefaultAsync(a => a.Email == loginAccount.Email);

            if (account == null || !BCrypt.Net.BCrypt.Verify(loginAccount.Password, account.PasswordHash))
            {
                return Unauthorized("Invalid e-mail or password");
            }

            //a string
            var token = GenerateJwtToken(account);

            //takes string, returns an object
            return Ok(new { token });
        }

        //this should be created in a seperate class - and can be public there.
        private string GenerateJwtToken(Account account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            var claims = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, $"{account.FirstName} {account.LastName}"),
                //added to check that logged in user is admin/authorized
                new Claim("IsAdmin", account.IsAdmin.ToString()),
                //new Claim(ClaimTypes.Role, "Admin"),//not needed, but here we use it for MVC
                new Claim(ClaimTypes.Email, account.Email)
            });

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };


            //creating token to be sent with API requests
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
