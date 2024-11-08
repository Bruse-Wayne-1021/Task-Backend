using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using TaskManager.DBContext;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Register : ControllerBase
    {
        private readonly TaskContext _taskContext;
        public Register(TaskContext taskContext)
        {
            _taskContext = taskContext;
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

                var Response = new UserRegistration
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email=user.Email,
                };
                return Ok(Response);


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


          
       
    }
}
