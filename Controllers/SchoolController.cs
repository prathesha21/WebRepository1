using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebRepository1.Authetication;
using WebRepository1.Domain;
using WebRepository1.Interface;
using WebRepository1.Models.Dtos;

namespace WebRepository1.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private readonly ISchoolRepository schoolRepository;

        SchoolDbContext db;
        private readonly IConfiguration configuration;

        public SchoolController(ISchoolRepository schoolRepository, SchoolDbContext db,IConfiguration configuration)
        {
            this.schoolRepository = schoolRepository;
            this.db = db;
            this.configuration = configuration;
        }
        [HttpGet]
        public ActionResult Get()
        {
            var ab = schoolRepository.GetAll();
            return Ok(ab);
        }

        [HttpGet("{id:int}")]
        public ActionResult Get(int id)
        {
            var ac = schoolRepository.GetById(id);
            return Ok(ac);
        }


        [HttpPost]
        public ActionResult Post([FromBody] School school)
        {
            var bc = schoolRepository.Add(school);
            return Ok(bc);
        }
        [HttpPut("{id:int}")]
        public IActionResult Put(int id, School school)
        {
            var bd = schoolRepository.Uppdatedata(id, school);
            return Ok(bd);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            schoolRepository.Delete(id);
            return Ok();
        }
        [AllowAnonymous]

        [HttpPost("login")]
        public IActionResult PostLogin(LoginRequestDto uuser)
        {
            var result = new LoginResponseDto();

            var user = db.StudentSchool.FirstOrDefault(p => p.Name == uuser.Name && p.Email == uuser.Email);
            if (user != null)
            {
                var token = GenerateJwtToken(user.Name);
                result.Id = user.Id;
                result.Username = user.Name;
                result.Token = token;
                return Ok(result);
            }
            else
            {
                return Unauthorized("Invalid username and email");

            }

        }
        private string GenerateJwtToken(string username)
        {
            var jwtSettings = configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
    new Claim(JwtRegisteredClaimNames.Sub, username),
    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
    new Claim(ClaimTypes.Role, "Admin")
};

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpiryInMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



    }
}

