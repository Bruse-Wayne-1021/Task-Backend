using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManager.DBContext;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Register : ControllerBase
    {
        private readonly TaskContext _taskContext;
        private readonly IConfiguration _configuration;
        public Register(TaskContext taskContext, IConfiguration configuration)
        {
            _taskContext = taskContext;
            _configuration = configuration;
        }


        [HttpPost("Registration")]
        public async Task<IActionResult> UserToRegister(Registration registration)
        {
            try
            {
                var RegReq = new UserRegistration
                {
                    FullName = registration.FullName,
                    Email = registration.Email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(registration.Password),
                    Role=registration.Role,
                    
                    
                };
                var data=await _taskContext.UserRegistration.AddAsync(RegReq);
                await _taskContext.SaveChangesAsync();
                var ResResponse = new Registration
                {
                    Id=data.Entity.Id,
                    FullName = data.Entity.FullName,
                    Email=data.Entity.Email,
                    Role=data.Entity.Role,
                   
                };
                return Ok(ResResponse);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("Login")]
        public async Task<IActionResult>Login(Loginuser emailAndPassword)
        {
            try
            {
               var user=await _taskContext.UserRegistration.FirstOrDefaultAsync(f=>f.Email==emailAndPassword.Email);
                if (user == null)
                {
                    throw new Exception("User Not Found ");
                }

                var IsValid = BCrypt.Net.BCrypt.Verify(emailAndPassword.Password, user.PasswordHash);
                if (!IsValid)
                {
                    throw new Exception("PassWord DoesNot Matching");
                }

                //var Response = new UserRegistration
                //{
                //    Id = user.Id,
                //    FullName = user.FullName,
                //    Email=user.Email,
                //};
                //return Ok(Response);

                var token=createToken(user);
                return Ok(token);


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        private string createToken(UserRegistration userRegistration)
        {
            var claimlist = new List<Claim>();
            claimlist.Add(new Claim("Id", userRegistration.Id.ToString()));

            claimlist.Add(new Claim("FullName", userRegistration.FullName));
            claimlist.Add(new Claim("Email",userRegistration.Email));
            claimlist.Add(new Claim("Role", userRegistration.Role.ToString()));

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]));
            var creadintials=new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"],
                claims: claimlist,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: creadintials

            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


          
       
    }
}
