using AuthTestApi.Data;
using AuthTestApi.Dtos;
using AuthTestApi.Helpers;
using AuthTestApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthTestApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly Jwt _jwtservice;
        public AuthController(IUserRepository userRepository,Jwt jwtservice)
        {
            _userRepository = userRepository;
            _jwtservice = jwtservice;
        }
        [HttpPost("register")]
        //[HttpGet]
        public IActionResult Register(Register register)
        {
            //return Ok("Success");
            var user = new User
            {
                Name = register.Name,
                Email = register.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(register.Password)
            };
            _userRepository.Create(user);
            return Ok("Success");
        }
        [HttpPost("login")]
        public IActionResult Login(Login login)
        {
            var user = _userRepository.GetEmail(login.Email);

            if (user == null)
                return BadRequest(new { message = "Invalid request!" });

            if (!BCrypt.Net.BCrypt.Verify(login.Password,user.Password))
            {
                return BadRequest(new { message = "Invalid request!" });
            }

            

            var jwt = _jwtservice.Generate(user.Id);

            Response.Cookies.Append("jwt", jwt, new CookieOptions
            {
                HttpOnly = true
            });

            return Ok(
            new
            {
                message = "Success!"

            });
        }
        [HttpGet("user")]
        public IActionResult User()
        {
            try
            {
                var jwt = Request.Cookies["jwt"];

                var token = _jwtservice.VerifyJwt(jwt);

                int userId = int.Parse(token.Issuer);

                var user = _userRepository.GetUserId(userId);

                return Ok(user);
            }
            catch(Exception e)
            {
                return Unauthorized(e);
            }
        }
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            return Ok(new
            {
                message = "Log Out!"
            });
        }
    }
}
